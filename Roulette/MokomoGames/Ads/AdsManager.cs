using System;
using System.Collections.Generic;
using System.Linq;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using MokomoGames.Ads.AppOpen;
using MokomoGames.Ads.Banner;
using UnityEngine;

namespace MokomoGames.Ads
{
    public class AdsManager : MonoBehaviour
    {
        private readonly List<ShowAppOpenAdProcess> _showAppOpenAdProcessesList = new List<ShowAppOpenAdProcess>();
        private readonly List<ShowBannerProcess> _showBannerProcesses = new List<ShowBannerProcess>();
        private static readonly List<string> TestDeviceIdList = new List<string>()
        {
        };

        public void Init(Action<InitializationStatus> initCompleteAction)
        {
            MobileAds.Initialize(initCompleteAction);
            MobileAds.SetRequestConfiguration(
                new RequestConfiguration.Builder()
                    .SetTestDeviceIds(TestDeviceIdList)
                    .build()
            );
        }

        public ShowAppOpenAdProcess CreateAppOpenAdProcess()
        {
            var process = new ShowAppOpenAdProcess();
            _showAppOpenAdProcessesList.Add(process);
            return process;
        }

        public ShowBannerProcess CreateShowBannerProcess()
        {
            var process = new ShowBannerProcess();
            _showBannerProcesses.Add(process);
            return process;
        }
    }
}