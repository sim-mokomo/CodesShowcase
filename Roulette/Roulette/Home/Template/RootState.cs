using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MokomoGames.StateMachine;
using MokomoGames.UI;
using Protobuf;
using Roulette.DataSet;
using Object = UnityEngine.Object;

namespace Roulette.Home.Template
{
    public class RootState : State
    {
        private TemplateTopPresenter _templateTop;
        public event Action<Protobuf.DataSet> OnClickedEditButton;
        public event Action OnClickedBackButton;

        public override async UniTask Begin(StateChangeRequest inputData, CancellationToken ct)
        {
            await base.Begin(inputData, ct);
            
            _templateTop = Object
                .FindObjectOfType<UIManager>()
                .Create<TemplateTopPresenter>(UIManager.CanvasOrder.Center);
            _templateTop.OnClickedEditButton += dataSet => { OnClickedEditButton?.Invoke(dataSet); };
            _templateTop.OnClickedBackButton += () => OnClickedBackButton?.Invoke();
            _templateTop.OnClickedTrashButton += async dataSet =>
            {
                var deleteRequest = new DeleteTempalteListRequest
                {
                    TemplateList = new DataSetList
                    {
                        List = { dataSet }
                    }
                };
                var dataSetRepository = new DataSetRepository();
                var deleteDataSetResponse = await dataSetRepository.DeleteTemplateListAsync(deleteRequest);
                _templateTop.Init(deleteDataSetResponse.TemplateList).Forget();
            };
            
            var dataSetRepository = new DataSetRepository();
            var loadDataSetListResponse = await dataSetRepository.LoadAsync(new LoadTemplateListRequest());
            _templateTop.Init(loadDataSetListResponse.TemplateList).Forget();
        }

        public override async UniTask End(CancellationToken token)
        {
            await base.End(token);
            Object.FindObjectOfType<UIManager>().Destroy(_templateTop);
        }

        public override StateHistoryItem CreateHistory()
        {
            return new StateHistoryItem(typeof(RootState), new Request());
        }

        public class Request : StateChangeRequest
        {
        }
    }
}