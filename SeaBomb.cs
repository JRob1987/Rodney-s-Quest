using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaBomb : MonoBehaviour
{
    //variables
    [SerializeField] private float movementSpeed = 1f;
    private Vector3 moveDirection = Vector3.down;
    private Vector3 startingPosition;
    private Vector3 destinationPosition;
    private bool canMove = false;
    [SerializeField] private bool canExplode = false;
    

    //reference variables
    private Rigidbody2D body;
    private Animator anim;
    private LevelThreePlayer player;
    private MainThree main;
    private Animation _animation;


    private void Awake()
    {
        //reference components
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<LevelThreePlayer>();
        main = GameObject.Find("Main Camera").GetComponent<MainThree>();
       

    }


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingPosition.y += 1f;

        destinationPosition = transform.position;
        destinationPosition.y -= 1f;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        //method calls
        SeaBombMovement();
    }

    //bomb's movement
    private void SeaBombMovement()
    {
        if (canMove)
        {
            transform.Translate(moveDirection * Time.smoothDeltaTime);
            if (transform.position.y >= startingPosition.y)
            {
                moveDirection = Vector3.down * movementSpeed;

            }
            else if (transform.position.y <= destinationPosition.y)
            {
                moveDirection = Vector3.up * movementSpeed;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Level3Tags.LevelThreePlayer)
        {
            canExplode = true;
            main.ExplosionAudio();
            anim.SetBool("BombExplode", true);
            player.PlayerDamaged();
            Destroy(this.gameObject, 1.5f);
           // Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == Tags.flameBulletTag)
        {
            canExplode = true;
            main.ExplosionAudio();
            anim.SetBool("BombExplode", true);
            Destroy(this.gameObject, 1.5f);
            Destroy(collision.gameObject);
        }
    }








}//end of clas
