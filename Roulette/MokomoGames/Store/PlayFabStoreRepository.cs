using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using MokomoGames.Network;
using MokomoGames.User;
using PlayFab;
using PlayFab.ClientModels;
using Protobuf;
using UnityEngine;
using UnityEngine.Purchasing;

namespace MokomoGames.Store
{
    public static class PlayFabStoreRepository
    {
        public static UniTask<List<ProductDefinition>> RequestCatalog()
        {
            var source = new UniTaskCompletionSource<List<ProductDefinition>>();
            PlayFabClientAPI.GetCatalogItems(
                new GetCatalogItemsRequest(),
                result =>
                {
                    var products = result.Catalog
                        .Select(x => 
                            new ProductDefinition(
                                x.ItemId,
                                ProductTypeExtensions.FromString(x.ItemClass)))
                        .ToList();
                    source.TrySetResult(products);
                },
                error => { Debug.LogError($"Failed Get Catalog {error.GenerateErrorReport()}"); }
            );
            return source.Task;
        }
        
        public static UniTask AddItemAsync(string productId)
        {
            var request = new AddProductRequest
            {
                ItemID = productId
            };
            return new ApiRequestRunner().Request
                <AddProductRequest, AddProductResponse>
                (ApiRequestRunner.ApiName.AddProduct, request);
        }

        public static void ValidateGooglePlayReceipt(
            PurchaseEventArgs purchaseEventArgs,
            Receipt receipt,
            Action<ValidateGooglePlayPurchaseResult> onCompleted, 
            Action<PlayFabError> onError)
        {
            var googlePayload = GoogleReciptPayload.Parser.ParseJson(receipt.Payload);
            PlayFabClientAPI.ValidateGooglePlayPurchase(
                new ValidateGooglePlayPurchaseRequest
                {
                    CurrencyCode = purchaseEventArgs.purchasedProduct.metadata.isoCurrencyCode,
                    PurchasePrice = (uint) (purchaseEventArgs.purchasedProduct.metadata.localizedPrice * 100),
                    ReceiptJson = receipt.Payload,
                    Signature = googlePayload.Signature
                },
                onCompleted,
                onError);
        }

        public static void ValidateAppStoreReceipt(
            PurchaseEventArgs purchaseEventArgs,
            Receipt receipt,
            Action<ValidateIOSReceiptResult> onCompleted, 
            Action<PlayFabError> onError)
        {
            var base64Receipt = receipt.Payload;
            PlayFabClientAPI.ValidateIOSReceipt(
                new ValidateIOSReceiptRequest
                {
                    CurrencyCode = purchaseEventArgs.purchasedProduct.metadata.isoCurrencyCode,
                    PurchasePrice = (int) (purchaseEventArgs.purchasedProduct.metadata.localizedPrice * 100),
                    ReceiptData = base64Receipt
                },
                onCompleted,
                onError);
        }
        
        public class PlayFabErrorException<T> : Exception
        {
            private T _error;

            public PlayFabErrorException(T error)
            {
                _error = error;
            }
        }
        
        public static UniTask<UserInventory> LoadInventoryItems()
        {
            var source = new UniTaskCompletionSource<UserInventory>();
            var request = new GetUserInventoryRequest();
            PlayFabClientAPI.GetUserInventory(
                request,
                result =>
                {
                    var itemList =
                        result
                            .Inventory
                            .Select(x => new UserInventoryItem(
                                new ItemId(x.ItemId),
                                x.ItemClass)
                            )
                            .ToList();
                    source.TrySetResult(new UserInventory(itemList));
                },
                error => throw new PlayFabErrorException<PlayFabError>(error));
            return source.Task;
        }
    }
}