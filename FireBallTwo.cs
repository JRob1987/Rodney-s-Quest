using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallTwo : MonoBehaviour
{
    [SerializeField] private float fireBallSpeed = 10f;

    private Rigidbody2D body;
    private Animator anim;
    private Cactus cactus;

    private void Awake()
    {
        //reference to componenets
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cactus = GameObject.Find("Cactus").GetComponent<Cactus>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        FireOppositeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireOppositeDirection()
    {
        body.velocity = new Vector2(-fireBallSpeed, body.velocity.y);
        anim.Play("Flamebullet");
        Destroy(this.gameObject, 0.9f);
    }
}
