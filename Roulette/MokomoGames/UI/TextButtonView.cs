using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MokomoGames.UI
{
    public class TextButtonView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;
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
        }
    }
}