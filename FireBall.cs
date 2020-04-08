//script defines the behavior of the fireball: move forward, damage enemies, damage boss

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    [SerializeField] private float fireBallSpeed;
   

    //reference variables
    private Rigidbody2D body;
    private Animator anim;
   
    

    private void Awake()
    {
        //reference to componenets
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
       
       
    }
   
    private void FixedUpdate()
    {
        body.velocity = new Vector2(fireBallSpeed, body.velocity.y);
        anim.Play("Flamebullet");
    }

    








}
