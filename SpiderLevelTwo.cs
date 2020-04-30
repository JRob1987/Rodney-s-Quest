using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLevelTwo : MonoBehaviour
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
    private PlayerTwo player;
    private MainCameraTwo main;
    
    
    void Awake()
    {
        //reference components
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerTwo>();
        main = GameObject.Find("Main Camera").GetComponent<MainCameraTwo>();

        
        
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
        //method calls

        SpiderMovement();
    }

    
    //spider movement
    void SpiderMovement()
    {

        if (canMove)
        {
            transform.Translate(movementDirection * Time.smoothDeltaTime);
            if (transform.position.y >= originPosition.y)
            {
                movementDirection = Vector3.down * _speed;
                ChangeDirection(0.6f);
            }
            else if (transform.position.y <= movePosition.y)
            {
                movementDirection = Vector3.up * _speed;
                ChangeDirection(-0.6f);
            }
        }
    }

    //change direction
    void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.y = direction;
        transform.localScale = tempScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == LevelTwoTags.PlayerTwo)
        {
           Debug.Log("Player Damaged");
           player.PlayerDamaged();
        }
        else if(collision.gameObject.tag == Tags.flameBulletTag)
        {
            Debug.Log("Destroy!");
            main.GetDamagedSound();
            Destroy(this.gameObject, 1f);
            Destroy(collision.gameObject);
            anim.Play("SpiderDead");
            body.isKinematic = false;

        }
        
    }






}
