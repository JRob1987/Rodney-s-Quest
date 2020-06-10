using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCrab : MonoBehaviour
{
    //variables
    [SerializeField] private float _movementSpeed = 2f;
    private Vector3 movementDirection = Vector3.left;
    private Vector3 originPosition;
    private Vector3 movePosition;
    private bool canMove = false;
    [SerializeField] private GameObject _sawPrefab;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float nextFire = 0.1f;

    //reference variables
    private Rigidbody2D _body;
    private Animator _anim;
    private LevelThreePlayer _player;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _player = GameObject.Find("Player").GetComponent<LevelThreePlayer>(); 
    }


    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
        originPosition.x += 6f;

        movePosition = transform.position;
        movePosition.x -= 11f;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        //method calls
        if(_player.transform.position.x >= 78)
        {
            CrabMovement();
            StartCoroutine(FireBallShoot(Random.Range(0, 3)));
        }
        
    }

    //Fish Movement
    void CrabMovement()
    {
        if (canMove)
        {
            transform.Translate(movementDirection * _movementSpeed * Time.smoothDeltaTime);
            if (transform.position.x >= originPosition.x)
            {
                movementDirection = Vector3.left;
                ChangeDirection(1f);
            }
            else if (transform.position.x <= movePosition.x)
            {
                movementDirection = Vector3.right;
                ChangeDirection(1f);
            }
        }
    }

    //change direction while moving
    private void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.gameObject.tag == Tags.flameBulletTag)
        {
            Destroy(this.gameObject);
            Destroy(trigger.gameObject);
        }
        else if(trigger.gameObject.tag == Level3Tags.LevelThreePlayer)
        {
            _player.PlayerDamaged();
        }
    }

    //enemy shooting fireball
    IEnumerator FireBallShoot(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (Time.time > nextFire)
        {

            nextFire = Time.time + fireRate;
            Instantiate(_sawPrefab, transform.position + Vector3.up, Quaternion.identity);



        }


    }





} //end of class
