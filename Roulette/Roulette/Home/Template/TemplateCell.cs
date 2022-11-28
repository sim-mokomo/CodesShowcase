using System;
using MokomoGames.UI.Animation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roulette.Home.Template
{
    public class TemplateCell : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private Button editButton;
        [SerializeField] private Button trashButton;
        [SerializeField] private ScaleAnimator scaleAnimator;
        private Protobuf.DataSet _dataSetEntity;

        private void Awake()
        {
            editButton.onClick.AddListener(() => { OnClickEditButton?.Invoke(_dataSetEntity); });
            trashButton.onClick.AddListener(() => { OnClickedTrashButton?.Invoke(_dataSetEntity); });

            scaleAnimator.Init(new Vector3(1, 0, 1), Vector3.one);
        }

        public event Action<Protobuf.DataSet> OnClickedTrashButton;
        public event Action<Protobuf.DataSet> OnClickEditButton;

        public void Init(Protobuf.DataSet dataSetEntity, float duration)
        {
            _dataSetEntity = dataSetEntity;
            titleText.text = _dataSetEntity.Title;

            scaleAnimator.PlayShowAnimation(false, 0f);
            scaleAnimator.PlayShowAnimation(true, duration);
        }
    }
}