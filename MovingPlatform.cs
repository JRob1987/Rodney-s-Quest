using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform movingA, movingB;
    [SerializeField] private float movementSpeed = 3.0f;
    [SerializeField] private bool switchDirection = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //method call
        MovingBlockMovement();
    }

    //moving block movement
    private void MovingBlockMovement()
    {
        if (switchDirection== false)
        {
            transform.position = Vector3.MoveTowards(transform.position, movingB.position, movementSpeed * Time.deltaTime);
        }
        else if (switchDirection == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, movingA.position, movementSpeed * Time.deltaTime);
        }

        if (transform.position == movingB.position)
        {
            switchDirection = true;
        }

        else if (transform.position == movingA.position)
        {
            switchDirection = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = null;
        }
    }

   

}
