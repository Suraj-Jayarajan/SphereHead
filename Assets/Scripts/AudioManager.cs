using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	[SerializeField]
	Sound[] sounds;
    public bool joinMusic = false;
    public bool forceInstance = false;

    private bool mainMusicJoined = false;

    void Awake ()
	{
		if (instance != null)
		{
            if (PlayerPrefs.GetInt("LastAudioInstanceChangeScene") != SceneManager.GetActiveScene().buildIndex)
            {
                if (forceInstance)
                {
                    Destroy(instance.gameObject);
                    instance = this;
                    PlayerPrefs.SetInt("LastAudioInstanceChangeScene", SceneManager.GetActiveScene().buildIndex);
                    DontDestroyOnLoad(this);
                }
            }
         

          
			if(instance != this)
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

	void Start ()
	{
        if (SceneManager.GetActiveScene().name == "_StartPage")
        {
            PlayerPrefs.SetInt("PlaySFX", 1);
            PlayerPrefs.SetInt("PlayMusic", 1);
        }


        for (int i = 0; i < sounds.Length; i++)
		{
			GameObject _AudioManager = new GameObject("Sound_" + i + "_" + sounds[i].name);
			_AudioManager.transform.SetParent(this.transform);
			sounds[i].SetSource (_AudioManager.AddComponent<AudioSource>());
		}

        if(PlayerPrefs.GetInt("PlayMusic") == 1)
        {
            if (joinMusic)
            {
                PlaySound("Music2");
            }
            else
            {
                PlaySound("Music");
            }
        }
        
	}

    void Update()
    {
        if (joinMusic)
        {
            if (!IsPlaying("Music2"))
            {
                PlaySound("Music");
                joinMusic = false;
            }
        }
    }

    public void PlaySound(string _name)
    {
        //check sfx mute
        if (_name != "Music" && _name != "Music2")
        {
            if (PlayerPrefs.GetInt("PlaySFX") == 0)
            {

                return;
            }
        }
        else
        {

            if (PlayerPrefs.GetInt("PlayMusic") == 0)
            {

                return;
            }
        }

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        // no sound with _name
        Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
    }
    public void StopSound (string _name)
	{
		for (int i = 0; i < sounds.Length; i++)
		{
			if (sounds[i].name == _name)
			{
				sounds[i].Stop();
				return;
			}
		}

		// no sound with _name
		Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
	}


    public bool IsPlaying(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                return sounds[i].IsPlaying();
            }
        }
        joinMusic = false;
        Debug.LogWarning("No sound clip with name " + name + "found!");
        return true;
    }

    public float GetPitch(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                return sounds[i].pitch;
            }
        }
        return 1;
    }

    public void SetPitch(string name, float pitchAmount)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                sounds[i].pitch = pitchAmount;
            }
        }
        
    }
}
