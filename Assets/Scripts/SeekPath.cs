using UnityEngine;

public class SeekPath : MonoBehaviour {

    public float speed = 5f;
    public bool targetLocked = false;

    GameObject target;
    Transform targetPosition;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checking if target is destroyed
        target = GameObject.FindGameObjectWithTag("Player");
        if (target== null)
        {
            return;
        }

        // Follow Target
        targetPosition = target.transform;
        if (targetLocked)
        {
            Vector3 dir = targetPosition.position - transform.position;
            transform.Translate(dir.normalized * (speed * Time.deltaTime));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
