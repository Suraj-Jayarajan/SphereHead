using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LevelSelectorContentController : MonoBehaviour {

    public GameObject LevelButton;

    int totalScenes;
    int levelNumber = 1;
    int levelReached;
    int firstLevelIndex;

	// Use this for initialization
	void Start () {
        firstLevelIndex = PlayerPrefs.GetInt("FirstLevelIndex");
        levelReached = PlayerPrefs.GetInt("LevelReached");
        totalScenes = SceneManager.sceneCountInBuildSettings;
        for(int i = firstLevelIndex; i < totalScenes; i++)
        {
            AddNewLevelButton(levelNumber);
            levelNumber++;
        }

    }

    

    void AddNewLevelButton(int levelNumber)
    {
        GameObject newButton = Instantiate(LevelButton) as GameObject;
        newButton.transform.SetParent(this.transform, false);
        newButton.GetComponentInChildren<Text>().text = levelNumber.ToString();
        newButton.GetComponent<LevelButtonController>().levelIndexNumber = firstLevelIndex + (levelNumber-1);
        if(levelNumber > levelReached)
        {
            newButton.GetComponent<Button>().interactable = false;
        }
        if(levelNumber == 1)
        {
            newButton.GetComponent<Button>().interactable = true;
        }
    }
}
