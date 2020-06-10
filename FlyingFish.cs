using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingFish : MonoBehaviour
{

    //variables
    private Vector3 movementDirection = Vector3.down;
    private Vector3 originPosition;
    private Vector3 movePosition;
    private bool canMove = false;
    [SerializeField] private float _speed = 1f;


    //reference varaiables
    private Rigidbody2D body;
    private Animator anim;
    private LevelThreePlayer player;
    private MainThree main;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<LevelThreePlayer>();
        main = GameObject.Find("Main Camera").GetComponent<MainThree>();
    }

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
        originPosition.y += 2f;

        movePosition = transform.position;
        movePosition.y -= 5f;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        //method call
        FlyingFishMovement();
    }

    //fish movement
    //spider movement
    void FlyingFishMovement()
    {

        if (canMove)
        {
            transform.Translate(movementDirection * Time.smoothDeltaTime);
            if (transform.position.y >= originPosition.y)
            {
                movementDirection = Vector3.down * _speed;
                
            }
            else if (transform.position.y <= movePosition.y)
            {
                movementDirection = Vector3.up * _speed;
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Level3Tags.LevelThreePlayer)
        {
            player.PlayerDamaged();
        }
        else if (collision.gameObject.tag == Tags.flameBulletTag)
        {
            Destroy(collision.gameObject);           

        }

    }




}
