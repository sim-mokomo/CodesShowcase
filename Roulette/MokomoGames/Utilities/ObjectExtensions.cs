using MokomoGames.Utilities;

namespace Utilities
{
    public static class ObjectExtensions
    {
        public static void Release(this object self)
        {
            if (self is IReleaseable obj)
            {
                obj.Release();
            }
        }
    }
}