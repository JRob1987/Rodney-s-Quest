//makes the camera follow the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float resetSpeed = 4.0f; //determine how fast the camera goes back to player
    public float cameraSpeed = 4.0f; //speed of the camera
    public Bounds camerabounds;
    private Transform target;
    private float offsetZ;
    private Vector3 lastTargetPosition;
    private Vector3 currentVelocity;
    private bool followsPlayer;

    private void Awake()
    {
        BoxCollider2D myCol = GetComponent<BoxCollider2D>();
        myCol.size = new Vector2(Camera.main.aspect * 2 * Camera.main.orthographicSize, 15f);
        camerabounds = myCol.bounds;
    }

    // Start is called before the first frame update
    void Start()
    {
        // target = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag(Tags.playerTag).transform;
        lastTargetPosition = target.position;
        offsetZ = (transform.position - target.position).z;
        followsPlayer = true;
    }

    private void FixedUpdate()
    {
        CameraFollowPlayer();
    }

   
    //follows the player
    void CameraFollowPlayer()
    {
        if (followsPlayer == true)
        {
            Vector3 ahdeadTargetPos = target.position + Vector3.forward * offsetZ;
            if (ahdeadTargetPos.x >= transform.position.x)
            {
                Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, ahdeadTargetPos, ref currentVelocity, cameraSpeed);
                transform.position = new Vector3(newCameraPosition.x, transform.position.y, newCameraPosition.z);
                lastTargetPosition = target.position; //record las tposition of the target

            }
        }
    }




} //class
