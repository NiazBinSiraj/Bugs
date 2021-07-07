using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{   
    public Text textScore;
    public Text finalScoreText;
    public Text highScoreText;
    public GameObject gameOverPanel;
    public GameObject pauseButton;
    public GameObject pausePanel;
    public GameObject[] hearts;
    public GameObject scorePanel;
    public GameObject blackHoles;
    public GameObject rewardAdPanel;
    public GameObject gotRewardPanel;

    public Button BugSpeedSlowerButton;
    public Button DestroyAllBugsButton;
    private Image DestroyAllBugsButtonImage;
    private Image BugSpeedSlowerButtonImage;

    public int lives = 5;
    private float currentBugSpeed;
    private float currentVBugSpeed;
    private float currentTime;
    private float lastBugSpeedIncrementTime;
    private Spwan_Bugs spwan_Bugs;

    private int watchedAd = 0;

    void Start()
    {
        watchedAd = 0;
        if(!AdManager.instance.IsRewardedAdLoaded()) AdManager.instance.RequestRewaredAd();
        if(!AdManager.instance.IsInterstitialAdLoaded()) AdManager.instance.RequestFullscreenAd();
        
        blackHoles.SetActive(true);
        scorePanel.SetActive(true);
        highScoreText.gameObject.SetActive(true);
        if(SceneManager.GetActiveScene().buildIndex == 1) highScoreText.text = "High Score: " +  GameStatistics.highScoreClassic.ToString();
        else if(SceneManager.GetActiveScene().buildIndex == 3) highScoreText.text = "High Score: " +  GameStatistics.highScoreMastery.ToString();
        
        Time.timeScale = 1;
        
        GameData.InitializeGameData();
        
        lastBugSpeedIncrementTime = Time.time;

        textScore.text = GameData.score.ToString();

        DestroyAllBugsButtonImage = DestroyAllBugsButton.GetComponent<Image>();
        BugSpeedSlowerButtonImage = BugSpeedSlowerButton.GetComponent<Image>();

        DestroyAllBugsButtonImage.color = Color.green;
        BugSpeedSlowerButtonImage.color = Color.green;

        spwan_Bugs = GetComponent<Spwan_Bugs>();

        //if(SceneManager.GetActiveScene().buildIndex == 1) AudioManager.instance.PlayClassicBGM();
        //else if(SceneManager.GetActiveScene().buildIndex == 3) AudioManager.instance.PlayMasteryBGM();
    }

    void Update()
    {
        currentTime = Time.time;

        if(currentTime - lastBugSpeedIncrementTime >= GameData.bugSpeedIncrementDelay && BugSpeedSlowerButton.interactable)
        {
            if(GameData.bugSpeed < GameData.maximumBugSpeed) GameData.bugSpeed += GameData.bugSpeedIncrementRate;
            if(GameData.vBugSpeed < GameData.maximumBugSpeed) GameData.vBugSpeed += GameData.bugSpeedIncrementRate;

            lastBugSpeedIncrementTime = currentTime;
        }
    }

    public void IncreaseScore()
    {
        GameData.score++;
        textScore.text = GameData.score.ToString();
    }

    public void IncreaseLife()
    {
        if(lives == 5) return;
        hearts[lives].SetActive(true);
        lives++;
    }

    public void DecreaseLife()
    {
        hearts[lives-1].SetActive(false);
        lives--;
        if (lives == 0)
        {
            CameraShaker.instance.StopShakingCamera();
            if(watchedAd < 2 && AdManager.instance.IsRewardedAdLoaded())
            {
                WantToWatchAd();
            }
            else OnGameOver();
        }
        else CameraShaker.instance.StartShakingCamera();
    }

    private void WantToWatchAd()
    {
        Time.timeScale = 0;
        rewardAdPanel.SetActive(true);
    }

    public void WatchAd()
    {
        AdManager.instance.ShowRewardedAd();
        watchedAd++;
        rewardAdPanel.SetActive(false);
        gotRewardPanel.SetActive(true);
    }

    public void CancelAd()
    {
        rewardAdPanel.SetActive(false);
        OnGameOver();
    }

    public void CheckRewardStatus()
    {
        if(!AdManager.instance.IsGainReward())
        {
            watchedAd--;
            OnGameOver();
        }
        else
        {
            if(!AdManager.instance.IsRewardedAdLoaded()) AdManager.instance.RequestRewaredAd();
            
            IncreaseLife();
            Time.timeScale = 1;
        }
        gotRewardPanel.SetActive(false);
    }

    public void OnClickBugSpeedSlowerButton()
    {   
        BugSpeedSlowerButtonImage.color = Color.yellow;

        BugSpeedSlowerButton.interactable = false;
        
        currentBugSpeed = GameData.bugSpeed;
        currentVBugSpeed = GameData.vBugSpeed;

        GameData.bugSpeed = GameData.bugSpeed / 2f;
        GameData.vBugSpeed = GameData.vBugSpeed / 2f;

        Invoke("ResetBugSpeed", GameData.bugSlowerDuration);
        Invoke("ResetBugSpeedSlowerPower", GameData.resetBugSlowerDelay);
    }

    public void OnClickDestroyAllBugsButton()
    {
        DestroyAllBugsButtonImage.color = Color.red;
        
        DestroyAllBugsButton.interactable = false;
        CameraShaker.instance.StartShakingCamera();
        GameObject[] vBugs = GameObject.FindGameObjectsWithTag("vBug");
        GameObject[] vBugs2 = GameObject.FindGameObjectsWithTag("vBug2");
        GameObject[] vBugs3 = GameObject.FindGameObjectsWithTag("vBug3");

        SpriteRenderer sr = new SpriteRenderer();
        
        for(int i=0; i<vBugs.Length; i++)
        {
            sr = vBugs[i].GetComponent<SpriteRenderer>();
            if(vBugs[i].activeSelf && sr.isVisible) vBugs[i].GetComponent<VBugController>().DestroyByBomb();
        }

        for(int i=0; i<vBugs2.Length; i++)
        {
            sr = vBugs2[i].GetComponent<SpriteRenderer>();
            if(vBugs2[i].activeSelf && sr.isVisible) vBugs2[i].GetComponent<VBugController>().DestroyByBomb();
        }

        for(int i=0; i<vBugs3.Length; i++)
        {
            sr = vBugs3[i].GetComponent<SpriteRenderer>();
            if(vBugs3[i].activeSelf && sr.isVisible) vBugs3[i].GetComponent<VBugController>().DestroyByBomb();
        }

        Invoke("ResetDestroyAllPower", GameData.resetDestroyAllDelay);
    }

    void ResetBugSpeed()
    {
        BugSpeedSlowerButtonImage.color = Color.red;

        GameData.bugSpeed = currentBugSpeed;
        GameData.vBugSpeed = currentVBugSpeed;
    }

    void ResetBugSpeedSlowerPower()
    {
        BugSpeedSlowerButtonImage.color = Color.green;
        BugSpeedSlowerButton.interactable = true;
    }

    void ResetDestroyAllPower()
    {
        DestroyAllBugsButtonImage.color = Color.green;
        DestroyAllBugsButton.interactable = true;
    }

    void OnGameOver()
    {
        if(GameData.gameOverCount == 5)
        {
            GameData.gameOverCount = 0;
            AdManager.instance.ShowFullScreenAd();
        }
        else
        {
            GameData.gameOverCount++;
        }
        
        Time.timeScale = 0;
        scorePanel.SetActive(false);
        blackHoles.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        pauseButton.SetActive(false);
        BugSpeedSlowerButton.gameObject.SetActive(false);
        DestroyAllBugsButton.gameObject.SetActive(false);

        GameObject [] bugs = GameObject.FindGameObjectsWithTag("bug");
        GameObject [] bugs2 = GameObject.FindGameObjectsWithTag("bug2");
        GameObject [] bugs3 = GameObject.FindGameObjectsWithTag("bug3");
        GameObject [] vbugs = GameObject.FindGameObjectsWithTag("vBug");
        GameObject [] vbugs2 = GameObject.FindGameObjectsWithTag("vBug2");
        GameObject [] vbugs3 = GameObject.FindGameObjectsWithTag("vBug3");
        GameObject [] lbugs = GameObject.FindGameObjectsWithTag("LBug");

        for(int i=0; i<bugs.Length; i++) bugs[i].SetActive(false);
        for(int i=0; i<bugs2.Length; i++) bugs2[i].SetActive(false);
        for(int i=0; i<bugs3.Length; i++) bugs3[i].SetActive(false);
        for(int i=0; i<vbugs.Length; i++) vbugs[i].SetActive(false);
        for(int i=0; i<vbugs2.Length; i++) vbugs2[i].SetActive(false);
        for(int i=0; i<vbugs3.Length; i++) vbugs3[i].SetActive(false);
        for(int i=0; i<lbugs.Length; i++) lbugs[i].SetActive(false);


        gameOverPanel.SetActive(true);
        finalScoreText.text = GameData.score.ToString();

        if(SceneManager.GetActiveScene().name == "Classic")
        {
            GameStatistics.totalScoreClassic+=GameData.score;
            PlayerPrefs.SetInt("totalScoreClassic", GameStatistics.totalScoreClassic);

            if(GameStatistics.highScoreClassic < GameData.score)
            {
                GameStatistics.highScoreClassic = GameData.score;
                PlayerPrefs.SetInt("highScoreClassic", GameStatistics.highScoreClassic);
            }

            if(GameData.score >= 100)
            {
                GameStatistics.totalCenturyClassic++;
                PlayerPrefs.SetInt("totalCenturyClassic", GameStatistics.totalCenturyClassic);
            }
        }
        else if(SceneManager.GetActiveScene().name == "Mastery")
        {
            GameStatistics.totalScoreMastery+=GameData.score;
            PlayerPrefs.SetInt("totalScoreMastery", GameStatistics.totalScoreMastery);

            if(GameStatistics.highScoreMastery < GameData.score)
            {
                GameStatistics.highScoreMastery = GameData.score;
                PlayerPrefs.SetInt("highScoreMastery", GameStatistics.highScoreMastery);
            }

            if(GameData.score >= 100)
            {
                GameStatistics.totalCenturyMastery++;
                PlayerPrefs.SetInt("totalCenturyMastery", GameStatistics.totalCenturyMastery);
            }
        }
    }

    public void OnClickPlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickPlay()
    {
        AudioManager.instance.PlayBGM();
        
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
        DestroyAllBugsButton.gameObject.SetActive(true);
        BugSpeedSlowerButton.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    public void OnClickPause()
    {
        AudioManager.instance.PauseBGM();
        
        pauseButton.SetActive(false);
        pausePanel.SetActive(true);
        DestroyAllBugsButton.gameObject.SetActive(false);
        BugSpeedSlowerButton.gameObject.SetActive(false);
        Time.timeScale = 0;
    }
}
