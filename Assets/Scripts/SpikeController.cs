using System.Collections;
using UnityEngine;

public class SpikeController : MonoBehaviour {

    public float speed = 100f;
    public bool disablePausedRotation = false;
    private float delayTime = 1f;
    private bool rotateFlag = true;

    void Start()
    {
        transform.Rotate(new Vector3(0,0,Random.Range(0,45)));
        delayTime = Random.Range(0.4f, 1f);
        StartCoroutine(ToggleRotation());
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateFlag)
        {
            // Rotate Coin
            transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        }

    }

    //Couroutine
    IEnumerator ToggleRotation()
    {
        yield return new WaitForSeconds(delayTime);
        if (!disablePausedRotation)
        {
            rotateFlag = !rotateFlag;
            StartCoroutine(ToggleRotation());
        }
    }
}
