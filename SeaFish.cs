//behavior of fish enemy: move in both directions, take damage by flame bullet, damage the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaFish : MonoBehaviour
{
    //variables
    [SerializeField] private float _movementSpeed = 3f;
    private Vector3 movementDirection = Vector3.left;
    private Vector3 originPosition;
    private Vector3 movePosition;
    private bool canMove = false;

    //reference variables
    private Rigidbody2D _body;
    private Animator _anim;
    private LevelThreePlayer _player;
    private MainThree _main;
    

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _player = GameObject.Find("Player").GetComponent<LevelThreePlayer>();
        _main = GameObject.Find("Main Camera").GetComponent<MainThree>();
    }

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
        originPosition.x += 4f;

        movePosition = transform.position;
        movePosition.x -= 11f;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        //method call
        FishMovement();
    }

    //Fish Movement
    void FishMovement()
    {
        if (canMove)
        {
            transform.Translate(movementDirection * _movementSpeed * Time.smoothDeltaTime);
            if (transform.position.x >= originPosition.x)
            {
                movementDirection = Vector3.left;
                ChangeDirection(0.8f);
            }
            else if (transform.position.x <= movePosition.x)
            {
                movementDirection = Vector3.right;
                ChangeDirection(-0.8f);
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

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == Tags.flameBulletTag)
        {
            _main.DamageAudio();
            Destroy(target.gameObject);
            Destroy(this.gameObject);

        }
        else if(target.tag == Level3Tags.LevelThreePlayer)
        {
            _player.PlayerDamaged();
            Destroy(this.gameObject);
        }
    }



} // end of class
