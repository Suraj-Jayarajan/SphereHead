using System.Collections;
using UnityEngine;

public class ScalingBlockController : MonoBehaviour {

    public float scalingSpeed = 1f;
    public float rescaleTime = 2f;
    public float startDelay = -1;

    private Transform scalingLever1, scalingLever2, scalingCube;
    private Vector3 lever1OriginalSize, lever2OriginalSize, cubeOriginalSize;
    private Quaternion cubeOriginalRotation;
    private bool isLeversScaledUp = true, isCubeScaledUp = true, scaleDown = true;
    private bool startBig = false; // useless variable
    private bool ranOnce = false, preventUpdate = true;


    // Use this for initialization
    void Start () {
        scalingLever1 = transform.GetChild(0).transform;
        scalingLever2 = transform.GetChild(1).transform;
        scalingCube = transform.GetChild(2).transform;

       

        lever1OriginalSize = scalingLever1.localScale;
        lever2OriginalSize = scalingLever2.localScale;
        cubeOriginalSize = scalingCube.localScale;
        cubeOriginalRotation = scalingCube.localRotation;

        if (!startBig)
        {
            scalingLever1.localScale = new Vector3(1, lever1OriginalSize.y, lever1OriginalSize.z);
            scalingLever2.localScale = new Vector3(1, lever2OriginalSize.y, lever2OriginalSize.z);
            scalingCube.localScale = new Vector3(1, cubeOriginalSize.y, 1);
            isLeversScaledUp = false;
            isCubeScaledUp = false;
            scaleDown = false;
        }

        startDelay = Random.Range(0, 2);

        StartCoroutine(DelayStart(startDelay));
    }

	
	// Update is called once per frame
	void Update () {

        if (preventUpdate)
        {
            return;
        }

        if (scaleDown)
        {
            if (isCubeScaledUp && isLeversScaledUp)
            {
                if (scalingCube.localScale.x > 1)
                {
                    ScaleObject(scalingCube, -scalingSpeed * 2);
                }
                else
                {
                    isCubeScaledUp = false;
                }
            }

            if (!isCubeScaledUp && isLeversScaledUp)
            {
                if(scalingLever1.localScale.x > 1)
                {
                    ScaleObject(scalingLever1, -scalingSpeed * 2, false);
                    ScaleObject(scalingLever2, -scalingSpeed * 2, false);
                }
                else
                {
                    StartCoroutine(ScaleDownToggle());
                    isLeversScaledUp = false;
                }
            }
           

        }
        else
        {
            if (!isCubeScaledUp && isLeversScaledUp)
            {
                if (scalingCube.localScale.x < cubeOriginalSize.x)
                {
                    ScaleObject(scalingCube, scalingSpeed);
                    scalingCube.Rotate(new Vector3(0, -scalingSpeed / 2, 0));
                }
                else
                {
                    scalingCube.localRotation = cubeOriginalRotation;
                    isCubeScaledUp = true;

                    StartCoroutine(ScaleDownToggle());
                }
            }
           

            if ( !isCubeScaledUp && !isLeversScaledUp)
            {
                if (scalingLever1.localScale.x < lever1OriginalSize.x)
                {
                    ScaleObject(scalingLever1, scalingSpeed, false);
                    ScaleObject(scalingLever2, scalingSpeed, false);
                }
                else
                {
                    isLeversScaledUp = true;
                }
            }
        }

        
	}

    void ScaleObject(Transform t, float scalingSpeed, bool isSizableZ = true)
    {
        t.localScale = new Vector3(t.localScale.x + scalingSpeed * Time.deltaTime, t.localScale.y, isSizableZ? t.localScale.z + scalingSpeed * Time.deltaTime : t.localScale.z);
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
