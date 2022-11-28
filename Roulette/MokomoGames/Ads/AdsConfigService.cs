using System.Collections.Generic;
using System.Linq;
using GoogleMobileAds.Api;
using UnityEngine;

namespace MokomoGames.Ads
{
    public static class AdsConfigService
    {
        private class AdsConfig
        {
            public RuntimePlatform RuntimePlatform;
            public List<UnitIdRecord> UnitIdTable;
        }
        
        private class UnitIdRecord
        {
            public AdsType AdsType;
            public string UnitID;
            public string UnitTestId;
        }

        private static readonly List<AdsConfig> AdsConfigTable = new List<AdsConfig>()
        {
            new AdsConfig()
            {
                RuntimePlatform = RuntimePlatform.IPhonePlayer,
                UnitIdTable = new List<UnitIdRecord>
                {
                    new UnitIdRecord()
                    {
                        AdsType = AdsType.Banner,
                        UnitID = string.Empty,
                        UnitTestId ="ca-app-pub-3940256099942544/2934735716"
                    },
                    new UnitIdRecord()
                    {
                        AdsType = AdsType.Interstitial,
                        UnitID = string.Empty,
                        UnitTestId ="ca-app-pub-3940256099942544/4411468910"
                    },
                    new UnitIdRecord()
                    {
                        AdsType = AdsType.Reward,
                        UnitID = string.Empty,
                        UnitTestId ="ca-app-pub-3940256099942544/1712485313"
                    },
                    new UnitIdRecord()
                    {
                        AdsType = AdsType.AppOpen,
                        UnitID = string.Empty,
                        UnitTestId ="ca-app-pub-3940256099942544/5662855259"
                    },
                }
            },
            
            new AdsConfig()
            {
                RuntimePlatform = RuntimePlatform.Android,
                UnitIdTable = new List<UnitIdRecord>
                {
                    new UnitIdRecord()
                    {
                        AdsType = AdsType.Banner,
                        UnitID = string.Empty,
                        UnitTestId ="ca-app-pub-3940256099942544/6300978111"
                    },
                    new UnitIdRecord()
                    {
                        AdsType = AdsType.Interstitial,
                        UnitID = string.Empty,
                        UnitTestId ="ca-app-pub-3940256099942544/1033173712"
                    },
                    new UnitIdRecord()
                    {
                        AdsType = AdsType.Reward,
                        UnitID = string.Empty,
                        UnitTestId ="ca-app-pub-3940256099942544/5224354917"
                    },
                    new UnitIdRecord()
                    {
                        AdsType = AdsType.AppOpen,
                        UnitID = string.Empty,
                        UnitTestId ="ca-app-pub-3940256099942544/3419835294"
                    },
                }
            }
        };
        
        public static AdRequest.Builder CreateAdMobRequest()
        {
            var request = new AdRequest.Builder();
            return request;
        }

        public static string GetCurrentPlatformUnitId(AdsType adsType) =>
            FindUnitIDByPlatform(CalcCurrentAdsPlatform(), adsType);

        private static RuntimePlatform CalcCurrentAdsPlatform()
        {
            var platform = Application.platform;
            if (Application.isEditor)
            {
                return platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.LinuxEditor
                    ? RuntimePlatform.Android
                    : RuntimePlatform.IPhonePlayer;
            }

            return platform;
        }

        private static string FindUnitIDByPlatform(RuntimePlatform platform, AdsType adsType)
        {
            var record = AdsConfigTable
                .First(x => x.RuntimePlatform == platform).UnitIdTable
                .First(x => x.AdsType == adsType);
            if (record.UnitID == string.Empty) Debug.LogError($"{platform} {adsType} の広告が設定されていません");
            var isTest = Application.isEditor || Debug.isDebugBuild;
            return isTest ? record.UnitTestId : record.UnitID;
        }
    }
}