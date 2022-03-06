using UnityEngine;

public class DoorController : MonoBehaviour {

    public bool openDoor = false;
    public Material mat;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

	// Update is called once per frame
	void Update () {
        if (openDoor)
        {
            rend.sharedMaterial = Resources.Load("Materials/White", typeof(Material)) as Material;
  
        }
	}
}
