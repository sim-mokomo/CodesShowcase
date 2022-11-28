using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MokomoGames.StateMachine
{
    public class StateMachine
    {
        private const int HistoryCapacity = 10;
        private readonly List<StateHistoryItem> _stateHistoryList = new List<StateHistoryItem>();
        private readonly List<State> _stateMap;
        private readonly CancellationToken _cancellationToken;
        public State CurrentState { get; private set; }

        public StateMachine(List<State> stateMap, CancellationToken token)
        {
            _stateMap = stateMap;
            _cancellationToken = token;
        }

        public void Tick(float deltaTime)
        {
            if (CurrentState == null)
            {
                return;
            }
            
            if (!CurrentState.IsInitialized)
            {
                return;
            }
            
            CurrentState?.Tick(deltaTime);
        }

        public UniTask BackToLatestState()
        {
            var latestHistory = _stateHistoryList.LastOrDefault();
            if (latestHistory == null)
            {
                return default;
            }
                    
            _stateHistoryList.Remove(latestHistory);
            return ChangeStateAsync(latestHistory.StateType, latestHistory.ChangeRequest, _cancellationToken, true);
        }

        public async UniTask ChangeStateAsync(System.Type next, StateChangeRequest stateChangeRequest, CancellationToken token, bool isBack = false)
        {
            if (CurrentState != null)
            {
                // note: 戻る場合も履歴を積むとループになるのでそれを防ぐ
                if (!isBack)
                {
                    var history = CurrentState.CreateHistory();
                    if (history != null)
                    {
                        _stateHistoryList.Add(history);
                        if (_stateHistoryList.Count > HistoryCapacity) _stateHistoryList.RemoveAt(0);
                    }
                }

                await CurrentState.End(_cancellationToken);
                await CurrentState.EndAfter(_cancellationToken);
            }

            Debug.Log($"{next}に遷移");
            CurrentState = _stateMap.First(x => x.GetType() == next);
            await CurrentState.Begin(stateChangeRequest, _cancellationToken);
            await CurrentState.BeginAfter(_cancellationToken);
        }
    }
}