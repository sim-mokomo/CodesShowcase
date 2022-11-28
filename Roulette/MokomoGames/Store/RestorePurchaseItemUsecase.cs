// using Ads;
// using Inventory;
// using PlayerInventory;
// using LoadInventory;
// using MokomoGames.Store;
// using Zenject;
//
// namespace Store
// {
//     public class RestorePurchaseItemUsecase
//     {
//         private readonly IInventory _inventory;
//         private readonly RestorePurchaseItemPresenter _restorePurchaseItemPresenter;
//         private readonly StoreManger _storeManger;
//
//         public RestorePurchaseItemUsecase(RestorePurchaseItemPresenter restorePurchaseItemPresenter)
//         {
//             _restorePurchaseItemPresenter = restorePurchaseItemPresenter;
//             _restorePurchaseItemPresenter.Show(false);
//             _restorePurchaseItemPresenter.OnRestore += () => { _storeManger.Restore(); };
//
//             _inventory = ProjectContext.Instance.Container.Resolve<IInventory>();
//             _storeManger = ProjectContext.Instance.Container.Resolve<StoreManger>();
//             _storeManger.IapService.OnRestored += () =>
//             {
//                 _inventory.LoadInventory(new LoadInventoryRequest(), null);
//             };
//         }
//     }
// }