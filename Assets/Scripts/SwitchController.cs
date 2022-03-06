using System.Collections;
using UnityEngine;

public class SwitchController : MonoBehaviour {

    public GameObject[] game_Objects;
    public bool disableFollowPath = true;
    public bool disablePathLooping = true;

    private bool collisionActive = true;

    // Use this for initialization
    void Start () {

        if (disableFollowPath)
        {
            ToggleActivty();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Player")
        {
            if (collisionActive)
            {
                ToggleActivty();
                collisionActive = false;
                StartCoroutine(ReActivateCollision());
            }
        }

    }

    void ToggleSwitch()
    {
        transform.Rotate(0,180,0);
    }

    void ToggleActivty()
    {
        foreach (GameObject g in game_Objects)
        {
            FollowPath fp = g.GetComponent<FollowPath>();

            fp.switchPressed = !fp.switchPressed;
            fp.setInitialPosToNode = true;
            if (disablePathLooping)
            {
                fp.loopPath = false;
            }

        }
        ToggleSwitch();
    }

    //Couroutine
    IEnumerator ReActivateCollision()
    {
        yield return new WaitForSeconds(1);
        collisionActive = true;
    }
}
