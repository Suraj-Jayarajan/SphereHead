////////////////////////////////////////////////////////////////////////
// Name: Parallax Effect 
// Date Created: 20/11/2017
// Author: Suraj P J
// --------------------------------------------------------------------
// Algorithm
//  - Get transform of the player
//  - Get the initial position of the player and set it as Player Origin
//  - Get the initial position of the Object being parallaxed and set it as Self Origin
//  - In Update
//     - Get the offsetVector from player origin to player's current position
//     - Multiply offsetVector with parallaxStrength to control parallax movement
//     - Set the current position of parallaxed object by adding its origin to parallaxed offset vector;
///////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour {

    // variable: parallaxStrength
    //   - if set to 0 -> player origin and self origin will be equal and parallaxed object will with the player
    //   - if 0>parallaxStrength<1 -> parallax speed will be slower than player movement
    //   - if parallaxStrength=1 -> parallax speed will be equal to player movement
    //   - if parallaxStrength>1 -> parallax speed will be grater than player movement
    public float parallaxStrength = 0.5f;
    public bool enableTiling = false;

    private Vector3 selfOrigin, playerOrigin;
    private Transform playerTransform;
    private GameObject topTile, bottomTile, rightTile, leftTile, topLeftTile, topRightTile, bottomLeftTile, bottomRightTile;
    private float minDistanceToTileCreation;

    // Use this for initialization
    void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        selfOrigin = transform.position;
        playerOrigin = playerTransform.position;
        if(minDistanceToTileCreation != null)
        {
            minDistanceToTileCreation = Camera.main.orthographicSize;
        }
    }


    void LateUpdate()
    {
        // parallax effect mechanism
        if (playerTransform != null) //Checking if player object is not destroyed
        {
            Vector3 playerOffset = playerOrigin - playerTransform.position;
            transform.position = (selfOrigin + playerOffset * parallaxStrength);
        }

        if (enableTiling)
        {
            TileIfNeeded();
        }
    }

    void TileIfNeeded()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;

        //Verticle Tiling-for top tile, top-right tile and top-left tile
        if (Camera.main.transform.position.z + Camera.main.orthographicSize >= (transform.position.z + transform.localScale.z / 2) - minDistanceToTileCreation)
        {
            // top tile instatiate
            if (topTile == null)
            {
                topTile = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z + transform.localScale.z), transform.rotation);
                topTile.GetComponent<ParallaxEffect>().enableTiling = false;
            }
        }
        else
        {
            if (topTile != null)
            {
                Destroy(topTile);
            }
        }


        //Verticle Tiling-for bottom tile
        if (Camera.main.transform.position.z - Camera.main.orthographicSize <= (transform.position.z - transform.localScale.z / 2) + minDistanceToTileCreation)
        {
            if (bottomTile == null)
            {
                bottomTile = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z - transform.localScale.z), transform.rotation);
                bottomTile.GetComponent<ParallaxEffect>().enableTiling = false;
            }
        }
        else
        {
            if (bottomTile != null)
            {
                Destroy(bottomTile);
            }
        }

        //Horizontal Tiling-for Right Tile
        if (Camera.main.transform.position.x + widthOrtho >= (transform.position.x + transform.localScale.x / 2) - widthOrtho/2)
        {
            if (rightTile == null)
            {
                rightTile = Instantiate(gameObject, new Vector3(transform.position.x + transform.localScale.x, transform.position.y, transform.position.z), transform.rotation);
                rightTile.GetComponent<ParallaxEffect>().enableTiling = false;
            }
        }
        else
        {
            if (rightTile != null)
            {
                Destroy(rightTile);
            }
        }


        //Horizontal Tiling-for Left Tile
        if (Camera.main.transform.position.x - widthOrtho <= (transform.position.x - transform.localScale.x / 2) + widthOrtho/2)
        {
            if (leftTile == null)
            {
                leftTile = Instantiate(gameObject, new Vector3(transform.position.x- transform.localScale.x, transform.position.y, transform.position.z), transform.rotation);
                leftTile.GetComponent<ParallaxEffect>().enableTiling = false;
            }
        }
        else
        {
            if (leftTile != null)
            {
                Destroy(leftTile);
            }
        }

        // top-right tile instantiate
        if (topTile != null && rightTile != null)
        {
            if (topRightTile == null)
            {
                topRightTile = Instantiate(gameObject, new Vector3(transform.position.x + transform.localScale.x, transform.position.y, transform.position.z + transform.localScale.z), transform.rotation);
                topRightTile.GetComponent<ParallaxEffect>().enableTiling = false;
            }
        }
        if ((topTile == null || rightTile == null)&& topRightTile !=null)
        {
            Destroy(topRightTile);
        }
        
        // top-left tile instantiate
        if (topTile != null && leftTile != null)
        {
            if (topLeftTile == null)
            {
                topLeftTile = Instantiate(gameObject, new Vector3(transform.position.x - transform.localScale.x, transform.position.y, transform.position.z + transform.localScale.z), transform.rotation);
                topLeftTile.GetComponent<ParallaxEffect>().enableTiling = false;
            }
        }
        if ((topTile == null || leftTile == null) && topLeftTile != null)
        {
            Destroy(topLeftTile);
        }

        // bottom-right tile instantiate
        if (bottomTile != null && rightTile != null)
        {
            if (bottomRightTile == null)
            {
                bottomRightTile = Instantiate(gameObject, new Vector3(transform.position.x + transform.localScale.x, transform.position.y, transform.position.z - transform.localScale.z), transform.rotation);
                bottomRightTile.GetComponent<ParallaxEffect>().enableTiling = false;
            }
        }
        if ((bottomTile == null || rightTile == null) && bottomRightTile != null)
        {
            Destroy(bottomRightTile);
        }

        // bottom-left tile instantiate
        if (bottomTile != null && leftTile != null)
        {
            if (bottomLeftTile == null)
            {
                bottomLeftTile = Instantiate(gameObject, new Vector3(transform.position.x - transform.localScale.x, transform.position.y, transform.position.z - transform.localScale.z), transform.rotation);
                bottomLeftTile.GetComponent<ParallaxEffect>().enableTiling = false;
            }
        }
        if ((bottomTile == null || leftTile == null) && bottomLeftTile != null)
        {
            Destroy(bottomLeftTile);
        }
    }
}