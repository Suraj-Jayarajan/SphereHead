using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour {

    public int levelIndexNumber = 0;
    private Button thisButton;
    private AudioManager audioManager; //Audio Manager


    void Start()
    {
        thisButton = GetComponent<Button>(); 

        thisButton.onClick.AddListener(() => { myFunctionForOnClickEvent(); });

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager in the scene!!!");
        }
    }

    public void myFunctionForOnClickEvent()
    {
        if (levelIndexNumber != 0)
        {
            audioManager.StopSound("Music");
            audioManager.PlaySound("LevelSelect");

            StartCoroutine(LoadLevel(levelIndexNumber,2));
        }
    }


    // Co-routine
    IEnumerator LoadLevel(int sceneIndex, float waitSeconds = 0)
    {
        yield return new WaitForSeconds(waitSeconds);
        if (audioManager != null)
        {
            Destroy(audioManager.gameObject);
        }
        SceneManager.LoadScene(sceneIndex);
    }
}
