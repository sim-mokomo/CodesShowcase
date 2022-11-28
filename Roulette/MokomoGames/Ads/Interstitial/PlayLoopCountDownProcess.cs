using GoogleMobileAds.Api;
using UnityEngine;

namespace MokomoGames.Ads.Interstitial
{
    public class PlayLoopCountDownProcess
    {
        private AdsInterstitial _ads;
        private Counter.Counter _counter;
        private readonly int _adsIntervalMax;
        private readonly int _adsIntervalMin;

        public PlayLoopCountDownProcess(int adsIntervalMin, int adsIntervalMax)
        {
            _adsIntervalMin = adsIntervalMin;
            _adsIntervalMax = adsIntervalMax;

            BuildInterstitialAd();
        }

        public void IncreaseCount()
        {
            _counter?.Increase(1);
        }

        private void BuildInterstitialAd()
        {
            _ads = new AdsInterstitial(new InterstitialAd(AdsConfigService.GetCurrentPlatformUnitId(AdsType.Interstitial)));
            _ads!.OnAdClosed += args => { BuildInterstitialAd(); };

            _counter = new Counter.Counter(0, Random.Range(_adsIntervalMin, _adsIntervalMax));
            _counter.OnEnd += () => { _ads.Show(); };
        }
    }
}