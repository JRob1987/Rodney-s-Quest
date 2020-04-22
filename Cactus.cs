using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    private PlayerTwo _player;
    [SerializeField] private GameObject _flameBulletPrefab;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerTwo>();
       
        
    }

    //when collided with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == LevelTwoTags.PlayerTwo)
        {
            Destroy(collision.gameObject);
        }
          
                     

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Flame Bullet")
        {
            Destroy(other.gameObject);
        }
    }





}
