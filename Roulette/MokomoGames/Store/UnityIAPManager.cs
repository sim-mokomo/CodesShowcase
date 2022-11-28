using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Cysharp.Threading.Tasks;
using Protobuf;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

namespace MokomoGames.Store
{
    public class UnityIAPManager : IStoreListener
    {
        private IExtensionProvider _extensionProvider;
        private IStoreController _storeController;
        private CancellationToken _cancellationToken;
        private readonly Func<UniTask<List<ProductDefinition>>> _requestCatalog;
        private AsyncReactiveProperty
            <(IStoreController storeController, IExtensionProvider extensionProvider)> _onInitializedStore;
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private UniTask<(IStoreController storeController, IExtensionProvider extensionProvider)> OnInitializedStoreAsync
        {
            get
            {
                if (_onInitializedStore != null) return _onInitializedStore.WaitAsync();
                
                _onInitializedStore =
                    new AsyncReactiveProperty
                        <(IStoreController storeController, IExtensionProvider extensionProvider)>(default);
                _onInitializedStore.AddTo(_cancellationToken);
                return _onInitializedStore.WaitAsync();
            }
        }
        private AsyncReactiveProperty<InitializationFailureReason> _onInitializedStoreFailed;
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private UniTask<InitializationFailureReason> OnInitializeStoreFailedAsync
        {
            get
            {
                if (_onInitializedStoreFailed != null) return _onInitializedStoreFailed.WaitAsync();

                _onInitializedStoreFailed = new AsyncReactiveProperty<InitializationFailureReason>(default);
                _onInitializedStoreFailed.AddTo(_cancellationToken);
                return _onInitializedStoreFailed.WaitAsync();
            }
        }

        public event Action OnPurchased;

        public class StoreException : Exception
        {
               
        }

        public class FailedToInitializedStoreException : StoreException
        {
            public InitializationFailureReason Reason { get; }
            public FailedToInitializedStoreException(InitializationFailureReason reason)
            {
                Reason = reason;
            }
        } 
        
        public UnityIAPManager(Func<UniTask<List<ProductDefinition>>> requestCatalog, CancellationToken cancellationToken)
        {
            _requestCatalog = requestCatalog;
            _cancellationToken = cancellationToken;
        }

        public async UniTask InitializeAsync()
        {
            var task = UniTask.WhenAny(
                OnInitializedStoreAsync,
                OnInitializeStoreFailedAsync
            );
            
            // NOTE: カタログ作成
            var productDefineList = await _requestCatalog();
            var bundler = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            bundler.AddProducts(productDefineList);
            
            UnityPurchasing.Initialize(this, bundler);

            var (taskIndex,
                (storeController, extensionProvider),
                initializationFailureReason) = await task;

            // NOTE: OnInitialized
            if (taskIndex == 0)
            {
                _storeController = storeController;
                _extensionProvider = extensionProvider;
            }// NOTE: OnInitializeFailed
            else if (taskIndex == 1)
            {
                throw new FailedToInitializedStoreException(initializationFailureReason);
            }
        }
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions) => 
            _onInitializedStore.Value = (controller, extensions);
        public void OnInitializeFailed(InitializationFailureReason error)
            => _onInitializedStoreFailed.Value = error;
        
        public void Restore()
        {
            void RestoreTransactionEvent(bool success)
            {
                Debug.Log(success ? "リストアトランザクション成功" : "リストアトランザクション失敗");
            }
#if UNITY_IOS || UNITY_EDITOR_OSX
            _extensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(RestoreTransactionEvent);
#elif UNITY_ANDROID
            _extensionProvider.GetExtension<IGooglePlayStoreExtensions>().RestoreTransactions(RestoreTransactionEvent);
#endif
        }
        
        public void Purchase(string productId)
        {
            _storeController.InitiatePurchase(productId);
        }

        // NOTE: Pending中のままアプリが落ちた場合、IAP初期化以降再度実行される。
        // Androidの場合、IAP初期化後、リストア目的で各種プロダクトの処理が走る。
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEventArgs)
        {
            if (purchaseEventArgs.purchasedProduct == null)
            {
                Debug.LogError("知らない製品を購入しようとしています");
                return PurchaseProcessingResult.Complete;
            }
         
            // NOTE: 未購入/消費済みの場合はfalse
            if (!purchaseEventArgs.purchasedProduct.hasReceipt)
            {
                Debug.LogError("このプロダクトはレシートを持っていません");
                return PurchaseProcessingResult.Complete;
            }

#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR_OSX
            // NOTE: 購入済みの永続アイテムは復元を行う。
            var isPermanentItem = 
                purchaseEventArgs.purchasedProduct.definition.type == ProductType.Subscription ||
                purchaseEventArgs.purchasedProduct.definition.type == ProductType.NonConsumable;
            var purchasedPermanentItem = isPermanentItem && purchaseEventArgs.purchasedProduct.hasReceipt;
            if (purchasedPermanentItem)
            {
                try
                {
                    Debug.Log("復元時のレシートのローカル検証を開始");
                    // var validator = new CrossPlatformValidator(
                    //     GooglePlayTangle.Data(),
                    //     AppleTangle.Data(),
                    //     Application.identifier
                    // );
                    // var receipts = validator.Validate(purchaseEventArgs.purchasedProduct.receipt);
                    Debug.Log("復元時のレシートのローカル検証が成功");
                    // foreach (var purchaseReceipt in receipts)
                    // {
                    //     Debug.Log(purchaseReceipt.productID);
                    //     Debug.Log(purchaseReceipt.purchaseDate);
                    //     Debug.Log(purchaseReceipt.transactionID);
                    // }

                    // TODO: アイテム追加
                    _storeController.ConfirmPendingPurchase(purchaseEventArgs.purchasedProduct);
                }
                catch (IAPSecurityException exception)
                {
                    Debug.Log("復元時のレシートのローカル検証が失敗・または違法");
                    return PurchaseProcessingResult.Complete;
                }
            }
#endif

            var receipt = Receipt.Parser.ParseJson(purchaseEventArgs.purchasedProduct.receipt);
#if UNITY_ANDROID
            PlayFabStoreRepository.ValidateGooglePlayReceipt(
                purchaseEventArgs,
                receipt,
                result =>
                {
                    _storeController.ConfirmPendingPurchase(purchaseEventArgs.purchasedProduct);
                    OnPurchased?.Invoke();
                    Debug.Log("Android購入確定");
                },
                error =>
                {
                    _storeController.ConfirmPendingPurchase(purchaseEventArgs.purchasedProduct);
                    Debug.Log($"Android購入確定失敗 {error.GenerateErrorReport()}");
                }
            );
#elif UNITY_IOS || UNITY_EDITOR_OSX
            PlayFabStoreRepository.ValidateAppStoreReceipt(
                purchaseEventArgs,
                receipt,
                result =>
                {
                    _storeController.ConfirmPendingPurchase(purchaseEventArgs.purchasedProduct);
                    OnPurchased?.Invoke();
                    Debug.Log("iOS購入確定成功");
                },
                error =>
                {
                    _storeController.ConfirmPendingPurchase(purchaseEventArgs.purchasedProduct);
                    Debug.Log($"iOS購入確定失敗 {error.GenerateErrorReport()}");
                }
            );
#endif
            Debug.Log($"トランザクション確定待ち... {purchaseEventArgs.purchasedProduct.transactionID}");
            return PurchaseProcessingResult.Pending;
        }
        
        public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
        {
            Debug.LogError($"購入失敗: {reason.ToString()}");
        }
    }
}