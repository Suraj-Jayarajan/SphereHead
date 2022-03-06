using System.Collections;
using UnityEngine;

public class ScalingScript : MonoBehaviour
{

    public float scalingSpeed = 1f;
    public float rescaleTime = 2f;
    public float startDelay = -1;

    private Transform scalingObject;
    private Vector3 objectOriginalSize;
    private Quaternion objectOriginalRotation;
    private bool isObjectScaledUp = true, scaleDown = true;
    private bool startBig = false; // useless variable
    private bool ranOnce = false, preventUpdate = true;


    // Use this for initialization
    void Start()
    {
        scalingObject = transform;
        objectOriginalSize = scalingObject.localScale;
        objectOriginalRotation = scalingObject.localRotation;

        if (!startBig)
        {
            scalingObject.localScale = new Vector3(1, objectOriginalSize.y, 1);
            isObjectScaledUp = false;
            scaleDown = false;
        }

        startDelay = Random.Range(0, 2);

        //StartCoroutine(DelayStart(startDelay));
    }


    // Update is called once per frame
    void Update()
    {
        ScaleObject(scalingObject, scalingSpeed);
        scalingObject.Rotate(new Vector3(0, scalingSpeed / 2, 0));

        //if (preventUpdate)
        //{
        //    return;
        //}

        //if (scaleDown) {
        //    if (scalingObject.localScale.x > 1)
        //    {
        //        ScaleObject(scalingObject, -scalingSpeed * 2);
        //    }
        //    else
        //    {
        //        isObjectScaledUp = false;
        //    }
            
        //}
        //else
        //{
        //    if (scalingObject.localScale.x < objectOriginalSize.x)
        //    {
        //        ScaleObject(scalingObject, scalingSpeed);
        //        scalingObject.Rotate(new Vector3(0, -scalingSpeed / 2, 0));
        //    }
        //    else
        //    {
        //        scalingObject.localRotation = objectOriginalRotation;
        //        isObjectScaledUp = true;

        //        StartCoroutine(ScaleDownToggle());
        //    }
        //}



    }

    void ScaleObject(Transform t, float scalingSpeed, bool isSizableZ = true)
    {
        t.localScale = new Vector3(t.localScale.x + scalingSpeed * Time.deltaTime, t.localScale.y, isSizableZ ? t.localScale.z + scalingSpeed * Time.deltaTime : t.localScale.z);
    }

    // Couroutine
    IEnumerator ScaleDownToggle()
    {
        yield return new WaitForSeconds(rescaleTime);
        scaleDown = !scaleDown;
    }

    IEnumerator DelayStart(float time)
    {
        yield return new WaitForSeconds(time);
        preventUpdate = false;
    }

}
