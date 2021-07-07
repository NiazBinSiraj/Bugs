using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerLimited : MonoBehaviour
{
    public static GameControllerLimited instance;
    void Awake()
    {
        instance = this;
    }
    
    public Text textScore;
    public Text timeText;
    public Text finalScoreText;
    public Text highScoreText;
    public GameObject gameOverPanel;
    public GameObject pauseButton;
    public GameObject pausePanel;
    public GameObject audioManager;
    public GameObject scorePanel;
    public GameObject timePanel;
    public GameObject blackHoles;

    public GameObject[] vBugPrefab;
    public Transform[] spawnLocation;

    private int prevPosInd;
    private float currentTime;
    private float lastSpawnTime;

    private float timeRemaining;

    private bool isGameOver;

    void Start()
    {
        if(!AdManager.instance.IsRewardedAdLoaded()) AdManager.instance.RequestRewaredAd();
        if(!AdManager.instance.IsInterstitialAdLoaded()) AdManager.instance.RequestFullscreenAd();
        
        scorePanel.SetActive(true);
        timePanel.SetActive(true);
        blackHoles.SetActive(true);
        highScoreText.gameObject.SetActive(true);
        
        highScoreText.text = "High Score: " +  GameStatistics.highScoreLimited.ToString();
        
        Time.timeScale = 1;
        isGameOver = false;
        GameData.InitializeGameData();
        lastSpawnTime = Time.time;
        Time.timeScale = 1;
        textScore.text = "0";
        timeRemaining = 150f;
        //AudioManager.instance.PlayLimitedBGM();
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            if (timeRemaining > 0) timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else if(!isGameOver)
        {
            CameraShaker.instance.StopShakingCamera();
            isGameOver = true;
            OnGameOver();
        }

        currentTime = Time.time;

        if(currentTime - lastSpawnTime > GameData.vBugLimitedSpawnDelay)
        {
            SpawnVBug();
            SpawnVBug();
            SpawnVBug();
            SpawnVBug();
            lastSpawnTime = currentTime;
        }
    }

    void SpawnVBug()
    {
        int posInd = Random.Range(0, spawnLocation.Length);
        while (posInd == prevPosInd)
        {
            posInd = Random.Range(0, spawnLocation.Length);
        }
        prevPosInd = posInd;
        int type = Random.Range(0,3);
        GameObject temp = Instantiate(vBugPrefab[type], spawnLocation[posInd].position, Quaternion.identity);
        temp.GetComponent<VBugController>().isLimited = true;
    }

    public void IncreaseScore()
    {
        GameData.score++;
        textScore.text = GameData.score.ToString();
    }

    public void OnClickPlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void OnClickPlay()
    {
        AudioManager.instance.PlayBGM();
        
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickPause()
    {
        AudioManager.instance.PauseBGM();
        
        pauseButton.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    void OnGameOver()
    {
        DestroyAllBugsAfterGameOver();
        
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
        pauseButton.SetActive(false);
        scorePanel.SetActive(false);
        timePanel.SetActive(false);
        blackHoles.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        gameOverPanel.SetActive(true);
        finalScoreText.text = ( (float) GameData.score * 0.172f).ToString("#.#") + "%" ;

        GameStatistics.totalScoreLimited += GameData.score;
        PlayerPrefs.SetInt("totalScoreLimited", GameStatistics.totalScoreLimited);

        if (GameStatistics.highScoreLimited < GameData.score)
        {
            GameStatistics.highScoreLimited = GameData.score;
            PlayerPrefs.SetInt("highScoreLimited", GameStatistics.highScoreLimited);
        }

        if (GameData.score >= 100)
        {
            GameStatistics.totalCenturyLimited++;
            PlayerPrefs.SetInt("totalCenturyLimited", GameStatistics.totalCenturyLimited);
        }
    }

    void DestroyAllBugsAfterGameOver()
    {
        GameObject [] vbugs = GameObject.FindGameObjectsWithTag("vBug");
        GameObject [] vbugs2 = GameObject.FindGameObjectsWithTag("vBug2");
        GameObject [] vbugs3 = GameObject.FindGameObjectsWithTag("vBug3");

        for(int i=0; i<vbugs.Length; i++) vbugs[i].SetActive(false);
        for(int i=0; i<vbugs2.Length; i++) vbugs2[i].SetActive(false);
        for(int i=0; i<vbugs3.Length; i++) vbugs3[i].SetActive(false);
    }
}
