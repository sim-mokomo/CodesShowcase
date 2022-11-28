using System;
using DG.Tweening;
using Protobuf;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roulette.Home.Edit
{
    public class EditDataSetItemView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameField;
        [SerializeField] private TMP_InputField numField;
        [SerializeField] private Button trashButton;
        public bool IsPlayingAnimation { get; private set; }
        public DataSetItem DataSetEntityItem { get; private set; }

        private void Awake()
        {
            nameField.onValueChanged.AddListener(name =>
            {
                DataSetEntityItem.Name = name;
                OnChangedTitle?.Invoke(this);
            });

            numField.onValueChanged.AddListener(numStr =>
            {
                numStr = string.IsNullOrEmpty(numStr) ? "0" : numStr;
                DataSetEntityItem.Num = int.TryParse(numStr, out var num) ? num : 0;
                OnChangedNum?.Invoke(this);
            });

            trashButton.onClick.AddListener(() => { OnClickedTrashButton?.Invoke(this); });
        }

        public event Action<EditDataSetItemView> OnChangedTitle;
        public event Action<EditDataSetItemView> OnChangedNum;
        public event Action<EditDataSetItemView> OnClickedTrashButton;

        public void Init(DataSetItem entity, float duration)
        {
            DataSetEntityItem = entity;
            nameField.text = entity.Name;
            numField.text = entity.Num.ToString();

            Show(true, duration, null);
        }

        public void Show(bool show, float duration, Action<DataSetItem> OnCompleted)
        {
            IsPlayingAnimation = true;
            if (show)
            {
                transform.localScale = new Vector3(1, 0, 1);
                transform
                    .DOScale(Vector3.one, duration)
                    .OnComplete(() =>
                    {
                        IsPlayingAnimation = false;
                        OnCompleted?.Invoke(DataSetEntityItem);
                    });
            }
            else
            {
                transform
                    .DOScale(new Vector3(1, 0, 1), duration)
                    .OnComplete(() =>
                    {
                        IsPlayingAnimation = false;
                        OnCompleted?.Invoke(DataSetEntityItem);
                        Destroy(gameObject);
                    });
            }
        }
    }
}