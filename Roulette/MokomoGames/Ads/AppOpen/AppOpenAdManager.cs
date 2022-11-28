using GoogleMobileAds.Api;
using UnityEngine;

namespace MokomoGames.Ads.AppOpen
{
    public class AppOpenAdManager
    {
        private AppOpenAd ad;
        private bool isShowingAd = false;
        public bool IsAdAvailable => ad != null;

        public void LoadAd()
        {
            // Load an app open ad for portrait orientation
            AppOpenAd.LoadAd(
                AdsConfigService.GetCurrentPlatformUnitId(AdsType.AppOpen),
                ScreenOrientation.Portrait,
                AdsConfigService.CreateAdMobRequest().Build(),
                (appOpenAd, error) =>
                {
                    if (error != null)
                    {
                        // Handle the error.
                        Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
                        return;
                    }

                    // App open ad is loaded.
                    ad = appOpenAd;
                }
            );
        }
        
        public void ShowAdIfAvailable()
        {
            if (!IsAdAvailable || isShowingAd)
            {
                return;
            }

            ad.OnAdDidDismissFullScreenContent += (sender, args) =>
            {
                Debug.Log("Closed app open ad");
                // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
                ad = null;
                isShowingAd = false;
                LoadAd();
            };
            ad.OnAdFailedToPresentFullScreenContent += (sender, args) =>
            {
                Debug.LogFormat("Failed to present the ad (reason: {0})", args.AdError.GetMessage());
                // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
                ad = null;
                LoadAd();
            };
            ad.OnAdDidPresentFullScreenContent += (sender, args) =>
            {
                Debug.Log("Displayed app open ad");
                isShowingAd = true;
            };
            ad.OnAdDidRecordImpression += (sender, args) =>
            {
                Debug.Log("Recorded ad impression");
            };
            ad.OnPaidEvent += (sender, args) =>
            {
                Debug.LogFormat("Received paid event. (currency: {0}, value: {1}", 
                    args.AdValue.CurrencyCode,
                    args.AdValue.Value);
            };
            ad.Show();
        }
    }
}