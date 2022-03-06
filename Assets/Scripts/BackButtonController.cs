using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BackButtonController : MonoBehaviour {

    public string changeToLevel = "_ChallengePage";

    private AudioManager audioManager;
    private Button thisButton;
    void Start()
    {
        thisButton = GetComponent<Button>();

        thisButton.onClick.AddListener(() => { myFunctionForOnClickEvent(); });
        
        // For Audio Manager
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager in the scene!!!");
        }
    }

    public void myFunctionForOnClickEvent()
    {
        Destroy(audioManager.gameObject);
        StartCoroutine(LoadLevel(changeToLevel));
    }


    // Co-routine
    IEnumerator LoadLevel(string sceneName, float waitSeconds = 0)
    {
        yield return new WaitForSeconds(waitSeconds);

        SceneManager.LoadScene(sceneName);
    }
}
