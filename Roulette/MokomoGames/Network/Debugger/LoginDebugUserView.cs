using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MokomoGames.Network.Debugger
{
    public class LoginDebugUserView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI userIdText;

        public event Action<string> OnClickedCell;

        public void Initialize(string userId)
        {
            userIdText.text = userId;
            button.onClick.AddListener(() =>
            {
                OnClickedCell?.Invoke(userId);
            });
        }
    }
}