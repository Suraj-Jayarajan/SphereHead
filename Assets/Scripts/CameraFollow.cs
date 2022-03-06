using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public float smoothSpeed = 0.125f;
    public Vector3 offset =  new Vector3(0,10,0);

    private Transform target;
    private GameObject Ground;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Ground = GameObject.FindGameObjectWithTag("Ground");
        transform.position = target.position + offset;

    }

    void FixedUpdate()
    {
       
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = desiredPosition;
        }

        // Boundry Check
        //Vertical Boundry Check
        if (transform.position.z + Camera.main.orthographicSize >= Ground.transform.localScale.z / 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Ground.transform.localScale.z / 2 - Camera.main.orthographicSize);
        }
        if (transform.position.z - Camera.main.orthographicSize <= -Ground.transform.localScale.z / 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -Ground.transform.localScale.z / 2 + Camera.main.orthographicSize);
        }
        //Horizontal Boundry Check
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;
        if (transform.position.x + widthOrtho >= Ground.transform.localScale.x / 2)
        {
            transform.position = new Vector3(Ground.transform.localScale.x / 2 - widthOrtho, transform.position.y, transform.position.z);
        }

        if (transform.position.x - widthOrtho <= -Ground.transform.localScale.x / 2)
        {
            transform.position = new Vector3(-Ground.transform.localScale.x / 2 + widthOrtho, transform.position.y, transform.position.z);
        }
    }
}
