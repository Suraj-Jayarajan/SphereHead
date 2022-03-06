using System.Collections;
using UnityEngine;

public class CrystalController : MonoBehaviour {

    private Renderer rend;
    private Material primaryMat, secondaryMat;
    private int crystalIndex;
    private bool collisionActive = true;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;

        primaryMat = rend.sharedMaterial;
    }
	
    public void SetSecondaryMaterial(Material mat, int index)
    {
        secondaryMat = mat;
        crystalIndex = index;
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            if (collisionActive)
            {
                ToggleMaterial();
                transform.parent.GetComponent<ColorCubesController>().PairHandler(crystalIndex, secondaryMat.name);
                collisionActive = false;
                StartCoroutine(ReActivateCollision());
            }
               
        }
    }


    void ToggleMaterial()
    {
        if (rend.sharedMaterial == primaryMat)
        {
            rend.sharedMaterial = secondaryMat;
        }
        else
        {
            rend.sharedMaterial = primaryMat;
        }
    }

    public void MatchFound()
    {
        primaryMat = secondaryMat;
    }

    public void ToggleBackToPrimary()
    {
        rend.sharedMaterial = primaryMat;
    }

    //Couroutine
    IEnumerator ReActivateCollision()
    {
        yield return new WaitForSeconds(1);
        collisionActive = true;
    }
}
