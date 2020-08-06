//behavior for the beetle enemy: move forward, damage player, get destroyed by player's flame bullet

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : MonoBehaviour
{
    //variables that can be see in the inspector
    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField] private GameObject _beetlePrefab;
    
    //variables not visible in the inspector
    private Animator _anim;
    private Player _player;
    private Main _main;
    private bool canMove;
   


    private void Awake()
    {
        //reference components
        _anim = GetComponent<Animator>();
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
        if(_beetlePrefab != null)
        {
            //method call
            BeetleMovement();
        }
        else
        {
            //error
            Debug.LogError("Beetle enemy is null!");
        }       

    }

    //Beetle's movement
    private void BeetleMovement()
    {           
      canMove = true;
      transform.Translate(Vector2.left * _movementSpeed * Time.deltaTime);      
       
    }           
    //Beetle get's destroyed by player's flame bullet, and damages the player when collides with player
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.tag == Tags.flameBulletTag)
        {
             canMove = false;
            _main.EnemyDestroyedSound();
             Destroy(trigger.gameObject);
            _anim.SetBool("Dead", true);
             Destroy(this.gameObject, 0.3f);          
                             
        }
        else if(trigger.tag == Tags.playerTag)
        {
            _player.PlayerDamaged();
        }
    }

    




} //class
