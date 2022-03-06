///////////////////////////////////////////////////////////////////////////////////////////////////////////
// GameManagement
// -------------------------------------------------------------------------------------------------------
// 
// 1. Enable don't disturb on Load
// 2. Create transitionSceneList, transitionMusicList, transitionTypeList
// 3. If scene is changed set sceneChanged flag
// 4. If sceneChange is flagged get current instance of GameSupervisor
// 5. If currentGameSupervisor exists 
//      - If currentscene is one less than any scene in transitionList
//          - set currentGameSupervisor's set transition flag
//        Else
//          - unflag setTransition
////////////////////////////////////////////////////////////////////////////////////////////////////////////


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class TransitionLevel
{
    public int LevelNo;

    //[SerializeField]
    //public Sound[] sounds;
}


public class _SceneManager : MonoBehaviour {

    public static _SceneManager instance;


    [SerializeField]
    TransitionLevel[] transitionLevels;

    private GameSupervisor currentGameSupervisor;
    //private AudioManager currentAudioManager;
    private int currentScene;
    private List<int> transitionScenes = new List<int>();
    
    private bool sceneChanged = false;
    private int currentTransitionScene;

    void Awake()
    {
        // Making instace non destroyable on scene change
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

       
    }

    void Start()
    {
        // Setting up current scene
        currentScene = SceneManager.GetActiveScene().buildIndex;
        // population list of transition scene index
        for(int i = 0; i< transitionLevels.Length; i++)
        {
            transitionScenes.Add(transitionLevels[i].LevelNo + PlayerPrefs.GetInt("FirstLevelIndex") - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(currentScene != SceneManager.GetActiveScene().buildIndex)
        {
            sceneChanged = true;
            currentScene = SceneManager.GetActiveScene().buildIndex;
        }

        if (sceneChanged)
        {
            sceneChanged = false;  // Important
            GameObject go = GameObject.FindGameObjectWithTag("GameSupervisor");
            GameObject go_a = GameObject.FindGameObjectWithTag("AudioManager");


            // if it is a game level
            if (go != null)
            {
                currentGameSupervisor = go.GetComponent<GameSupervisor>();
                
                if (currentGameSupervisor != null)
                {

                    for (int i = 0; i < transitionScenes.Count; i++)
                    {
                        if (currentScene == transitionScenes[i] - 1)
                        {
                            currentGameSupervisor.SetTransition(true);
                            currentTransitionScene = currentScene;
                            PlayerPrefs.SetInt("LatestTransitionScene", currentScene);
                            break;
                        }
                        else
                        {
                            currentGameSupervisor.SetTransition(false);
                        }
                    }
                }
                
            }

            
        }            
    }
}
