using MokomoGames.Store;
using UnityEngine.Purchasing;

namespace MokomoGames.User
{
    public class UserInventoryItem
    {
        public ItemId ItemId { get; }
        public ProductType ItemType { get; }

        public UserInventoryItem(ItemId itemId, string itemType)
        {
            ItemId = itemId;
            ItemType = ProductTypeExtensions.FromString(itemType);
        }
    }
}