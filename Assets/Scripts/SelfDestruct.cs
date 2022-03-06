using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    public float delayToDestroy = 1f;


    void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "Player") {
            StartCoroutine(DestroySelf(delayToDestroy));
        }    
    }


    //Couroutine
    IEnumerator DestroySelf(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

}
