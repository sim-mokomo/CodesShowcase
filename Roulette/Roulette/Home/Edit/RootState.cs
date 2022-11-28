using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using MokomoGames.Extensions;
using MokomoGames.Localization;
using MokomoGames.StateMachine;
using MokomoGames.UI;
using MokomoGames.Utilities;
using Protobuf;
using Roulette.DataSet;
using Roulette.Home.Template;
using UnityEngine;
using Color = UnityEngine.Color;
using Object = UnityEngine.Object;

namespace Roulette.Home.Edit
{
    public class RootState : State, IReleaseable
    {
        private EditTopPresenter _editTopPresenter;
        private Protobuf.DataSet _dataSet;
        private TemplateConfirmDialog _templateConfirmDialog;
        public event Action<Protobuf.DataSet> OnClickedSubmitButton;
        public event Action OnClickedBackButton;

        public override async UniTask Begin(StateChangeRequest inputData, CancellationToken ct)
        {
            await base.Begin(inputData, ct);
            if (inputData is Request stateInputData)
            {
                var uiManager = Object.FindObjectOfType<UIManager>();
                _editTopPresenter = uiManager.Create<EditTopPresenter>(UIManager.CanvasOrder.Center);
                _editTopPresenter.OnClickedSubmitButton += () => { OnClickedSubmitButton?.Invoke(_dataSet); };
                _editTopPresenter.OnClickedBackButton += OnClickedBackButton;
                _editTopPresenter.OnClickedAddButton += () =>
                {
                    var localizeManager = Object.FindObjectOfType<LocalizeManager>();
                    _dataSet.ItemList.Add(new DataSetItem()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Color = ColorExtensions.ConvertColorToProtoBufColor(
                            Color.HSVToRGB(
                                UnityEngine.Random.Range(0.0f,1.0f),
                                UnityEngine.Random.Range(0.3f,0.9f), 
                                UnityEngine.Random.Range(0.3f,0.9f))),
                        Name = $"{localizeManager.GetLocalizedString("NoTitle")}_{_dataSet.ItemList.Count}",
                        Num = 1
                    });
                    _editTopPresenter.UpdateList(_dataSet);
                };
                _editTopPresenter.OnClickedTrashButton += dataSetItem =>
                {
                    var index = _dataSet.ItemList
                        .ToList()
                        .FindIndex(x => x.Id == dataSetItem.Id);
                    _dataSet.ItemList.RemoveAt(index);
                    _editTopPresenter.UpdateList(_dataSet);
                };
                _editTopPresenter.OnChangedItemContent += dataSetItem =>
                {
                    _editTopPresenter.UpdateButtonsEnable(_dataSet);
                };
                _editTopPresenter.OnClickedAddTemplateButton += () =>
                {
                    _templateConfirmDialog = uiManager.Create<TemplateConfirmDialog>(UIManager.CanvasOrder.Front);
                    _templateConfirmDialog.OnClosed += () => { uiManager.Destroy(_templateConfirmDialog); };
                    _templateConfirmDialog.OnSubmit += () =>
                    {
                        var request = new RegisterTemplateListRequest
                        {
                            TemplateList = new DataSetList
                            {
                                List = { _dataSet }
                            }
                        };
                        new DataSetRepository().RegisterListAsync(request).Forget();
                        uiManager.Destroy(_templateConfirmDialog);
                    };
                    _templateConfirmDialog.Open(_dataSet);
                };
                _dataSet = stateInputData.EditDataSet;
                _editTopPresenter.Init(_dataSet);
            }
            else
            {
                Debug.LogError($"Edit/RootState に不正なリクエストが送信されました。");
            }

        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
            if (_editTopPresenter != null) _editTopPresenter.Tick();
        }

        public override async UniTask End(CancellationToken token)
        {
            await base.End(token);
            Object.FindObjectOfType<UIManager>().Destroy(_editTopPresenter);
        }

        public override StateHistoryItem CreateHistory()
        {
            return new StateHistoryItem(typeof(RootState), new Request { EditDataSet = new Protobuf.DataSet() });
        }

        public class Request : StateChangeRequest
        {
            public Protobuf.DataSet EditDataSet;
        }

        public void Release()
        {
            OnClickedBackButton = null;
            OnClickedSubmitButton = null;
        }
    }
}