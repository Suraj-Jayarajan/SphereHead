using System.Collections;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public Vector3 rotationSpeed = new Vector3(0f,0f, 0f);
    public bool disablePausedRotation = true;
    public float delayTime = 3f;

    private bool rotateFlag = true;

	// Use this for initialization
	void Start () {
        if (!disablePausedRotation)
        {
            StartCoroutine(ToggleRotation());
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (rotateFlag)
        {
            transform.Rotate(rotationSpeed);
        }
	}

    //Couroutine
    IEnumerator ToggleRotation()
    {
        yield return new WaitForSeconds(delayTime);
        rotateFlag = !rotateFlag;
        StartCoroutine(ToggleRotation());
    }
}
