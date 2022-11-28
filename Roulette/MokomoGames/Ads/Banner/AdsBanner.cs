using System;
using GoogleMobileAds.Api;

namespace MokomoGames.Ads.Banner
{
    public class AdsBanner : IAds
    {
        private readonly BannerView _bannerView;
        public event Action<EventArgs> OnAdLoaded;
        public event Action<EventArgs> OnAdClosed;
        public AdsType AdsType => AdsType.Banner;

        public AdsBanner(BannerView bannerView)
        {
            _bannerView = bannerView;
            _bannerView.OnAdClosed += (sender, args) => { OnAdClosed?.Invoke(args); };
            _bannerView.OnAdLoaded += (sender, args) => { OnAdLoaded?.Invoke(args); };
        }

        public void Show()
        {
            _bannerView.Show();
        }

        public void Load()
        {
            _bannerView.LoadAd(AdsConfigService.CreateAdMobRequest().Build());
        }

        public void Destroy()
        {
            _bannerView.Destroy();
        }
    }
}