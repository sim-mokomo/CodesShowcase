using System;
using Audio;
using MokomoGames.UI.Animation;
using MokomoGames.UI.BlackCurtain;
using MokomoGames.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Roulette.GameConfig
{
    public class ConfigsPresenter : MonoBehaviour, IReleaseable
    {
        [SerializeField] private Button licenseButton;
        [SerializeField] private Button muteButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private SlideInOutAnimation slideInOutAnimation;
        [SerializeField] private BlackCurtain blackCurtain;
        [SerializeField] private float showDuration;
        [SerializeField] private MuteAudioButtonView muteAudioButtonView;

        public void Start()
        {
            licenseButton.onClick.AddListener(() => { OnClickedLicenseButton?.Invoke(); });
            muteButton.onClick.AddListener(() => { OnClickedMuteButton?.Invoke(); });
            closeButton.onClick.AddListener(() => { Show(false, showDuration); });
        }

        public event Action OnClickedMuteButton;
        public event Action OnClickedLicenseButton;
        public event Action OnClosed;

        public void Open()
        {
            Show(false, 0f);
            Show(true, showDuration);
        }

        private void Show(bool show, float duration)
        {
            if (show) gameObject.SetActive(true);

            blackCurtain.Show(show, duration);
            slideInOutAnimation.Show(show, duration, () =>
            {
                if (!show)
                {
                    OnClosed?.Invoke();
                    gameObject.SetActive(false);
                }
            });
        }

        public void OnUpdatedGameConfig(MokomoGames.GameConfig.GameConfig gameConfig)
        {
            muteAudioButtonView.UpdateRender(gameConfig.IsMute);
        }

        public void Release()
        {
            OnClickedMuteButton = null;
            OnClickedLicenseButton = null;
            OnClosed = null;
        }
    }
}