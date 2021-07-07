using UnityEngine;

public static class GameData
{
    public static float sound {get; set;}
    public static float music {get; set;}
    
    public static int score {get; set;}

    public static float bugSpeed {get; set;}
    public static float vBugSpeed {get; set;}

    public static float bugSpawnDelay {get; set;}
    public static float vBugSpawnDelay {get; set;}

    public static float resetBugSlowerDelay {get; set;}
    public static float resetDestroyAllDelay {get; set;}

    public static float bugSlowerDuration {get; set;}

    public static float bugSpeedIncrementRate {get; set;}
    public static float bugSpeedIncrementDelay {get; set;}

    public static float maximumBugSpeed {get; set;}

    public static float vBugLimitedSpeed {get; set;}
    public static float vBugLimitedSpawnDelay {get; set;}
    public static float dTapBugLimitedSpawnDelay {get; set;}

    public static int gameOverCount {get; set;} = 0;
    public static bool gameStoryShowed {get; set;} = false;

    public static void InitializeGameData()
    {
        score = 0;
        bugSpeed = 3;
        vBugSpeed = 3;
        bugSpawnDelay = 14;
        vBugSpawnDelay = 2;
        resetBugSlowerDelay = 20;
        resetDestroyAllDelay = 20;
        bugSlowerDuration = 5;
        bugSpeedIncrementRate = 0.2f;
        bugSpeedIncrementDelay = 5;
        maximumBugSpeed = 10;
        vBugLimitedSpeed = 12;
        vBugLimitedSpawnDelay = 1.3f;
        dTapBugLimitedSpawnDelay = 2.5f;
        //gameOverCount = 0;

        if(PlayerPrefs.HasKey("music"))
        {
            music = PlayerPrefs.GetFloat("music");
        }
        else
        {
            music = .7f;
            PlayerPrefs.SetFloat("music", .7f);
        }

        if(PlayerPrefs.HasKey("sound"))
        {
            sound = PlayerPrefs.GetFloat("sound");
        }
        else
        {
            sound = 1f;
            PlayerPrefs.SetFloat("sound", 1f);
        }
    }
}
