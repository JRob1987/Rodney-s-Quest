using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleEnemyTwo : MonoBehaviour
{

    //variables
    [SerializeField] private float _speed = 3f;
    [SerializeField] private Transform targetA, targetB;
    [SerializeField] private bool _switchDirection = false;

    //reference variables
    private PlayerTwo player;
    [SerializeField] private GameObject _flameBulletPrefab;
    


    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerTwo>();
        
    }
   
    private void FixedUpdate()
    {
        //method call
        BeetleMovement();
    }

    //beetle movement
    private void BeetleMovement()
    {
        if (_switchDirection == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetB.position, _speed * Time.deltaTime);
        }
        else if (_switchDirection == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetA.position, _speed * Time.deltaTime);
        }

        if (transform.position == targetB.position)
        {
            _switchDirection = true;
            ChangeDirection(-0.6f);
        }

        else if (transform.position == targetA.position)
        {
            _switchDirection = false;
            ChangeDirection(0.6f);
        }
    }

    //face the other direction
    void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    //collision with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == LevelTwoTags.PlayerTwo)
        {
            //damge the player
            player.PlayerDamaged();
        }
        
    }

    //collision with flame bullet
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == Tags.flameBulletTag)
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }




} //end of class
