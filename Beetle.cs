//behavior for the beetle enemy: move forward, damage player, get destroyed by player's flame bullet

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : MonoBehaviour
{
    //variables
    [SerializeField] private float _movementSpeed = 3f;
    private bool canMove = true;
    private bool beetleDead = false;

    //reference variables
    private Animator _anim;
    private GameObject _beetle;
    private Player _player;
    private Main _main;


    private void Awake()
    {
        //reference components
        _anim = GetComponent<Animator>();
        _beetle = GameObject.Find("Beetle Enemy");
        _player = GameObject.Find("Player").GetComponent<Player>();
        _main = GameObject.Find("Main Camera").GetComponent<Main>();
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        

       
    }

    private void FixedUpdate()
    {
        BeetleMovement();
    }

    //beetle movement
    private void BeetleMovement()
    {
        if (_beetle != null)
        {
            canMove = true;
            transform.Translate(Vector2.left * _movementSpeed * Time.deltaTime);
        }
       
    }

    //damage the player when collided
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _player.PlayerDamaged();
        }
    }

    //gets damaged when colliding with the player's flame bullet
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.tag == Tags.flameBulletTag)
        {
             canMove = false;
            _main.EnemyDestroyedSound();
            //destroy flame bullet
            Destroy(trigger.gameObject);
            
            beetleDead = true;
             //destroy beetle game object
           
            if(beetleDead == true)
            {
                Destroy(this.gameObject, 0.1f);
               
            }
           
          
        }
    }

    




} //class
