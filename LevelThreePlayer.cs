using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThreePlayer : MonoBehaviour
{
    //variables
    [SerializeField] private GameObject _player;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private GameObject _flameBulletPrefab;
    [SerializeField] private GameObject _flameBulletReversePrefab;
    [SerializeField] private bool canFire = false;
    [SerializeField] private int _playerLives = 3;
    private bool hasBeenDamaged = false;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private int _coinCount = 0;
    

    //reference variables
    private Rigidbody2D _body;
    private Animator _anim;
    private MainThree _main;
    private SpriteRenderer render;
    private UIManagerLevelThree _uiManager;
    private LevelLoader _levelLoader;

    private void Awake()
    {
        //reference components
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _main = GameObject.Find("Main Camera").GetComponent<MainThree>();
        render = GetComponent<SpriteRenderer>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManagerLevelThree>();
        _levelLoader = GameObject.Find("Levels GameObject").GetComponent<LevelLoader>();
        

    }

   
    // Update is called once per frame
    void Update()
    {
        ShootFlameBullet();
        
    }

    private void FixedUpdate()
    {
        if (_player != null)
        {
            //method call
            Movement();
        }
    }

    //player movement
    private void Movement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        //horizontal movement
        if(horizontalMovement > 0)
        {
            _body.velocity = new Vector2(movementSpeed, _body.velocity.y);
            ChangeHorizonalDirection(1.52f);
        }
        else if(horizontalMovement < 0)
        {
            _body.velocity = new Vector2(-movementSpeed, _body.velocity.y);
            ChangeHorizonalDirection(-1.52f);
        }
        //prevents player from sliding when movement stops
        else
        {
            _body.velocity = new Vector2(0f, _body.velocity.y);
           
        }

        //vertical movement
        if(verticalMovement > 0)
        {
            _body.velocity = new Vector2(_body.velocity.x, movementSpeed);
        }
        else if(verticalMovement < 0)
        {
            _body.velocity = new Vector2(_body.velocity.x, -movementSpeed);
        }

        //prevents player from sliding when movement stops
        else
        {
            _body.velocity = new Vector2(_body.velocity.x, 0f);
        }


    }

    private void ChangeHorizonalDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    private void ShootFlameBullet()
    {
        if (Input.GetMouseButtonDown(0) && transform.localScale.x == 1.52f)
        {
            canFire = true;
           // _anim.SetBool("Shoot", true);
            _main.ShootFlameBulletAudio();
            Instantiate(_flameBulletPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            


        }
        else if(Input.GetMouseButtonDown(0) && transform.localScale.x == -1.52f)
        {
            canFire = true;
            //_anim.SetBool("Shoot", true);
            _main.ShootFlameBulletAudio();
            Instantiate(_flameBulletReversePrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
       
    }

    public void PlayerDamaged()
    {
        _main.DamageAudio();
        StartCoroutine(FlashWhenDamaged(0.1f));
        _playerLives--;
        _uiManager.UpdatePlayerLivesUI(_playerLives);
       if (_playerLives < 1)
        {
            _main.StopLevelThreeMusic();
            _main.GameOverAudio();
            _uiManager.DisplayGameOverText();
            Destroy(this.gameObject);
            Time.timeScale = 0;
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

    //update player lives when collecting a certain amount of coins
    private void UpdatePlayerLives()
    {
        //every 10 coins player collects, add 1 life.
        if(_coinCount % 10 == 0)
        {
            _playerLives = _playerLives + 1;
            _uiManager.UpdatePlayerLivesUI(_playerLives);
        }
    }

    IEnumerator LoadLevelFourDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _levelLoader.LoadLevelFourScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == Level3Tags.coins)
        {
            _main.CoinCollect();
            _coinCount = _coinCount + 1;
            _uiManager.UpdateCoinCollectedUI(_coinCount);
            Destroy(collision.gameObject);
            UpdatePlayerLives();
        }
        else if(collision.gameObject.tag == Tags.castle)
        {
            _main.StopLevelThreeMusic();
            _main.CastleFinish();
            _uiManager.DisplayLevelCompleteText();
            _uiManager.HideImagesAndText();
            StartCoroutine(LoadLevelFourDelay(5f));
            Debug.Log("Level 3 Complete!");
        }

    }

   







} //end of class
