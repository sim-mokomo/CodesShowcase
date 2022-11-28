using System;
using MokomoGames.UI.Animation;
using MokomoGames.UI.BlackCurtain;
using MokomoGames.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roulette.Home.Template
{
    public class TemplateConfirmDialog : MonoBehaviour, IReleaseable
    {
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private float duration;
        [SerializeField] private BlackCurtain blackCurtain;
        [SerializeField] private SlideInOutAnimation slideInOutAnimation;
        [SerializeField] private TMP_InputField templateNameField;
        private Protobuf.DataSet _dataSetEntity;

        private void Awake()
        {
            templateNameField.onValueChanged.AddListener(x => { _dataSetEntity.Title = x; });

            confirmButton.onClick.AddListener(() =>
            {
                Show(false, duration);
                OnSubmit?.Invoke();
            });

            cancelButton.onClick.AddListener(() => { Show(false, duration); });
        }

        public event Action OnClosed;
        public event Action OnOpened;
        public event Action OnSubmit;

        public void Open(Protobuf.DataSet dataSetEntity)
        {
            _dataSetEntity = dataSetEntity;
            templateNameField.text = string.Empty;
            Show(false, 0f);
            Show(true, duration);
        }

        private void Show(bool show, float d)
        {
            if (gameObject.activeSelf == show) return;

            var immediately = d <= 0f;
            if (immediately)
            {
                gameObject.SetActive(show);
                blackCurtain.Show(show, 0f);
                slideInOutAnimation.Show(show, 0f);
                return;
            }

            if (show) gameObject.SetActive(true);

            blackCurtain.Show(show, d);
            slideInOutAnimation.Show(show, d, () =>
            {
                if (show)
                {
                    OnOpened?.Invoke();
                }
                else
                {
                    OnClosed?.Invoke();
                    gameObject.SetActive(false);
                }
            });
        }

        public void Release()
        {
            OnOpened = null;
            OnClosed = null;
            OnSubmit = null;
        }
    }
}