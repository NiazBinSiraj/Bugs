using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject statisticsPanel;
    public GameObject soundOnButton;
    public GameObject soundOffButton;
    public GameObject musicOnButton;
    public GameObject musicOffButton;
    public GameObject loadingScreen;
    public GameObject startPanel;
    public GameObject gameStory;
    public GameObject gameRules;
    public GameObject nextButton;
    public Slider slider;
    public Text progressText;

    //Statistics
    [Header("Statistics")]
    public Text totalScoreClassic;
    public Text gamePlayedClasic;
    public Text highScoreClassic;
    public Text totalCenturyClassic;
    public Text averageVaccineClassic;
    public AudioSource audioSource;

    public Text totalScoreLimited;
    public Text gamePlayedLimited;
    public Text highScoreLimited;
    public Text totalCenturyLimited;
    public Text averageVaccineLimited;

    public Text totalScoreMastery;
    public Text gamePlayedMastery;
    public Text highScoreMastery;
    public Text totalCenturyMastery;
    public Text averageVaccineMastery;

    private int classicSceneIndex = 1;
    private int limitedSceneIndex = 2;
    private int masterySceneIndex = 3;

    private string[] statisticsKeyClassic = {
        "totalScoreClassic",
        "gamePlayedClasic",
        "highScoreClassic",
        "totalCenturyClassic"
    };

    private string[] statisticsKeyLimited = {
        "totalScoreLimited",
        "gamePlayedLimited",
        "highScoreLimited",
        "totalCenturyLimited"
    };

    private string[] statisticsKeyMastery = {
        "totalScoreMastery",
        "gamePlayedMastery",
        "highScoreMastery",
        "totalCenturyMastery"
    };

    void Start()
    {
        Debug.Log(Screen.width + " "+Screen.height);
        if(!GameData.gameStoryShowed) ShowStartPanel();
        
        GameData.InitializeGameData();
        LoadStatistics();

        if(GameData.music != 0)
        {
            musicOffButton.SetActive(false);
            musicOnButton.SetActive(true);
        }
        else
        {
            musicOffButton.SetActive(true);
            musicOnButton.SetActive(false);
        }

        if(GameData.sound != 0)
        {
            soundOffButton.SetActive(false);
            soundOnButton.SetActive(true);
        }
        else
        {
            soundOffButton.SetActive(true);
            soundOnButton.SetActive(false);
        }
    }

    void ShowStartPanel()
    {
        GameData.gameStoryShowed = true;
        startPanel.SetActive(true);
        gameStory.SetActive(true);
        gameRules.SetActive(false);
        nextButton.SetActive(true);
    }


    void Update()
    {
        audioSource.volume = GameData.music;
    }

    public void LoadScene(int sceneIndex)
    {
        if (sceneIndex == classicSceneIndex)
        {
            GameStatistics.gamePlayedClasic++;
            PlayerPrefs.SetInt("gamePlayedClasic", GameStatistics.gamePlayedClasic);
        }

        else if (sceneIndex == limitedSceneIndex)
        {
            GameStatistics.gamePlayedLimited++;
            PlayerPrefs.SetInt("gamePlayedLimited", GameStatistics.gamePlayedLimited);
        }

        else if (sceneIndex == masterySceneIndex)
        {
            GameStatistics.gamePlayedMastery++;
            PlayerPrefs.SetInt("gamePlayedMastery", GameStatistics.gamePlayedMastery);
        }

        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = (progress * 100f).ToString("#.#") + "%";

            yield return null;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OnClickNextButton()
    {
        if(gameStory.activeInHierarchy)
        {
            gameStory.SetActive(false);
            gameRules.SetActive(true);
        }
        else if(gameRules.activeInHierarchy)
        {
            gameRules.SetActive(false);
            nextButton.SetActive(false);
            startPanel.SetActive(false);
        }
    }

    public void OnClickSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void OnClickStatistics()
    {
        statisticsPanel.SetActive(true);
        UpdateStatistics();
    }

    public void BackToMenu(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void OnClickSoundON()
    {
        GameData.sound = 0;
        soundOffButton.SetActive(true);
        soundOnButton.SetActive(false);
        PlayerPrefs.SetFloat("sound", 0);
    }

    public void OnClickSoundOFF()
    {
        GameData.sound = 1f;
        soundOffButton.SetActive(false);
        soundOnButton.SetActive(true);
        PlayerPrefs.SetFloat("sound", 1f);
    }

    public void OnClickMusicON()
    {
        GameData.music = 0;
        musicOffButton.SetActive(true);
        musicOnButton.SetActive(false);
        PlayerPrefs.SetFloat("music", 0);
    }

    public void OnClickMusicOFF()
    {
        GameData.music = .7f;
        musicOffButton.SetActive(false);
        musicOnButton.SetActive(true);
        PlayerPrefs.SetFloat("music", .7f);
    }

    void UpdateStatistics()
    {
        totalScoreClassic.text = GameStatistics.totalScoreClassic.ToString();
        gamePlayedClasic.text = GameStatistics.gamePlayedClasic.ToString();
        highScoreClassic.text = GameStatistics.highScoreClassic.ToString();
        totalCenturyClassic.text = GameStatistics.totalCenturyClassic.ToString();
        averageVaccineClassic.text = ((float)GameStatistics.totalScoreClassic / GameStatistics.gamePlayedClasic).ToString("#.0");

        totalScoreLimited.text = GameStatistics.totalScoreLimited.ToString();
        gamePlayedLimited.text = GameStatistics.gamePlayedLimited.ToString();
        highScoreLimited.text = GameStatistics.highScoreLimited.ToString();
        totalCenturyLimited.text = GameStatistics.totalCenturyLimited.ToString();
        averageVaccineLimited.text = ((float)GameStatistics.totalScoreLimited / GameStatistics.gamePlayedLimited).ToString("#.0");

        totalScoreMastery.text = GameStatistics.totalScoreMastery.ToString();
        gamePlayedMastery.text = GameStatistics.gamePlayedMastery.ToString();
        highScoreMastery.text = GameStatistics.highScoreMastery.ToString();
        totalCenturyMastery.text = GameStatistics.totalCenturyMastery.ToString();
        averageVaccineMastery.text = ((float)GameStatistics.totalScoreMastery / GameStatistics.gamePlayedMastery).ToString("#.0");
    }

    void LoadStatistics()
    {
        //For Classic Mode
        if (PlayerPrefs.HasKey(statisticsKeyClassic[0])) GameStatistics.totalScoreClassic = PlayerPrefs.GetInt(statisticsKeyClassic[0]);
        else
        {
            GameStatistics.totalScoreClassic = 0;
            PlayerPrefs.SetInt(statisticsKeyClassic[0], 0);
        }

        if (PlayerPrefs.HasKey(statisticsKeyClassic[1])) GameStatistics.gamePlayedClasic = PlayerPrefs.GetInt(statisticsKeyClassic[1]);
        else
        {
            GameStatistics.gamePlayedClasic = 0;
            PlayerPrefs.SetInt(statisticsKeyClassic[1], 0);
        }

        if (PlayerPrefs.HasKey(statisticsKeyClassic[2])) GameStatistics.highScoreClassic = PlayerPrefs.GetInt(statisticsKeyClassic[2]);
        else
        {
            GameStatistics.highScoreClassic = 0;
            PlayerPrefs.SetInt(statisticsKeyClassic[2], 0);
        }

        if (PlayerPrefs.HasKey(statisticsKeyClassic[3])) GameStatistics.totalCenturyClassic = PlayerPrefs.GetInt(statisticsKeyClassic[3]);
        else
        {
            GameStatistics.totalCenturyClassic = 0;
            PlayerPrefs.SetInt(statisticsKeyClassic[3], 0);
        }

        //For Limited Mode
        if (PlayerPrefs.HasKey(statisticsKeyLimited[0])) GameStatistics.totalScoreLimited = PlayerPrefs.GetInt(statisticsKeyLimited[0]);
        else
        {
            GameStatistics.totalScoreLimited = 0;
            PlayerPrefs.SetInt(statisticsKeyLimited[0], 0);
        }

        if (PlayerPrefs.HasKey(statisticsKeyLimited[1])) GameStatistics.gamePlayedLimited = PlayerPrefs.GetInt(statisticsKeyLimited[1]);
        else
        {
            GameStatistics.gamePlayedLimited = 0;
            PlayerPrefs.SetInt(statisticsKeyLimited[1], 0);
        }

        if (PlayerPrefs.HasKey(statisticsKeyLimited[2])) GameStatistics.highScoreLimited = PlayerPrefs.GetInt(statisticsKeyLimited[2]);
        else
        {
            GameStatistics.highScoreLimited = 0;
            PlayerPrefs.SetInt(statisticsKeyLimited[2], 0);
        }

        if (PlayerPrefs.HasKey(statisticsKeyLimited[3])) GameStatistics.totalCenturyLimited = PlayerPrefs.GetInt(statisticsKeyLimited[3]);
        else
        {
            GameStatistics.totalCenturyLimited = 0;
            PlayerPrefs.SetInt(statisticsKeyLimited[3], 0);
        }

        //For Mastery Mode
        if (PlayerPrefs.HasKey(statisticsKeyMastery[0])) GameStatistics.totalScoreMastery = PlayerPrefs.GetInt(statisticsKeyMastery[0]);
        else
        {
            GameStatistics.totalScoreMastery = 0;
            PlayerPrefs.SetInt(statisticsKeyMastery[0], 0);
        }

        if (PlayerPrefs.HasKey(statisticsKeyMastery[1])) GameStatistics.gamePlayedMastery = PlayerPrefs.GetInt(statisticsKeyMastery[1]);
        else
        {
            GameStatistics.gamePlayedMastery = 0;
            PlayerPrefs.SetInt(statisticsKeyMastery[1], 0);
        }

        if (PlayerPrefs.HasKey(statisticsKeyMastery[2])) GameStatistics.highScoreMastery = PlayerPrefs.GetInt(statisticsKeyMastery[2]);
        else
        {
            GameStatistics.highScoreMastery = 0;
            PlayerPrefs.SetInt(statisticsKeyMastery[2], 0);
        }

        if (PlayerPrefs.HasKey(statisticsKeyMastery[3])) GameStatistics.totalCenturyMastery = PlayerPrefs.GetInt(statisticsKeyMastery[3]);
        else
        {
            GameStatistics.totalCenturyMastery = 0;
            PlayerPrefs.SetInt(statisticsKeyMastery[3], 0);
        }
    }
}
