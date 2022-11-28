using System;
using UnityEngine;
using UnityEngine.UI;

namespace MokomoGames.UI
{
    public class IconButtonView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image icon;
        private Color defaultColor;

        private void Awake()
        {
            button.onClick.AddListener(() => OnClickedButton?.Invoke());
            defaultColor = button.image.color;
        }

        public event Action OnClickedButton;

        public void SetEnable(bool enable)
        {
            button.image.color = enable ? defaultColor : Color.gray; 
            button.enabled = enable;
            icon.color = enable ? Color.white : Color.gray;
        }
    }
}