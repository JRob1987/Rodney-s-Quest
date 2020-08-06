//behavior for the beetle enemy: move forward, damage player, get destroyed by player's flame bullet

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    //turtle variables
    [SerializeField] private float _movementSpeed = 3.0f;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool turtleDead = false;

    //reference variables
    private Animator _animator;
    private Player _player;
    private Main _main;
    private GameObject _turtle;

    private void Awake()
    {
        //reference components
        _animator = GetComponent<Animator>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _main = GameObject.Find("Main Camera").GetComponent<Main>();
        _turtle = GameObject.Find("Turtle Enemy");
    }



    

    private void FixedUpdate()
    {
        //method call
        Movement();
    }

    //Turtle's  movement
    void Movement()
    {
        if (_turtle != null )
        {
            canMove = true;
            transform.Translate(Vector2.left * _movementSpeed * Time.deltaTime);
        }
        

    }

   

    //collision with the player's flame bullet
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.tag == Tags.flameBulletTag)
        {
            canMove = false;
            _main.EnemyDestroyedSound();
            Destroy(trigger.gameObject);
            turtleDead = true;

            if (turtleDead == true)
            {
                Destroy(this.gameObject, 0.1f);
            }
        }
        else if (trigger.gameObject.tag == Tags.playerTag)
        {
            _player.PlayerDamaged();
        }
            

    }
}
