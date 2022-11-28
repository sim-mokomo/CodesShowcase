using UnityEngine.Purchasing;

namespace MokomoGames.Store
{
    public static class ProductTypeExtensions
    {
        public static ProductType FromString(string fromString)
        {
            return fromString switch
            {
                "NonConsumable" => ProductType.NonConsumable,
                "Consumable" => ProductType.Consumable,
                "Subscription" => ProductType.Subscription,
                _ => ProductType.Consumable
            };
        }
    }
}