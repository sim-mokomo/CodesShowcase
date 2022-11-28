using System;
using TMPro;
using UnityEngine;

namespace MokomoGames.Localization
{
    public class LocalizeText : MonoBehaviour
    {
        [SerializeField] private string textKey;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private bool isBitMapFont;
        private LocalizeManager _localizeManager;

        private void OnEnable()
        {
            UpdateSelfAll();
        }

        public void Awake()
        {
            _localizeManager = FindObjectOfType<LocalizeManager>();
            _localizeManager.OnChangedLanguage += UpdateSelfAll;
        }

        private void UpdateSelfAll()
        {
            if (string.IsNullOrEmpty(textKey))
            {
                SetTextDirectly(text.text);   
            }
            else
            {
                SetTextFromKey(textKey);
            }
        }

        public void SetTextFromKey(string key)
        {
            textKey = key;
            if (!_localizeManager.isEndedLoading())
            {
                return;
            }
            
            text.text = string.IsNullOrEmpty(key) ? "" : _localizeManager.GetLocalizedString(textKey);
            UpdateSelfFont();
        }

        public void SetTextDirectly(string content)
        {
            textKey = string.Empty;
            if (!_localizeManager.isEndedLoading())
            {
                return;
            }
            text.text = content;
            UpdateSelfFont();
        }
        
        private void UpdateSelfFont()
        {
            text.font = Resources.Load<TMP_FontAsset>(_localizeManager.LocalizeEntity.Config.GetFontPath(isBitMapFont));
            text.UpdateFontAsset();
        }
    }
}