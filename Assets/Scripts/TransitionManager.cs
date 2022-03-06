using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TransitionManager : MonoBehaviour {

    public float transitionTime = 2;


    private AudioManager audioManager; //Audio Manager


    // Use this for initialization
    void Start () {

        print(PlayerPrefs.GetInt("CurrentSceneIndex")+ " " + SceneManager.sceneCountInBuildSettings);
        if(PlayerPrefs.GetInt("CurrentSceneIndex") >= SceneManager.sceneCountInBuildSettings-1)
        {
            StartCoroutine(LoadLevel("_CreditsPage", transitionTime));
        }

        StartCoroutine(LoadLevel(PlayerPrefs.GetInt("CurrentSceneIndex")+1, transitionTime));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator LoadLevel(int sceneIndex, float transitionTime)
    {
        yield return new WaitForSeconds(transitionTime);
        //Kill Audio Manager
        audioManager = AudioManager.instance;
        if (audioManager != null)
        {
            Destroy(audioManager.gameObject);
        }
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator LoadLevel(string sceneName, float delayTime = 0)
    {
        yield return new WaitForSeconds(delayTime);
        //Kill Audio Manager
        audioManager = AudioManager.instance;
        if (audioManager != null)
        {
            Destroy(audioManager.gameObject);
        }
        SceneManager.LoadScene(sceneName);
    }
}
