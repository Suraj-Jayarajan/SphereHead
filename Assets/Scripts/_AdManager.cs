using System.Collections;
using UnityEngine.Advertisements;
using System.Collections.Generic;
using UnityEngine;

public class _AdManager : MonoBehaviour
{
    public static _AdManager instance;

    private static string gameId = "1679056";


    void Awake()
    {
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
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, true);
        }
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video", new ShowOptions() { resultCallback = HandleAdResult });
        }
    }

    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Ad showed");
                break;
            case ShowResult.Skipped:
                Debug.Log("Player did not fully watched the ad");
                break;
            case ShowResult.Failed:
                Debug.Log("No internet!!! Player failed to launch the ad");
                break;

        }
    }
}
