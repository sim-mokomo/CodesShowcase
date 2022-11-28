using System;
using UnityEngine;

namespace MokomoGames.Event
{
    public class UnityEventListener : MonoBehaviour
    {
        public event Action<bool> ApplicationFocus;

        public static UnityEventListener Create() =>
            new GameObject("UnityEventListener").AddComponent<UnityEventListener>();
        private void OnApplicationFocus(bool hasFocus)
        {
            ApplicationFocus?.Invoke(hasFocus);
        }
    }
}