using UnityEngine;

namespace MokomoGames.Utilities
{
    public static class QuaternionExtensions
    {
        public static Quaternion LookRotation2D(Vector3 lookDirection)
        {
            var angle = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, -Vector3.forward);
        }
    }
}
