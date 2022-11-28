using System.Collections.Generic;
using System.Linq;
using UnityEngine.Purchasing;

namespace MokomoGames.User
{
    public class UserInventory
    {
        private readonly List<UserInventoryItem> _userInventoryItems;
        public UserInventory(List<UserInventoryItem> inventoryItems)
        {
            _userInventoryItems = inventoryItems;
        }

        public UserInventoryItem FindSubscriptionInfo(ItemId itemId)
        {
            var item = _userInventoryItems.FirstOrDefault(x =>
                Equals(x.ItemId, itemId) && x.ItemType == ProductType.Subscription);
            return item;
        }

        public bool HasItem(ItemId itemId)
        {
            return _userInventoryItems.Any(x => x.ItemId.Equals(itemId));
        }
    }
}