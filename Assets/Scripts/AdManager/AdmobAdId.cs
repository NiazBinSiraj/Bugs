public static class AdmobAdId
{
    private static string bannerID = "ca-app-pub-3940256099942544/6300978111"; //Test Ad ID
    //private static string bannerID = ""; //Paste Your Banner Ad ID Here

    private static string interstitialAdID = "ca-app-pub-3940256099942544/8691691433"; //Test Ad ID
    //private static string interstitialAdID = ""; //Paste Your Interstitial Ad ID Here

    private static string rewardedAdID = "ca-app-pub-3940256099942544/5224354917"; //Test Ad ID
    //private static string rewardedAdID = ""; //Paste Your Rewarded Ad ID Here

    public static string GetBannerId() {return bannerID;}
    public static string GetInterstitialAdId() {return interstitialAdID;}
    public static string GetRewardedAdId() {return rewardedAdID;}
}
