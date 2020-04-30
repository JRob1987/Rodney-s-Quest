using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdStone : MonoBehaviour
{
    private PlayerTwo player;
   


    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerTwo>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == LevelTwoTags.PlayerTwo)
        {
            player.PlayerDamaged();
        }
        this.gameObject.SetActive(false);
    }












} //end class
