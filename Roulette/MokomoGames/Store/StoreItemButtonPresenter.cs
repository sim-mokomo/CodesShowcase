// using Store;
// using UniRx;
// using UnityEngine;
// using Zenject;
//
// namespace MokomoGames.Store
// {
//     public class StoreItemButtonPresenter : MonoBehaviour
//     {
//         [SerializeField] private StoreItemButtonView _storeItemButtonView;
//         [SerializeField] private float showAnimDuration;
//         [SerializeField] private string productID;
//         private StoreManger _storeManger;
//
//         private InventoryManager _inventoryManager;
//         
//         [Inject]
//         private void Constructor(StoreManger storeManger,InventoryManager inventoryManager)
//         {
//             _storeManger = storeManger;
//             _inventoryManager = inventoryManager;
//             _storeItemButtonView.OnClickPurchasedButton.Subscribe(_ =>
//             {
//                 _storeManger.Purchase(productID);
//             });
//         
//             _inventoryManager.OnUpdated += () =>
//             {
//                 if (_inventoryManager.HasItem(ItemID.NO_ADS))
//                 {
//                     _storeItemButtonView.Show(false, showAnimDuration);
//                 }
//             };
//             _storeManger.IapService.OnInitialize += () =>
//             {
//                 if (_inventoryManager.HasItem(ItemID.NO_ADS)) return;
//                 _storeItemButtonView.Show(true, showAnimDuration);
//             };
//         }
//     }
// }