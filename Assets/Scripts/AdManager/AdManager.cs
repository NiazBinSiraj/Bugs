// Attach this script with a gameobject of your menu scene

using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public static string initialized;
	private bool isInitialized = false;

	public static AdManager instance;
	private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        if(initialized == null) InitializeAd();
        else{
            isInitialized = true;
        }
    }

    private void InitializeAd()
    {
        MobileAds.Initialize(init => {
            isInitialized = true;
            Debug.Log("Admob is Initialized");
            initialized = "initialized";
            RequestFullscreenAd();
            RequestRewaredAd();
        });
    }
	
	
	
	// Call the following methods from your game scripts

    public bool IsInitialized()
	{
		return isInitialized;
	}
	
	public void RequestBanner()
    {
        if(!isInitialized) return;
        AdmobBannerAd.Request();
    }

    public void ShowBanner()
    {
        if(!isInitialized) return;
        AdmobBannerAd.Show();
    }

    public void HideBanner()
    {
        if(!isInitialized) return;
        AdmobBannerAd.Hide();
    }

    public void RequestFullscreenAd()
    {
        if(!isInitialized) return;
        AdmobInterstitialAd.Request();
    }

    public void ShowFullScreenAd()
    {
        if(!isInitialized) return;
        AdmobInterstitialAd.Show();
        RequestFullscreenAd();
    }

    public void RequestRewaredAd()
    {
        if(!isInitialized) return;
        AdmobRewardedAd.Request();
    }

    public void ShowRewardedAd()
    {
        if(!isInitialized) return;

        AdmobRewardedAd.Show();
        RequestRewaredAd();
    }

    public bool IsGainReward()
    {
        if(!isInitialized) return false;
        return AdmobRewardedAd.IsRewarded();
    }

    public bool IsRewardedAdLoaded()
    {
        if(!isInitialized) return false;
        return AdmobRewardedAd.IsLoaded();
    }

    public bool IsInterstitialAdLoaded()
    {
        if(!isInitialized) return false;
        return AdmobInterstitialAd.IsLoaded();
    }

    public bool IsBannerAdLoaded()
    {
        if(!isInitialized) return false;
        return AdmobBannerAd.IsLoaded();
    }
}
