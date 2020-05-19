using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private MainCameraTwo _main;
   

    void Start()
    {
        _main = GameObject.Find("Main Camera").GetComponent<MainCameraTwo>();
    }

    //counts the number of coins collected by the player
   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == LevelTwoTags.PlayerTwo)
        {
           
            _main.CollectCoinsAudio();
            Destroy(this.gameObject);
        }
    }
}
