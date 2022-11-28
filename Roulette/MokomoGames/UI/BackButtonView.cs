using System;
using UnityEngine;
using UnityEngine.UI;

namespace Roulette.Home
{
    public class BackButtonView : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        public event Action OnClickedButton;

        private void Start()
        {
            backButton.onClick.AddListener(() => { OnClickedButton?.Invoke(); });
        }
    }
}