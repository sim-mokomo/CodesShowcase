using System;
using System.Collections.Generic;
using MokomoGames.Localization;
using MokomoGames.UI.Animation;
using MokomoGames.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace MokomoGames.UI.Dialog
{
    public class ConfirmDialog : MonoBehaviour, IReleaseable
    {
        public enum DialogType
        {
            Confirm,
            YesOrNo
        }

        [SerializeField] private Button confirmButton;
        [SerializeField] private Button okButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private LocalizeText headerText;
        [SerializeField] private LocalizeText contentText;
        [SerializeField] private float showAnimDuration;
        [SerializeField] private BlackCurtain.BlackCurtain blackCurtain;
        [SerializeField] private SlideInOutAnimation dialogSlideAnimation;
        [SerializeField] private List<GameObject> typeButtonContainerList;
        private DialogType _dialogType;

        private void Awake()
        {
            confirmButton.onClick.AddListener(() => { Show(_dialogType, false, showAnimDuration); });

            okButton.onClick.AddListener(() => { Show(_dialogType, false, showAnimDuration); });

            cancelButton.onClick.AddListener(() => { Show(_dialogType, false, showAnimDuration); });
        }

        public event Action OnClosed;
        public event Action OnOpened;

        public void Open(ConfirmDialogParam param)
        {
            if (string.IsNullOrEmpty(param.headerKey))
                headerText.SetTextDirectly(param.header);
            else
                headerText.SetTextFromKey(param.headerKey);

            if (string.IsNullOrEmpty(param.contentkey))
                contentText.SetTextDirectly(param.content);
            else
                contentText.SetTextFromKey(param.contentkey);
            
            Show(param.dialogType, false, 0f);
            Show(param.dialogType, true, showAnimDuration);
        }

        private void Show(DialogType type, bool show, float animDuration)
        {
            // NOTE: タイプごとにボタンを表示
            _dialogType = type;
            foreach (var typeButtonContainer in typeButtonContainerList) typeButtonContainer.SetActive(false);
            typeButtonContainerList[(int)type].SetActive(true);

            // NOTE: 即時不可視化対応
            if (animDuration <= 0f)
            {
                gameObject.SetActive(show);
                blackCurtain.Show(show, 0f);
                dialogSlideAnimation.Show(show, 0f);
                return;
            }

            if (show) gameObject.SetActive(true);

            blackCurtain.Show(show, animDuration);
            dialogSlideAnimation.Show(show, animDuration, () =>
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

        public class ConfirmDialogParam
        {
            public string content;
            public string contentkey;
            public DialogType dialogType;
            public string header;
            public string headerKey;
        }

        public void Release()
        {
            OnClosed = null;
            OnOpened = null;
        }
    }
}