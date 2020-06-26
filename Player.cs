//script defines the behavior of Rodney in the game: walk, jump, shoot flame bullet, trigger boss movement, damage boss, collect coins, damage enemies 

//namespaces
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//variables that can be seen in inspector
	[SerializeField] private GameObject player;
	[SerializeField] private float speed = 5f;
	[SerializeField] private bool isGrounded;
	[SerializeField] private float jumpForce;
	[SerializeField] private GameObject flameBulletPrefab;
	[SerializeField] private bool canFire;	
	[SerializeField] private int playerLives = 3;
	[SerializeField] private int coinsCollected = 0;
	[SerializeField] Transform groundCheckPosition;
	[SerializeField] LayerMask groundLayer;

	//variables that are hidden from the inspector
	private bool jumped;
	private float _xPos;
	private Scorpion _scorpion;
	private GameObject trampoline;


	//reference variables
	private Rigidbody2D body;
	private Animator anim;
	private Main _mainCamera;
	private UIManager _uiManager;
	private LevelLoader _levelLoader;


	void Awake()
	{
		//reference to components
		body = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		_mainCamera = GameObject.Find("Main Camera").GetComponent<Main>();
		_scorpion = GameObject.Find("Scorpion Boss").GetComponent<Scorpion>();
		trampoline = GameObject.Find("Trampoline");
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		_levelLoader = GameObject.Find("Levels GameObject").GetComponent<LevelLoader>();
				
	}



	// Use this for initialization
	void Start () 
	{
		isGrounded = true;
		if(_uiManager == null)
		{
			Debug.LogError("UI manager is null");
		}
		
		
				
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(player != null)
        {
			//method calls
			PlayerWalking();
			PlayerBounds();
			CheckIfPlayerGrounded();
			Jump();
			ShootFireBall();
			
		}
		else
        {
			Debug.LogError("Player is null!");
        }
		
		
	}
	
	//player walking
	void PlayerWalking()
	{
		
		//enabling horizontal movement using the a,d,left, right arrow keys
		float hMovement = Input.GetAxisRaw("Horizontal");
		
		//if the player moves to the right or to the left
		if(hMovement > 0)
		{
			
			body.velocity = new Vector2(speed, body.velocity.y);
			ChangeDirection(1);
		}
		else if(hMovement < 0)
		{
			
			body.velocity = new Vector2(-speed, body.velocity.y);
			ChangeDirection(-1);
		}
		//prevents player from sliding when movement stops
		else
		{
			body.velocity = new Vector2(0f, body.velocity.y);
		}

		//triggers transitions between the idle, and walk animation state. Parameter name is speed
		anim.SetInteger("Speed", Mathf.Abs((int)body.velocity.x));
		
		
		
	}

	void PlayerBounds()
	{
		if(transform.position.x <= -8.33f)
		{
			transform.position = new Vector2(-8.33f, transform.position.y);
		}
	  
	}
		
	

	//change player's direction
	void ChangeDirection(int direction)
	{
		Vector3 tempScale = transform.localScale;
		tempScale.x = direction;
		transform.localScale = tempScale;
		
	}
	//checks to verify if the player is grounded
	void CheckIfPlayerGrounded()
	{
		//cast a ray from this position in this direction, length of the ray, search for gameobjects on the ground layer
		isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

		if(isGrounded == true)
		{
			//and we jumped before
			if(jumped)
			{
				jumped = false;
				//setting jump animator parameter to false
				anim.SetBool("Jump", false);
			}
			else if(canFire)
			{
				canFire = false;
				anim.SetBool("Fire", false);
			}
		}
	}

	//jump method
	void Jump()
	{
		if(isGrounded == true)
		{
			if(Input.GetKey(KeyCode.Space))
			{
				jumped = true;
				_mainCamera.PlayerJumpSound();
				body.velocity = new Vector2(body.velocity.x, jumpForce); //adding velocity to the rigid body
				//setting jump animator parameter to true
				anim.SetBool("Jump", true);
			}
		}
	}

	//enables player to shoot a fireball
	void ShootFireBall()
	{	
		if(isGrounded == true)
		{
			if (Input.GetMouseButtonDown(0))
			{
				
				canFire = true;
				_mainCamera.ShootFireBallSound();
				anim.SetBool("Fire", true);
				StartCoroutine(ShootFireballDelay(0.1f));

			}
		}		
				
		
	}

	//adds a slight delay between firings
	IEnumerator ShootFireballDelay(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		Instantiate(flameBulletPrefab, new Vector2(transform.position.x + 0.75f, transform.position.y), Quaternion.identity);

			   
	}

	//player collisions with tramploline
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag ==  "Trampoline")
		{
			//trigger scorpion movement
			_scorpion.EnableScorptionMovement();
		}
		

    }

	//player damaged method
	public void PlayerDamaged()
	{
		playerLives--;
		_uiManager.UpdatePlayerLivesUIText(playerLives);
		if (playerLives < 1)
		{
			Destroy(this.gameObject);
			gameObject.SetActive(false);
		}
	}
	
	//increases the amount of coins player collects
	private void CoinsCollected()
	{
		//increment coins collected by 1
		coinsCollected++;
		_uiManager.UpdateCoinsCollectedText(coinsCollected);

	}

	//collision detection against the coins, scorpion boss, and the castle 
	private void OnTriggerEnter2D(Collider2D trigger)
	{
		if(trigger.tag == Tags.coins)
		{
			_mainCamera.CoinCollectSound();
			Destroy(trigger.gameObject);
			CoinsCollected();
		}
		else if (trigger.gameObject.tag == Tags.scorpionBoss)
		{
			//damage player
			PlayerDamaged();
		}
		else if(trigger.tag == Tags.castle)
		{
			_mainCamera.StopLevelSong();
			_mainCamera.PlayerReachesCastleSound();
			_uiManager.DisplayLevelCompleteText();
			//loads level 2 after 5 seconds
			StartCoroutine(LoadLevelTwoDelay(5f));
		}

		

	}

	//delays the time for level 2 to load
	IEnumerator LoadLevelTwoDelay(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		_levelLoader.LoadGamePlayScenes(2);
	}






} //class
