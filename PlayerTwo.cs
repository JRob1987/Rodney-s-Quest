using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
    public int lives = 3;
    private Vector3 facingRight;
    private Vector3 facingLeft;
    private bool hasBeenDamaged = false;
   [SerializeField] private GameObject coinPrefab;
   public int coinsCollected = 0;
    
    
  

    //jumping
    private bool jumped;
    [SerializeField] private float jumpForce = 15f;

    //flame bullet
    [SerializeField] private GameObject _flameBulletPrefab;
    [SerializeField] private float _flameBulletSpeed = 3f;
    [SerializeField] private GameObject _flameBulletPrefabReverse;
     private bool canFire = false;

   
    
    //reference variables
    private Rigidbody2D body;
    private Animator anim;
    private MainCameraTwo main;
    private SpriteRenderer render;
    private UIManagerTwo uiManager;
    private LevelLoader levelLoader;






    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        main = GameObject.Find("Main Camera").GetComponent<MainCameraTwo>();
        render = GetComponent<SpriteRenderer>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManagerTwo>();
        levelLoader = GameObject.Find("Levels GameObject").GetComponent<LevelLoader>();


    }

    
    // Update is called once per frame
    void Update()
    {
       

        if(player != null)
        {
            CheckIfPlayerGrounded();
            PlayerWalk();
            Jump();
            ShootFireBall();
            PauseGame();

        }
        else
        {
            Debug.LogError("Player is Null!");
        }
       
    }

    //enables player to pause the game
    private void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            uiManager.PauseGameText();
            //pauses the game
            Time.timeScale = 0;
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            uiManager.ClearPauseGameText();
            //continue game play
            Time.timeScale = 1;
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
            ChangeDirection(1.52f);
            
            
        }
        else if (horizontalMovement < 0)
        {

            body.velocity = new Vector2(-speed, body.velocity.y);
            ChangeDirection(-1.52f);
           
            
           
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
    void ChangeDirection(float direction)
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
        if (isGrounded)
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
        if(isGrounded == true)
        {
            if (Input.GetMouseButtonDown(0) && transform.localScale.x == 1.52f)
            {

                canFire = true;
                main.ShootFlameBulletAudio();
                anim.SetBool("Fire", true);
                Instantiate(_flameBulletPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                


            }
            else if (Input.GetMouseButtonDown(0) && transform.localScale.x == -1.52f)
            {
                
                canFire = true;
                main.ShootFlameBulletAudio();
                anim.SetBool("Fire", true);
                Instantiate(_flameBulletPrefabReverse, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            }


            
           
        }
        
    }
      
    
    //player Damaged
    public void PlayerDamaged()
    {
        StartCoroutine(FlashWhenDamaged(0.1f));
        main.GetDamagedSound();
        lives = lives - 1;
        uiManager.UpdatePlayerLivesUIText(lives);
        if (lives < 1)
        {
            main.GameOverClip();
            uiManager.DisplayGameOverText();
            Destroy(this.gameObject);
            levelLoader.LoadMainMenuScene();
           //ime.timeScale = 0;
        }
    }

   

    //coroutine for player to flash when damaged
    IEnumerator FlashWhenDamaged(float seconds)
    {
        hasBeenDamaged = true;
        if (hasBeenDamaged)
        {
            render.enabled = true;
            yield return new WaitForSeconds(seconds);
            render.enabled = false;
            yield return new WaitForSeconds(seconds);
        }
        render.enabled = true;

    }

    IEnumerator LoadLevelThreeDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //levelLoader.LoadLevelThreeScene();
        levelLoader.LoadGamePlayScenes(3);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Tags.coins)
        {
            main.CollectCoinsAudio();
            coinsCollected = coinsCollected + 1;
            uiManager.UpdateCoinsCollectedText(coinsCollected);
            if(coinsCollected == 10)
            {
                lives++;
                uiManager.UpdatePlayerLivesUIText(lives);
            }
        }
        else if(collision.tag == LevelTwoTags.Fireball)
        {
            PlayerDamaged();
            Destroy(collision.gameObject);
        }
        else if(collision.tag == Tags.castle)
        {
            main.StopLevelTwoSong();
            main.LevelClearSound();
            uiManager.DisplayLevelCompleteText();
            StartCoroutine(LoadLevelThreeDelay(5f));
        }
    }








} //end class
