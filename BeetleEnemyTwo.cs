﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleEnemyTwo : MonoBehaviour
{

    //variables
    [SerializeField] private float _speed = 3f;
    private Vector3 movementDirection = Vector3.left;
    private Vector3 originPosition;
    private Vector3 movePosition;
    private bool canMove = false;

    //reference varaiables
    private Rigidbody2D body;
    private Animator anim;
    private PlayerTwo player;
    private MainCameraTwo main;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerTwo>();
        main = GameObject.Find("Main Camera").GetComponent<MainCameraTwo>();
    }

    private void Start()
    {
        originPosition = transform.position;
        originPosition.x += 2f;

        movePosition = transform.position;
        movePosition.x -= 14f;

        canMove = true;
    }

    private void Update()
    {
        BeetleMovement();
    }

    //movement
    private void BeetleMovement()
    {
        if (canMove)
        {
            transform.Translate(movementDirection * Time.smoothDeltaTime);
            if (transform.position.x >= originPosition.x)
            {
                movementDirection = Vector3.left;
                ChangeDirection(0.7f);
            }
            else if (transform.position.x <= movePosition.x)
            {
                movementDirection = Vector3.right;
                ChangeDirection(-0.7f);
            }
        }
    }

    //change direction while moving
    private void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == LevelTwoTags.PlayerTwo)
        {
            Debug.Log("Player Damaged");
            player.PlayerDamaged();
        }
        else if (collision.gameObject.tag == Tags.flameBulletTag)
        {
            Debug.Log("Destroy!");
            main.GetDamagedSound();
            Destroy(this.gameObject, 0.5f);
            Destroy(collision.gameObject);
            anim.SetBool("Dead", true);
           

        }

    }











} //end of class
