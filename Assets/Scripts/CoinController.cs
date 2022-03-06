using UnityEngine;

public class CoinController : MonoBehaviour {

    public float speed = 10f;

	// Update is called once per frame
	void Update () {
        // Rotate Coin
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);

	}
}
