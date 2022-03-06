using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSeekerTrigger : MonoBehaviour {

    public bool triggerPressed = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            triggerPressed = true;
        }
    }
}
