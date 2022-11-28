using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MokomoGames.Utilities;
using Protobuf;
using UnityEngine;

namespace Roulette.Home.Template
{
    public class TemplateTopPresenter : MonoBehaviour, IReleaseable
    {
        [SerializeField] private BackButtonView backButtonView;
        [SerializeField] private RectTransform contentRoot;
        [SerializeField] private TemplateCell templateCellPrefab;
        [SerializeField] private float cellAnimDuration = 0.25f;
        [SerializeField] private float showCellAnimIntervalSeconds = 0.25f;
        private readonly List<TemplateCell> _cells = new();

        public event Action OnClickedBackButton;
        public event Action<Protobuf.DataSet> OnClickedEditButton;
        public event Action<Protobuf.DataSet> OnClickedTrashButton;

        private void Start()
        {
            backButtonView.OnClickedButton += () => { OnClickedBackButton?.Invoke(); };
        }

        public async UniTaskVoid Init(DataSetList dataSetList)
        {
            Clear();

            foreach (var dataSet in dataSetList.List)
            {
                var cell = Instantiate(templateCellPrefab, contentRoot.transform);
                cell.Init(dataSet, cellAnimDuration);
                cell.OnClickEditButton += OnClickedEditButton;
                cell.OnClickedTrashButton += OnClickedTrashButton;
                _cells.Add(cell);

                await UniTask.Delay(TimeSpan.FromSeconds(showCellAnimIntervalSeconds));
            }
        }

        private void Clear()
        {
            _cells.ForEach(x => Destroy(x.gameObject));
            _cells.Clear();
        }

        public void Release()
        {
            OnClickedBackButton = null;
            OnClickedEditButton = null;
            OnClickedTrashButton = null;
            
            Clear();
        }
    }
}