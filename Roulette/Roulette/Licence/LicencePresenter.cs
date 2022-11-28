using System;
using System.Collections.Generic;
using System.Text;
using MokomoGames.UI.Animation;
using MokomoGames.UI.BlackCurtain;
using MokomoGames.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roulette.Licence
{
    public class LicencePresenter : MonoBehaviour , IReleaseable
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private TextMeshProUGUI licenceContent;
        [SerializeField] private float showingDuration;
        [SerializeField] private SlideInOutAnimation showAnimation;
        [SerializeField] private BlackCurtain blackCurtain;

        private void Awake()
        {
            closeButton.onClick.AddListener(() => { Show(false, showingDuration); });
        }

        public event Action OnClosed;

        public void Initialize(IEnumerable<LicenceEntity> licenceEntities)
        {
            licenceContent.text = CreateContent(licenceEntities);
        }
        
        public void Open()
        {
            Show(false, 0f);
            Show(true, showingDuration);
        }

        private static string CreateContent(IEnumerable<LicenceEntity> licenceEntities)
        {
            var contentBuilder = new StringBuilder();
            foreach (var licenceEntity in licenceEntities)
            {
                contentBuilder.AppendLine($"■{licenceEntity.Title}");
                contentBuilder.AppendLine(licenceEntity.Content);
            }
            
            return contentBuilder.ToString();
        }

        private void Show(bool show, float animDuration)
        {
            if (animDuration <= 0.0f)
            {
                gameObject.SetActive(show);
                blackCurtain.Show(show, 0f);
                showAnimation.Show(show, 0f);
                return;
            }

            if (show) gameObject.SetActive(true);

            blackCurtain.Show(show, animDuration);
            showAnimation.Show(show, animDuration, () =>
            {
                if (show) return;
                OnClosed?.Invoke();
                gameObject.SetActive(false);
            });
        }

        public void Release()
        {
            OnClosed = null;
        }
    }
}