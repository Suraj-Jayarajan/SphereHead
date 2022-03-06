using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class _MangerController : MonoBehaviour {

    public int firstLevelIndex = 4;
    public int scoreMultiplier = 10;
    public int deathCountToShowAds = 5;

    int highScore, levelReached;
    float bestTime;

    Text bestTimeText, highScoreText;

    private AudioManager audioManager; //Audio Manager
    //private _AdManager adManager;
    Button muteBtn = null;
    Button sfxBtn = null; 
    Color lightGrey = Color.grey;
    

    void Awake()
    {
        
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        bestTime = PlayerPrefs.GetFloat("BestTime", 0);
        levelReached = PlayerPrefs.GetInt("LevelReached", 1);
        PlayerPrefs.SetInt("FirstLevelIndex", firstLevelIndex);
        PlayerPrefs.SetInt("LastAudioInstanceChangeScene", 0);
        PlayerPrefs.SetInt("CurrentSceneIndex", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("ScoreMultiplier", scoreMultiplier);
        
        PlayerPrefs.SetInt("LatestTransitionScene", 0);
        PlayerPrefs.SetInt("CurrentDeathCount", 0);
        

    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "_StartPage")
        {
            bestTimeText = GameObject.FindGameObjectWithTag("Txt_BestTime").GetComponent<Text>();
            bestTimeText.text = "Best Time : " + (int)bestTime / 60 + ":" + (bestTime % 60).ToString("f2").Replace('.',':');

            highScoreText = GameObject.FindGameObjectWithTag("Txt_HighScore").GetComponent<Text>();
            highScoreText.text = "High Score : " + highScore;

            PlayerPrefs.SetInt("PlaySFX", 1);
            PlayerPrefs.SetInt("PlayMusic", 1);
            PlayerPrefs.SetInt("DeathCountToShowAds", deathCountToShowAds);
        }

        lightGrey.a = 0.5f;

        // For Audio Manager
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager in the scene!!!");
        }

        if (GameObject.FindGameObjectWithTag("Btn_Mute") != null)
        {
            muteBtn = GameObject.FindGameObjectWithTag("Btn_Mute").GetComponent<Button>();
            int playMusic = PlayerPrefs.GetInt("PlayMusic");
            if (playMusic == 0)
            {
                muteBtn.image.color = lightGrey;
            }
        }
        if (GameObject.FindGameObjectWithTag("Btn_SfxBtn"))
        {
            sfxBtn = GameObject.FindGameObjectWithTag("Btn_SfxBtn").GetComponent<Button>();
            int playSfx = PlayerPrefs.GetInt("PlaySFX");
            if (playSfx == 0)
            {
                sfxBtn.image.color = lightGrey;
            }

        }

        
    }

	public void OnStartGamePress()
    {
        StartCoroutine(LoadLevel("_ChallengePage"));
    }

    public void OnRateUsPress()
    {
        Application.OpenURL("market://details?id=com.TeamCrytal.SphereHead");
    }

    public void OnCreditsButtonClick()
    {
        StartCoroutine(LoadLevel("_CreditsPage"));
    }

    public void OnMuteButtonClick()
    {
        
        if (PlayerPrefs.GetInt("PlayMusic") == 1)
        {
            PlayerPrefs.SetInt("PlayMusic", 0);
            muteBtn.image.color = lightGrey;
            audioManager.StopSound("Music");
            audioManager.StopSound("Music2");
           
        }
        else
        {
            PlayerPrefs.SetInt("PlayMusic", 1);
            muteBtn.image.color = Color.white;
            audioManager.PlaySound("Music");
            
        }
    }

    public void OnSFXButtonClick()
    {
        if (PlayerPrefs.GetInt("PlaySFX") == 1)
        {
            PlayerPrefs.SetInt("PlaySFX", 0);
            sfxBtn.image.color = lightGrey;
        }
        else
        {
            PlayerPrefs.SetInt("PlaySFX", 1);
            sfxBtn.image.color = Color.white;
            audioManager.PlaySound("KeySound");
        }

    }

    public void OnGoBackButtonClick()
    {
        StartCoroutine(LoadLevel("_ChallengePage"));
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void OnFacebookButtonClick()
    {
        Application.OpenURL("https://www.facebook.com/team.crytal");
    }

    public void OnTwitterButtonClick()
    {
        Application.OpenURL("https://twitter.com/team_crytal");
    }

    public void OnYoutubeButtonClick()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCgYSeliakNBnHaU0NvpByDw");
    }

    public void OnWebButtonClick()
    {
        Application.OpenURL("https://www.teamcrytal.com/spherehead");
    }
    public void OnInstagramButtonClick()
    {
        Application.OpenURL("https://www.instagram.com/teamcrytal");
    }





    // Co-routine
    IEnumerator LoadLevel(string sceneName, float waitSeconds = 0)
    {
        yield return new WaitForSeconds(waitSeconds);

        SceneManager.LoadScene(sceneName);
    }
}
