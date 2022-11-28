using System;
using GoogleMobileAds.Api;

namespace MokomoGames.Ads
{
    public class AdsReward
    {
        private readonly RewardedAd ads;
        public event Action<EventArgs> OnAdLoaded;
        public event Action<EventArgs> OnAdClosed;
        public AdsType AdsType => AdsType.Reward;

        public AdsReward(RewardedAd ads)
        {
            this.ads = ads;
            ads.OnAdClosed += (sender, args) => { OnAdClosed?.Invoke(args); };
            ads.OnAdLoaded += (sender, args) => { OnAdLoaded?.Invoke(args); };
        }
        
        public void Show()
        {
            ads.Show();
        }

        public void Load()
        {
            ads.LoadAd(AdsConfigService.CreateAdMobRequest().Build());
        }

        public void Destroy()
        {
            // NOTE: 明示的に破壊できない
        }
    }
}