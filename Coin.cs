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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == LevelTwoTags.PlayerTwo)
        {
            _main.CollectCoinsAudio();
            Destroy(this.gameObject);
        }
    }
}
