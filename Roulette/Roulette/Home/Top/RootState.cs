using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MokomoGames.Audio;
using MokomoGames.GameConfig;
using MokomoGames.Network;
using MokomoGames.StateMachine;
using MokomoGames.UI;
using Protobuf;
using Roulette.GameConfig;
using Roulette.Licence;
using Object = UnityEngine.Object;

namespace Roulette.Home.Top
{
    public class RootState : State
    {
        private ConfigsPresenter _configsPresenter;
        private GameConfigManager _gameConfigManager;
        private LicencePresenter _licencePresenter;
        private TopStatePresenter _topStatePresenter;
        private UIManager _uiManager;
        public event Action OnClickedEditButton;
        public event Action OnClickedTemplateButton;

        public override async UniTask Begin(StateChangeRequest inputData, CancellationToken ct)
        {
            await base.Begin(inputData, ct);

            _uiManager = Object.FindObjectOfType<UIManager>();
            if (inputData is Request request)
            {
                _topStatePresenter = _uiManager.Create<TopStatePresenter>(UIManager.CanvasOrder.Center);
                _topStatePresenter.Init(request.dataSetEntity);
                _topStatePresenter.OnClickedEditButton += OnClickedEditButton;
                _topStatePresenter.OnClickedTemplateButton += OnClickedTemplateButton;
                _topStatePresenter.OnClickedConfigButton += () =>
                {
                    _gameConfigManager = Object.FindObjectOfType<GameConfigManager>();
                    _configsPresenter = _uiManager.Create<ConfigsPresenter>(UIManager.CanvasOrder.Front);
                    var onUpdateGameConfig = new Action<MokomoGames.GameConfig.GameConfig>(config =>
                    {
                        _configsPresenter.OnUpdatedGameConfig(config);
                    });
                    _configsPresenter.OnClickedMuteButton += () =>
                    {
                        _gameConfigManager.OnUpdatedGameConfig += onUpdateGameConfig;
                        _gameConfigManager.Config.ToggleMute();
                        _gameConfigManager.Save();
                    };
                    _configsPresenter.OnClickedLicenseButton += () =>
                    {
                        _licencePresenter = _uiManager.Create<LicencePresenter>(UIManager.CanvasOrder.Front);
                        _licencePresenter.Initialize(Object.FindObjectOfType<LicenceRepository>().Load());
                        _licencePresenter.OnClosed += () =>
                        {
                            _uiManager.Destroy(_licencePresenter);
                        };
                        _licencePresenter.Open();
                    };
                    _configsPresenter.OnClosed += () =>
                    {
                        _uiManager.Destroy(_configsPresenter);
                        _gameConfigManager.OnUpdatedGameConfig -= onUpdateGameConfig;
                    };
                    _configsPresenter.Open();
                };
            }
        }

        public override async UniTask End(CancellationToken token)
        {
            await base.End(token);
            new AudioService().StopAll(Object.FindObjectOfType<AudioManager>());
            Object.FindObjectOfType<UIManager>().Destroy(_topStatePresenter);
        }

        public override StateHistoryItem CreateHistory()
        {
            return new StateHistoryItem(typeof(RootState), new Request
            {
                dataSetEntity = new Protobuf.DataSet()
            });
        }

        public class Request : StateChangeRequest
        {
            public Protobuf.DataSet dataSetEntity;
        }
    }
}