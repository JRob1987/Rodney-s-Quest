using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTwo : MonoBehaviour
{
    //variables
    [SerializeField] private GameObject player;
    [SerializeField] private float speed = 5f;
    private float horizontalMovement; 
    [SerializeField] Transform groundCheckPosition;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private int lives = 3;

    //jumping
    private bool jumped;
    [SerializeField] private float jumpForce = 15f;

    //flame bullet
    [SerializeField] private GameObject _flameBulletPrefab;
    private bool canFire;

   
    
    //reference variables
    private Rigidbody2D body;
    private Animator anim;
    private MainCameraTwo main;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        main = GameObject.Find("Main Camera").GetComponent<MainCameraTwo>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerGrounded();
        Jump();
        ShootFireBall();
    }

    private void FixedUpdate()
    {
        if(player != null)
        {
            PlayerWalk();
            PlayerBounds();
            
        }
    }

    //player movement
    void PlayerWalk()
    {
        horizontalMovement = Input.GetAxis("Horizontal");

        //if the player moves to the right or to the left
        if (horizontalMovement > 0)
        {

            body.velocity = new Vector2(speed, body.velocity.y);
            ChangeDirection(1);
            _flameBulletPrefab.SetActive(true);
        }
        else if (horizontalMovement < 0)
        {

            body.velocity = new Vector2(-speed, body.velocity.y);
            ChangeDirection(-1);
            anim.SetBool("Fire", false);
            _flameBulletPrefab.SetActive(false);
        }
        //prevents player from sliding when movement stops
        else
        {
            body.velocity = new Vector2(0f, body.velocity.y);
        }

        //triggers transitions between the idle, and walk animation state. Parameter name is speed
        anim.SetInteger("Speed", Mathf.Abs((int)body.velocity.x));
    }

    //player changing direction 
    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    //check if the player is on the ground
    void CheckIfPlayerGrounded()
    {
        //cast a ray from this position in this direction, length of the ray, search for gameobjects on the ground layer
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

        if (isGrounded == true)
        {
            //and we jumped before
            if (jumped)
            {
                jumped = false;
                //setting jump animator parameter to false
                anim.SetBool("Jump", false);
            }
            else if (canFire)
            {
               canFire = false;
                anim.SetBool("Fire", false);
            }
        }
    }

    //player jump
    void Jump()
    {
        if (isGrounded == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                main.PlayerJumpAudio();
                body.velocity = new Vector2(body.velocity.x, jumpForce); //adding velocity to the rigid body, 
                anim.SetBool("Jump", true); //setting jump animator parameter to true
            }
        }
    }

    void ShootFireBall()
    {
        if(isGrounded)
        {
            if(Input.GetMouseButtonDown(0))
            {
                canFire = true;
                main.ShootFlameBulletAudio();
                anim.SetBool("Fire", true);
                StartCoroutine(ShootFireballDelay(0.1f));

            }
        }
    }

    IEnumerator ShootFireballDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Instantiate(_flameBulletPrefab, new Vector2(transform.position.x + 0.75f, transform.position.y), Quaternion.identity);


    }

    //player Damaged
    public void PlayerDamaged()
    {
        lives = lives - 1;
        if(lives < 1)
        {
            Destroy(this.gameObject);
        }
    }

    void PlayerBounds()
    {
        if (transform.position.x <= -6.7f)
        {
            transform.position = new Vector2(-6.7f, transform.position.y);
        }

    }






} //end class
