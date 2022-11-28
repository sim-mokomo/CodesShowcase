using System;
using GoogleMobileAds.Api;

namespace MokomoGames.Ads.Banner
{
    public class ShowBannerProcess
    {
        private AdsBanner _adsBanner;

        public void Start()
        {
            var adsManager = UnityEngine.Object.FindObjectOfType<AdsManager>();
            _adsBanner = CreateBanner();
            // note: ロードが完了した時点でバナーが表示されるため、明示的に表示命令を出す必要がない。
            _adsBanner.Load();
        }
        
        private static AdsBanner CreateBanner()
        {
            var bannerView = new BannerView(
                AdsConfigService.GetCurrentPlatformUnitId(AdsType.Banner), 
                AdSize.SmartBanner,
                AdPosition.Bottom);
            return new AdsBanner(bannerView);
        }      
    }
}