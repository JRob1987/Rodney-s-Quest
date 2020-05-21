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
    private bool canFire = false;
    [SerializeField] private int _playerLives = 3;
    private bool hasBeenDamaged = false;

    //reference variables
    private Rigidbody2D _body;
    private Animator _anim;
    private MainThree _main;
    private Renderer render;

    private void Awake()
    {
        //reference components
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _main = GameObject.Find("Main Camera").GetComponent<MainThree>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
            ChangeHorizonalDirection(1);
        }
        else if(horizontalMovement < 0)
        {
            _body.velocity = new Vector2(-movementSpeed, _body.velocity.y);
            ChangeHorizonalDirection(-1);
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

    private void ChangeHorizonalDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    private void ShootFlameBullet()
    {
        if (Input.GetMouseButtonDown(0) && transform.localScale.x == 1)
        {
            canFire = true;
            _main.ShootFlameBulletAudio();
            Instantiate(_flameBulletPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            


        }
        else if(Input.GetMouseButtonDown(0) && transform.localScale.x == -1)
        {
            canFire = true;
            _main.ShootFlameBulletAudio();
            Instantiate(_flameBulletReversePrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
    }

    public void PlayerDamaged()
    {
        StartCoroutine(FlashWhenDamaged(0.1f));
        _playerLives--;
        if(_playerLives < 1)
        {
            Destroy(this.gameObject);
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







} //end of class
