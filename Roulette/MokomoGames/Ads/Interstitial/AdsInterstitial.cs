using System;
using GoogleMobileAds.Api;

namespace MokomoGames.Ads.Interstitial
{
    public class AdsInterstitial : IAds
    {
        private readonly InterstitialAd _interstitialAd;
        public event Action<EventArgs> OnAdLoaded;
        public event Action<EventArgs> OnAdClosed;
        public AdsType AdsType => AdsType.Interstitial;

        public AdsInterstitial(InterstitialAd interstitialAd)
        {
            _interstitialAd = interstitialAd;
            _interstitialAd.OnAdClosed += (sender, args) => { OnAdClosed?.Invoke(args); };
            _interstitialAd.OnAdLoaded += (sender, args) => { OnAdLoaded?.Invoke(args); };
        }

        public void Show()
        {
            _interstitialAd.Show();
        }

        public void Load()
        {
            _interstitialAd.LoadAd(AdsConfigService.CreateAdMobRequest().Build());
        }

        public void Destroy()
        {
            _interstitialAd.Destroy();
        }
    }
}