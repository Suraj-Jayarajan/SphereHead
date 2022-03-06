using UnityEngine;

public class FollowPath : MonoBehaviour {
 
    public Transform[] nodes;
    public float reachDistance = 0.1f;
    public float xSpeed = 3f;
    public bool switchPressed = true;
    public bool loopPath = true;
    public bool setInitialPosToNode = false;

    private int currentPath = 0;
    private bool neverSetInitialPos = false;

    void Start()
    {
        if (nodes.Length > 0)
        {
          
            SetNextPath();
        }
        else
        {
            Debug.LogError("No nodes assigned to " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (setInitialPosToNode && neverSetInitialPos)
        {
            transform.position = nodes[0].position;
            neverSetInitialPos = false;
        }

        if (nodes.Length != 0 && switchPressed)
        {
            moveObjectByPath();
        }
    }

    void moveObjectByPath() {
        Vector3 dir = nodes[currentPath].position - transform.position;
        if(dir.magnitude <= reachDistance) {
            transform.position = nodes[currentPath].position;
            if (!loopPath)
            {
                switchPressed = false;
            }
            SetNextPath();
        }
        else
        {
            transform.Translate(dir.normalized * (xSpeed * Time.deltaTime));
        }
    }

    void SetNextPath()
    {
        currentPath++;

        if (currentPath >= nodes.Length)
        {
            currentPath = 0;
        }
    }
}
