using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLevelTwo : MonoBehaviour
{

    [SerializeField] private Transform targetA, targetB;
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private bool _switching = false;
    [SerializeField] private GameObject _player;
    private PlayerTwo _playerTwo;

    
    
    
    void Awake()
    {
        //layer = GameObject.Find("Player");
        _playerTwo = GameObject.Find("Player").GetComponent<PlayerTwo>();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //method call
        Movement();

    }



    //spider movement
    private void Movement()
    {
        if (_switching == false)
        {
           transform.position = Vector3.MoveTowards(transform.position, targetB.position, _speed * Time.deltaTime);
        }
        else if (_switching == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetA.position, _speed * Time.deltaTime);
        }

        if (transform.position == targetB.position)
        {
            _switching = true;
        }
        
        else if (transform.position == targetA.position)
        {
            _switching = false;
        }
    }

    //collision with player
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == LevelTwoTags.PlayerTwo)
        {
            _playerTwo.PlayerDamaged();
          //Destroy(other.gameObject);
        }
    }
    
}
