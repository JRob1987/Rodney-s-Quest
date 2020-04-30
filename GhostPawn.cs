using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPawn : MonoBehaviour
{
    //variables
    private Vector3 movementDirection = Vector3.left;
    private Vector3 originPosition;
    private Vector3 movePosition;
    private bool canMove = false;
    [SerializeField] private float _speed = 3f;

    //reference variables
    private Rigidbody2D body;
    private Animator anim;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        originPosition = transform.position;
        originPosition.x += 8f;

        movePosition = transform.position;
        movePosition.x -= 8f;

        canMove = true;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (canMove)
        {
            transform.Translate(movementDirection * _speed * Time.smoothDeltaTime);
            if (transform.position.x >= originPosition.x)
            {
                movementDirection = Vector3.left * _speed;
                ChangeDirection(3.8f);
            }
            else if (transform.position.x <= movePosition.x)
            {
                movementDirection = Vector3.right * _speed;
                ChangeDirection(-3.8f);
            }
        }


    }

    private void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

}
