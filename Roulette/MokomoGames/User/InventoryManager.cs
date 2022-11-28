using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MokomoGames.User
{
    public class InventoryManager : MonoBehaviour
    {
        private UserInventory _userInventory;
        public bool HasItem(ItemId itemId) => _userInventory.HasItem(itemId);
        public async UniTask<UserInventory> LoadInventoryItems()
        {
            _userInventory = await MokomoGames.Store.PlayFabStoreRepository.LoadInventoryItems();
            return _userInventory;
        }

        public UniTask AddItem(string itemId)
        {
            return MokomoGames.Store.PlayFabStoreRepository.AddItemAsync(itemId);
        }
    }
}