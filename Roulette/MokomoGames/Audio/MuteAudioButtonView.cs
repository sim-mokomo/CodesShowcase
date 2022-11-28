using System;
using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class MuteAudioButtonView : MonoBehaviour
    {
        [SerializeField] private Button muteButton;
        [SerializeField] private Image soundIcon;
        [SerializeField] private Sprite muteSprite;
        [SerializeField] private Sprite unMuteSprite;
        public event Action OnToggleMute;

        private void Awake()
        {
            muteButton.onClick.AddListener(() =>
            {
                OnToggleMute?.Invoke();
            });
        }

        public void UpdateRender(bool isMute)
        {
            soundIcon.sprite = isMute ? muteSprite : unMuteSprite;
        }
    }
}