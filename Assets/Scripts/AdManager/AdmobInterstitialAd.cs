using System;
using UnityEngine;
using GoogleMobileAds.Api;

public static class AdmobInterstitialAd
{
    private static InterstitialAd interstitialAd;

    public static void Request()
    {
        interstitialAd = new InterstitialAd(AdmobAdId.GetInterstitialAdId());

        interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        interstitialAd.OnAdOpening += HandleOnAdOpened;

        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);
    }

    public static void Show()
    {
        if(interstitialAd.IsLoaded()) interstitialAd.Show();
    }

    public static bool IsLoaded()
    {
        return interstitialAd.IsLoaded();
    }

    public static void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Interstitial Ad is loaded.");
    }

    public static void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Interstitial Ad is not loaded : " + args.Message);
    }

    public static void HandleOnAdOpened(object sender, EventArgs args)
    {
        Debug.Log("Interstitial Ad is being showed");
    }
}
