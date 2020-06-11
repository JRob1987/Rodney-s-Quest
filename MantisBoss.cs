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
    private void Start()
    {
        _originPosition = transform.position;
        _originPosition.x += 4f;

        _movePosition = transform.position;
        _movePosition.x -= 10f;

        _canMove = true;
    }

    // Update is called once per frame
    private void Update()
    {
        //method calls
        MantisMovement();
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





} //end of class
