using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSupervisor : MonoBehaviour
{
    public bool setTransition = false;
    public int timer = 70;
    public int coinsCollected = 0;
    public int playerHealth = 100;
    public ParticleSystem playerDeathEffect, doorParticleEffect;
    public float playerDeathLifetime = 2;
    public bool startTimer;
    public int si = 0;


    private Text timerText;
    private float startTime;
    private float endTime;
    private float timeLeft;
    private bool playerIsDead = false;
    private bool gotAllKeys = false;
    private GameObject door;
    private int noOfKeys;
    private int noOfKeysAqquired = 0;
    private int scoreMultiplier = 10;
    private Renderer doorRenderer;
    private AudioManager audioManager; //Audio Manager
    private _AdManager adManager;
    private float originalKeyPitch = 1f;
    private float keyPitch = 1f;
    private bool countdownNotStarted = true;
    private bool setTimer = true;


    // Use this for initialization
    void Start()
    {
        si = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("CurrentSceneIndex", SceneManager.GetActiveScene().buildIndex);

        int currentLevel = SceneManager.GetActiveScene().buildIndex + 1 - PlayerPrefs.GetInt("FirstLevelIndex");
        if (currentLevel > PlayerPrefs.GetInt("LevelReached"))
        {
            PlayerPrefs.SetInt("LevelReached", currentLevel);
        }


        timerText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<Text>();
        timerText.text = ((int)timer / 60 + ":" + (timer % 60).ToString("f2")).Replace('.', ':');

        //For keys
        door = GameObject.FindGameObjectWithTag("Door");
        doorRenderer = door.GetComponent<Renderer>();
        doorRenderer.enabled = true;
        noOfKeys = GameObject.FindGameObjectsWithTag("Key").Length;


        //For Audio Manager
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager in the scene!!!");
        }

        //For Audio Manager
        adManager = _AdManager.instance;
        if (adManager == null)
        {
            Debug.LogError("No AdManager in the scene!!!");
        }

        originalKeyPitch = keyPitch = audioManager.GetPitch("KeySound");
    }


    // Update is called once per frame
    void Update()
    {
        //Death Controller
        if (playerHealth <= 0)
        {
            if (!playerIsDead)
            {
                KillPlayer();
                playerIsDead = true;
            }
        }

        if (startTimer)
        {
            if (setTimer)
            {
                startTime = Time.time;
                endTime = Time.time + timer;
                setTimer = false;
            }


            UpdateTimer();
        }
       
        // Key Check
        UpdateKeys();
        if (noOfKeysAqquired == noOfKeys)
        {
            if (!gotAllKeys)
            {
                doorRenderer.sharedMaterial = Resources.Load("Materials/White", typeof(Material)) as Material;
                Instantiate(doorParticleEffect.gameObject, door.transform);
                //Instantiate(doorParticleEffect.gameObject, door.transform.position, Quaternion.FromToRotation(Vector3.forward, Vector3.up));
                gotAllKeys = true;
            }
        }

        // back up for level manager
        if(PlayerPrefs.GetInt("LatestTransitionScene") == si)
        {
            setTransition = true;
        }

    }

    // Adds/Reduces player score
    public void AddCoin(int noOfCoins)
    {
        endTime += noOfCoins;
        audioManager.PlaySound("CoinSound");
    }

    // Adds Player Health
    public void AddHealth(int health)
    {
        this.playerHealth += health;
    }

    // Kills Player
    public void KillPlayer()
    {
        PlayerPrefs.SetInt("CurrentDeathCount", PlayerPrefs.GetInt("CurrentDeathCount") + 1);
        audioManager.SetPitch("KeySound", originalKeyPitch);
        audioManager.StopSound("Countdown");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Transform playerTransform = player.transform;
        Destroy(player.gameObject);
        audioManager.PlaySound("GameOver");
        Destroy((Instantiate(playerDeathEffect.gameObject, playerTransform.position, Quaternion.FromToRotation(Vector3.forward, Vector3.up))) as GameObject, playerDeathLifetime);

        if(PlayerPrefs.GetInt("CurrentDeathCount") == PlayerPrefs.GetInt("DeathCountToShowAds"))
        {
            PlayerPrefs.SetInt("CurrentDeathCount", 0);
            adManager.ShowAd();
        }

        StartCoroutine(RestartLevel());
    }

    public void SavePlayerData()
    {

        int highScore = PlayerPrefs.GetInt("HighScore");
        float bestTime = PlayerPrefs.GetFloat("BestTime");

        int playerScore = (int)timeLeft * PlayerPrefs.GetInt("ScoreMultiplier");
        if (playerScore > highScore)
        {
            // Made a new Highscore
            PlayerPrefs.SetInt("HighScore", playerScore);
        }
        if (timeLeft > bestTime)
        {
            // Made a new BestTime
            PlayerPrefs.SetFloat("BestTime", timeLeft);
        }
    }

    public void UpdateTimer()
    {
        if (Time.time >= endTime)
        {
            audioManager.StopSound("Countdown");
            playerHealth = 0;
        }
        else
        {
            timeLeft = endTime - Time.time;
            string timeLeftText = (int)timeLeft / 60 + ":" + (timeLeft % 60).ToString("f2");

            // Update time on ui
            timerText.text = timeLeftText.Replace('.', ':');

            if (countdownNotStarted)
            {
                if (timeLeft < 11f)
                {
                    audioManager.PlaySound("Countdown");
                    countdownNotStarted = false;
                }
            }
        }
    }

    public void UpdateKeys()
    {
        Text keyText = GameObject.FindGameObjectWithTag("Txt_Keys").GetComponent<Text>();
        keyText.text = noOfKeysAqquired + "/" + noOfKeys;
    }

    public void CollidedWithAKey()
    {
        noOfKeysAqquired++;

        audioManager.PlaySound("KeySound");
        //audioManager.SetPitch("KeySound", keyPitch += 0.15f);
    }

    public void CollidedWithADoor()
    {
        if (gotAllKeys)
        {
            
            audioManager.SetPitch("KeySound", originalKeyPitch);
            audioManager.StopSound("Countdown");
            SavePlayerData();
            audioManager.PlaySound("DoorSound");
            if (setTransition)
            {
                adManager.ShowAd();
                PlayerPrefs.SetInt("CurrentDeathCount", 0);
                Destroy(audioManager.gameObject);
                StartCoroutine(LoadLevel("_TransitionPage"));
                return;
            }
            StartCoroutine(GoToNextLevel());
        }
    }


    public void OnGoBackButtonClicked()
    {
        Destroy(audioManager.gameObject);
        StartCoroutine(LoadLevel("_ChallengePage", 1));
    }

    public void SetTransition(bool setTransition)
    {
        this.setTransition = setTransition;
    }

    public void PlaySFX(string name)
    {
        if(PlayerPrefs.GetInt("PlaySFX") != 0)
        {
            audioManager.PlaySound(name);
        }
        
    }

    //Couroutine
    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(playerDeathLifetime + 1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator GoToNextLevel()
    {
        // Set Level Reached
        //int currentLevel = SceneManager.GetActiveScene().buildIndex + 1 - PlayerPrefs.GetInt("FirstLevelIndex");
        //int levelReached = PlayerPrefs.GetInt("LevelReached");
        //if (levelReached < currentLevel)
        //{
        //    PlayerPrefs.SetInt("LevelReached", currentLevel);
        //}

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex > SceneManager.sceneCountInBuildSettings - 1)
        {
            audioManager.PlaySound("LevelSelect");
            Destroy(audioManager.gameObject);
            yield return new WaitForSeconds(0);
            StartCoroutine(LoadLevel("_TransitionPage"));
        }
        yield return null;
        SceneManager.LoadScene(nextSceneIndex);
    }

    IEnumerator LoadLevel(string sceneName, float delayTime = 0)
    {
        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(sceneName);
    }

}
