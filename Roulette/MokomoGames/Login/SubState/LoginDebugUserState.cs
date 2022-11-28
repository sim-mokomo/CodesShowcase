#if DEV
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MokomoGames.Network;
using MokomoGames.Network.Debugger;
using MokomoGames.StateMachine;
using MokomoGames.UI;
using Object = UnityEngine.Object;

namespace MokomoGames.Login.SubState
{
    public class SelectLoginDebugUserState : State
    {
        public class Request : StateChangeRequest
        {
            
        }
        private MainDebugPresenter _mainDebugPresenter;
        private LoginDebugUserPresenter _loginDebugUserPresenter;
        public event Action OnLoggedIn;
        
        public override async UniTask Begin(StateChangeRequest inputData, CancellationToken token)
        {
            await base.Begin(inputData, token);

            var uiManager = Object.FindObjectOfType<UIManager>();
            _mainDebugPresenter = uiManager.Create<MainDebugPresenter>(UIManager.CanvasOrder.Front);
            _loginDebugUserPresenter = uiManager.Create<LoginDebugUserPresenter>(UIManager.CanvasOrder.Front);

            var loginUserDataRepository = new LoginUserDataRepository();
            var loginUserIdList = await loginUserDataRepository.LoadUserIdList();
            _loginDebugUserPresenter.Initialize(loginUserIdList);
            _loginDebugUserPresenter.onClickedAddUserButton += userId =>
            {
                loginUserIdList.idList.Add(userId);
                loginUserDataRepository.SaveUserIdList(loginUserIdList);
                _loginDebugUserPresenter.RefreshDebugUserList(loginUserIdList);
            };
            _loginDebugUserPresenter.onClickedLoginUserButton += async userId =>
            {
                _mainDebugPresenter.SetCurrentLoggedInUserId(userId);
                await new LoginService().CustomLoginAsync(userId);
                uiManager.Destroy(_loginDebugUserPresenter);
                OnLoggedIn?.Invoke();
            };
        }

        public override StateHistoryItem CreateHistory()
        {
            return null;
        }
    }
}
#endif