using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using MokomoGames.StateMachine;

namespace Roulette.Home
{
    public class RootState : State
    {
        private StateMachine _stateMachine;

        public override async UniTask Begin(StateChangeRequest inputData, CancellationToken ct)
        {
            await base.Begin(inputData, ct);

            var topState = new Top.RootState();
            topState.OnClickedEditButton += async () =>
            {
                await _stateMachine.ChangeStateAsync(
                    typeof(Edit.RootState),
                    new Edit.RootState.Request
                    {
                        EditDataSet = new Protobuf.DataSet()
                    },
                    ct);
            };
            topState.OnClickedTemplateButton += async () =>
            {
                await _stateMachine.ChangeStateAsync(
                    typeof(Template.RootState),
                    new Template.RootState.Request(),
                    ct);
            };

            var editState = new Edit.RootState();
            editState.OnClickedBackButton += () => { _stateMachine.BackToLatestState(); };
            editState.OnClickedSubmitButton += async submitDataSet =>
            {
                await _stateMachine.ChangeStateAsync(
                    typeof(Top.RootState),
                    new Top.RootState.Request()
                    {
                        dataSetEntity = submitDataSet
                    },
                    ct);
            };

            var templateState = new Template.RootState();
            templateState.OnClickedBackButton += () => { _stateMachine.BackToLatestState(); };
            templateState.OnClickedEditButton += async dataSet =>
            {
                await _stateMachine.ChangeStateAsync(
                    typeof(Edit.RootState),
                    new Edit.RootState.Request
                    {
                        EditDataSet = dataSet
                    },
                    ct);
            };
            _stateMachine = new StateMachine(new List<State>
                {
                    topState,
                    editState,
                    templateState
                },
                ct);
            await _stateMachine.ChangeStateAsync(
                typeof(Top.RootState),
                new Top.RootState.Request
                {
                    dataSetEntity = new Protobuf.DataSet()
                },
                ct);
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
            _stateMachine.Tick(deltaTime);
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