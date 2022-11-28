using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MokomoGames.Ads;
using MokomoGames.Ads.AppOpen;
using MokomoGames.Ads.Banner;
using MokomoGames.Login;
using MokomoGames.Network;
using MokomoGames.StateMachine;
using UnityEngine;

namespace Roulette
{
    public class GameEntry : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private ApiRequestProcess _apiRequestProcess;
        
        private async void Start()
        {
            var adsManager = FindObjectOfType<AdsManager>();
            adsManager.Init(_ => { });
            await adsManager.CreateAppOpenAdProcess().Start();
            adsManager.CreateShowBannerProcess().Start();
            _apiRequestProcess = FindObjectOfType<ApiRequestProcess>();
            var ct = gameObject.GetCancellationTokenOnDestroy();
            _stateMachine = new StateMachine(new List<State>
            {
                new Home.RootState(),
                new RootState(() =>
                {
                    _stateMachine.ChangeStateAsync(typeof(Home.RootState), new Home.RootState.Request(), ct).Forget();
                })
            }, ct);
            _stateMachine.ChangeStateAsync(typeof(RootState), new RootState.Request(), ct).Forget();
        }

        private void Update()
        {
            _apiRequestProcess.Tick(Time.deltaTime);
            _stateMachine.Tick(Time.deltaTime);
        }
    }
}