﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GhostPawn : MonoBehaviour
{
    //variables
    private Vector3 movementDirection = Vector3.left;
    private Vector3 originPosition;
    private Vector3 movePosition;
    private bool canMove = false;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private int health = 100;
    private bool hasBeenDamaged = false;
    [SerializeField] private GameObject ghostFireBall;
     private bool attacked = false;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float nextFire = 0.1f;
   
  


    //reference variables
    private Rigidbody2D body;
    private Animator anim;
    private PlayerTwo player;
    private FireBall flameBullet;
    private SpriteRenderer render;
    private MainCameraTwo main;
    private UIManagerTwo uiManager;
    

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerTwo>();
        render = GetComponent<SpriteRenderer>();
        main = GameObject.Find("Main Camera").GetComponent<MainCameraTwo>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManagerTwo>();

}

    private void Start()
    {
        originPosition = transform.position;
        originPosition.x += 8f;

        movePosition = transform.position;
        movePosition.x -= 8f;

        canMove = true;
    }

    private void Update()
    {
        
        if(player.transform.position.x >= 86)
        {
            Movement();
            StartCoroutine(FireBallShoot(Random.Range(0,3)));
        }   

             

    }

    private void Movement()
    {
        if (canMove)
        {
            transform.Translate(movementDirection * _speed *  Time.smoothDeltaTime);
            if (transform.position.x >= originPosition.x)
            {
                movementDirection = Vector3.left;
                ChangeDirection(3.8f);
                ghostFireBall.SetActive(true);
            }
            else if (transform.position.x <= movePosition.x)
            {
                movementDirection = Vector3.right;
                ChangeDirection(-3.8f);
                ghostFireBall.SetActive(false);
            }
           
        }


    }

    private void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    

    //enemy shooting fireball
    IEnumerator FireBallShoot(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (Time.time > nextFire)
           {
            
            nextFire = Time.time + fireRate;
            Instantiate(ghostFireBall, transform.position + Vector3.left, Quaternion.identity);
               


          }            
       

    }

    //ghost collides with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == LevelTwoTags.PlayerTwo)
        {
            player.PlayerDamaged();
        }
        else if(collision.tag == Tags.flameBulletTag)
        {
            main.GetDamagedSound();
            Destroy(collision.gameObject);
            Damaged();
        }
    }

    //ghost enemy health damaged
    private void Damaged()
    {
        StartCoroutine(FlashWhenDamaged(0.1f));
        health -= 50;
       if (health < 1)
        {
            Destroy(this.gameObject);
            
        }
        
    }

    //coroutine for player to flash when damaged
    IEnumerator FlashWhenDamaged(float seconds)
    {
        hasBeenDamaged = true;
        if (hasBeenDamaged)
        {
            render.enabled = true;
            yield return new WaitForSeconds(seconds);
            render.enabled = false;
            yield return new WaitForSeconds(seconds);
        }
        render.enabled = true;

    }

   



}
