using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed = 10f;

    private GameObject joystickBg;
    private VirtualJoystick moveJoystick;
    private GameSupervisor gameSupervisor;
    private bool playerMovedForFistTime = false;
    private Vector3 leftEyeOriginalPos, rightEyeOriginalPos;
    private Rigidbody rbody;

    void Start()
    {

        moveJoystick = GameObject.FindGameObjectWithTag("JoystickBg").GetComponent<VirtualJoystick>();
        gameSupervisor = GameObject.FindGameObjectWithTag("GameSupervisor").GetComponent<GameSupervisor>();
        leftEyeOriginalPos = transform.GetChild(0).transform.localPosition;
        rightEyeOriginalPos = transform.GetChild(1).transform.localPosition;
        rbody = GetComponent<Rigidbody>();
    }

    int i = 0;
    // Update is called once per frame
    void Update()
    {
        // Check to prevent rigid body /Colliders from sleeping
        if (rbody.IsSleeping())
        {
            rbody.WakeUp();
            
        }

        // Boundry Checks
        // Vertical Boundry Check
        if (transform.position.z + transform.localScale.z / 2 >= Camera.main.transform.position.z + Camera.main.orthographicSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z + Camera.main.orthographicSize - transform.localScale.z / 2);
        }
        if (transform.position.z - transform.localScale.z / 2 <= Camera.main.transform.position.z - Camera.main.orthographicSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z - Camera.main.orthographicSize + transform.localScale.z / 2);
        }
        // Horizontal Boundry Check
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;
        if (transform.position.x + transform.localScale.x / 2 >= Camera.main.transform.position.x + widthOrtho)
        {
            transform.position = new Vector3(Camera.main.transform.position.x + widthOrtho - transform.localScale.x / 2, transform.position.y, transform.position.z);
        }
        if (transform.position.x - transform.localScale.x / 2 <= Camera.main.transform.position.x - widthOrtho)
        {
            transform.position = new Vector3(Camera.main.transform.position.x - widthOrtho + transform.localScale.x / 2, transform.position.y, transform.position.z);
        }

        // Set up Navigation Input
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 direction = input.normalized;
        if (moveJoystick.InputDirection != Vector3.zero)
        {
            direction = moveJoystick.InputDirection;
        }



        // Movement Controls
        Vector3 moveAmount = direction * speed * Time.deltaTime;
        transform.position += moveAmount;

        //Rotation Controls
        transform.Rotate(new Vector3(0, direction.x * speed * 2, 0));
        if (direction.x == 0)
        {
            transform.Rotate(new Vector3(0, direction.z * speed * 2, 0));
        }
        if (direction == Vector3.zero)
        {
            transform.rotation = new Quaternion();
        }


        //start timer mechanism
        if (!playerMovedForFistTime && moveAmount != Vector3.zero)
        {
            gameSupervisor.startTimer = true;
        }
    }



    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "MovingEnemy" || other.collider.tag == "Spike" || other.collider.tag == "PathSeekingEnemy" || other.collider.tag == "Obstacle")
        {
            gameSupervisor.AddHealth(-100);
        }

        if (other.collider.tag == "Coin")
        {
            gameSupervisor.AddCoin(1);
            Destroy(other.gameObject);
        }

        if (other.collider.tag == "Key")
        {
            gameSupervisor.CollidedWithAKey();
            Destroy(other.gameObject);
        }

        if (other.collider.tag == "Door")
        {
            gameSupervisor.CollidedWithADoor();
        }

    }



    // Couroutine
    IEnumerator GoToNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex > SceneManager.sceneCountInBuildSettings - 1)
        {
            nextSceneIndex--;
        }
        yield return null;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
