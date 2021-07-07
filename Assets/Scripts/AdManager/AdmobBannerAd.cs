using System;
using UnityEngine;
using GoogleMobileAds.Api;

public static class AdmobBannerAd
{
    private static bool isAdLoaded;
    private static BannerView bannerView;

    public static void Request()
    {
        isAdLoaded = false;
        bannerView = new BannerView(AdmobAdId.GetBannerId(),AdSize.Banner,AdPosition.Bottom);
        bannerView.OnAdLoaded += HandleOnAdLoaded;
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;

        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }

    public static void Show()
    {
        if(isAdLoaded) bannerView.Show();
    }

    public static void Hide()
    {
        if(bannerView != null) {bannerView.Hide(); bannerView.Destroy();}
    }

    public static bool IsLoaded()
    {
        return isAdLoaded;
    }

    public static void HandleOnAdLoaded(object sender, EventArgs args)
    {
        isAdLoaded = true;
        Debug.Log("Banner Ad is Loaded");
    }
    
    public static void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        isAdLoaded = false;
        Debug.Log("Banner Ad is failed to load : " + args.Message);
    }
}
