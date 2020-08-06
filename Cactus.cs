using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    private PlayerTwo _player;
    [SerializeField] private GameObject _flameBulletPrefab;
    private UIManagerTwo _uiManagerTwo;
    private MainCameraTwo _mainTwo;
    private LevelLoader levelLoader;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerTwo>();
        _uiManagerTwo = GameObject.Find("Canvas").GetComponent<UIManagerTwo>();
        _mainTwo = GameObject.Find("Main Camera").GetComponent<MainCameraTwo>();
        levelLoader = GameObject.Find("Levels GameObject").GetComponent<LevelLoader>();


    }

    //when collided with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == LevelTwoTags.PlayerTwo)
        {
            _mainTwo.GameOverClip();
            _uiManagerTwo.DisplayGameOverText();
             Destroy(collision.gameObject);
            levelLoader.LoadMainMenuScene();
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
