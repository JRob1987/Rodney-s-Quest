using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour
{
    //variables
    private Vector3 movementDirection = Vector3.left;
    private Vector3 originPosition;
    private Vector3 movePosition;
    [SerializeField] private GameObject birdStone;
    [SerializeField] private LayerMask playerLayer;
    private bool attacked = false;
    private bool canMove = false;

    //reference variables
    private Rigidbody2D body;
    private Animator anim;
    private PlayerTwo player;

    private void Awake()
    {
        //reference components
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerTwo>();

    }

    private void Start()
    {
        originPosition = transform.position;
        originPosition.x += 6f;

        movePosition = transform.position;
        movePosition.x -= 6f;

        canMove = true;
    }

    private void Update()
    {
        BirdMovement();
        DropStone();
    }

    void BirdMovement()
    {
        if(canMove)
        {
            transform.Translate(movementDirection * Time.smoothDeltaTime);
            if(transform.position.x >= originPosition.x)
            {
                movementDirection = Vector3.left;
                ChangeDirection(0.6f);
            }
            else if(transform.position.x <= movePosition.x)
            {
                movementDirection = Vector3.right;
                ChangeDirection(-0.6f);
            }
        }
    }


    void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void DropStone()
    {
        if(!attacked)
        {
            if(Physics2D.Raycast(transform.position, Vector2.down, Mathf.Abs(-6f), playerLayer))
            {
                Instantiate(birdStone, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
                attacked = true;
                anim.Play("BirdFly");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == LevelTwoTags.PlayerTwo)
        {
            player.PlayerDamaged();
        }
    }

    











}
