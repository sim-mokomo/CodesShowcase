using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace MokomoGames.StateMachine
{
    public abstract class State 
    {
        private StateChangeRequest _stateInputData;
        public bool IsInitialized { get; private set; }
        private DateTime? _timeWhenBackground = null;
        public event Action OnCompletedEndAfter;

        public TStateChangeRequest GetRequest<TStateChangeRequest>() where TStateChangeRequest : StateChangeRequest
        {
            return _stateInputData as TStateChangeRequest;
        }

        public virtual async UniTask Begin(StateChangeRequest inputData, CancellationToken ct)
        {
            _stateInputData = inputData;
        }

        public virtual async UniTask BeginAfter(CancellationToken token)
        {
            IsInitialized = true;
        }

        public virtual void Tick(float deltaTime)
        {
        }

        public virtual async UniTask End(CancellationToken token)
        {
            IsInitialized = false;
        }

        public virtual async UniTask EndAfter(CancellationToken token)
        {
            OnCompletedEndAfter?.Invoke();
        }

        public abstract StateHistoryItem CreateHistory();

        public virtual void OnAppFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                TimeSpan diffTime = default;
                if (_timeWhenBackground != null)
                {
                    diffTime = DateTime.Now - _timeWhenBackground.Value;
                }

                _timeWhenBackground = null;
                OnForeground(diffTime);
            }
            else
            {
                _timeWhenBackground = DateTime.Now;
                OnBackground();
            }
        }
        
        public virtual void OnForeground(TimeSpan diffTimeFromBackground)
        {
            
        }

        public virtual void OnBackground()
        {
        }
    }
}