using System;
using UnityEngine;
using GoogleMobileAds.Api;

public static class AdmobRewardedAd
{
    private static bool isRewarded;
    private static RewardedAd rewardedAd;

    public static void Request()
    {
        rewardedAd = new RewardedAd(AdmobAdId.GetRewardedAdId());

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }

    public static void Show()
    {
        isRewarded = false;
        if(rewardedAd.IsLoaded()) rewardedAd.Show();
    }

    public static bool IsRewarded()
    {
        return isRewarded;
    }

    public static bool IsLoaded()
    {
        return rewardedAd.IsLoaded();
    }

    public static void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Reward Ad is loaded");
    }

    public static void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        Debug.Log( "Reward Ad is not loaded : " + args.Message);
    }

    public static void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("Reward Ad is closed by user");
        if(isRewarded == false) Debug.Log("Reward Cancelled");
    }

    public static void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        Debug.Log("Reward Ad is being showed.");
    }

    public static void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        Debug.Log( "Reward ad is being failed to be showed : " + args.Message);
    }

    public static void HandleUserEarnedReward(object sender, Reward args)
    {
        isRewarded = true;
        MonoBehaviour.print("User is rewarded.");
    }
}
