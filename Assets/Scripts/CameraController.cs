using UnityEngine;

public class CameraController : MonoBehaviour {

    public bool followPlayer = false;
    public bool playerAtCenter = false;
    public float followSpeed = 5.0f;
    public float followOffset = 0.1f; // setting this value to any value other than 0 stops camera jittring

    //public float smoothSpeed = 0.125f;
    //public Vector3 offsetPosition;

    private GameObject player;
    private GameObject Ground;
    private float initialCameraPosX;
    private float initialCameraPosZ;
    private float smoothSpeed = 0.125f; // value should be between 0 and 1
    private float offsetPosY = 10f;

    // Use this for initialization
    void Start() {
        Ground = GameObject.FindGameObjectWithTag("Ground");
        player = GameObject.FindGameObjectWithTag("Player");
        initialCameraPosX = transform.position.x - player.transform.position.x;
        initialCameraPosZ = transform.position.z - player.transform.position.z;
        if (playerAtCenter)
        {
            initialCameraPosX = transform.position.x;
            initialCameraPosZ = transform.position.z;
        }
        offsetPosY = transform.position.y - player.transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate() {

        // Check if player is Destroyed
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            return;
        }


        if (followPlayer)
        {
            float cameraPosX = player.transform.position.x + initialCameraPosX;
            float cameraPosZ = player.transform.position.z + initialCameraPosZ;
            float cameraPozY = player.transform.position.y + offsetPosY;

            if (followSpeed !=0 )
            { 
                Vector3 dir = new Vector3(cameraPosX, cameraPozY, cameraPosZ) - transform.position;
                if ((player.transform.position.x - transform.position.x >= followOffset || player.transform.position.x - transform.position.x <= -followOffset) || (player.transform.position.z - transform.position.z >= followOffset || player.transform.position.z - transform.position.z <= -followOffset))
                {
                    transform.Translate(dir.normalized * (followSpeed * Time.deltaTime), Space.World);
                }
            }
            else
            {
                //Vector3 desiredPosition = new Vector3(cameraPosX, transform.position.y, cameraPosZ);
                //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                //transform.position = smoothedPosition;
                transform.position = new Vector3(cameraPosX, gameObject.transform.position.z, cameraPosZ);
            }
        }

        // Boundry Check
        //Vertical Boundry Check
        if (transform.position.z + Camera.main.orthographicSize >= Ground.transform.localScale.z / 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Ground.transform.localScale.z / 2 - Camera.main.orthographicSize);
        }
        if (transform.position.z - Camera.main.orthographicSize <= -Ground.transform.localScale.z/2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -Ground.transform.localScale.z/2 + Camera.main.orthographicSize);
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
