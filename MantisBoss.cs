using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisBoss : MonoBehaviour
{
    //variables
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private int _mantisHealth = 350;
    private Vector3 _movementDirection = Vector3.left;
    private Vector3 _originPosition;
    private Vector3 _movePosition;
    private bool _canMove = false;
    private bool _isDead = false;
    [SerializeField] private GameObject _stonePrefab;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private float _nextFire = 1f;
    private bool _hasBeenDamaged = false;
   


    //reference variables
    private Rigidbody2D _body;
    private Animator _anim;
    private LevelThreePlayer _player;
    private SpriteRenderer _render;
    private MainThree _main;
    private UIManagerLevelThree _uiManager;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _player = GameObject.Find("Player").GetComponent<LevelThreePlayer>();
        _render = GetComponent<SpriteRenderer>();
        _main = GameObject.Find("Main Camera").GetComponent<MainThree>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManagerLevelThree>();

    }

    // Start is called before the first frame update
    private void Start()
    {
        _originPosition = transform.position;
        _originPosition.x += 4f;

        _movePosition = transform.position;
        _movePosition.x -= 4f;

        _canMove = true;
    }

    // Update is called once per frame
    private void Update()
    {
        //method calls
        if(_player.transform.position.x >= 260)
        {
            
            
            _uiManager.EnableBossUI();
            MantisMovement();
            StartCoroutine(ShootStone(Random.Range(0, 3f)));
        }
        
    }

    //movement method
    private void MantisMovement()
    {
        if (_canMove == true)
        {
            transform.Translate(_movementDirection * _movementSpeed * Time.smoothDeltaTime);
            if (transform.position.x >= _originPosition.x)
            {
                _movementDirection = Vector3.left;
                //ChangeDirection(1f);
            }
            else if (transform.position.x <= _movePosition.x)
            {
                _movementDirection = Vector3.right;
               // ChangeDirection(1f);
            }
        }
    }

    private void MantisDamaged()
    {
        _mantisHealth = _mantisHealth - 50;
        StartCoroutine(FlashWhenDamaged(0.1f));
        _uiManager.UpdateBossHealthUI(_mantisHealth);
        if(_mantisHealth < 1)
        {
            _isDead = true;
            _anim.Play("MantisDead");
            Destroy(this.gameObject, 1f);
        }
    }
    //coroutine for player to flash when damaged
    IEnumerator FlashWhenDamaged(float seconds)
    {
        _hasBeenDamaged = true;
        if (_hasBeenDamaged)
        {
            _render.enabled = true;
            yield return new WaitForSeconds(seconds);
            _render.enabled = false;
            yield return new WaitForSeconds(seconds);
        }
        _render.enabled = true;

    }

    private IEnumerator ShootStone(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if(Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            Instantiate(_stonePrefab , transform.position + Vector3.down, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.tag == Level3Tags.LevelThreePlayer)
        {
            _player.PlayerDamaged();
        }
        else if(target.gameObject.tag == Tags.flameBulletTag)
        {
            _main.DamageAudio();
            MantisDamaged();
            Destroy(target.gameObject);
        }

        
    }





} //end of class
