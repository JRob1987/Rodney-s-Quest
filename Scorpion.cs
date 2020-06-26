//behavior of the Scorpion boss: move forward, damage the player, get damaged by player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpion : MonoBehaviour
{
    //variables
    [SerializeField] private int _speed = 1;
    [SerializeField] private int _health = 100;
    [SerializeField] private GameObject flameBulletPrefab;
    private bool canMove;
    [SerializeField] private bool hasBeenDamaged;
    [SerializeField] private bool canAttack;

    //reference variables
    private Animator _anim;
    private Player _player;
    private SpriteRenderer render;
    private UIManager _uiManager;



    private void Awake()
    {
        //reference components
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        render = GameObject.Find("Scorpion Boss").GetComponent<SpriteRenderer>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

       if(canMove == true)
        {
            ScorpionMovement();
        }


    }
    public void EnableScorptionMovement()
    {
        canMove = true;
    }

    //scorpion's movement
    private void ScorpionMovement()
    {
        _uiManager.EnableBossUI();
        //_anim.Play("Walk Animation");
        _anim.SetBool("CanMove", true);
        transform.Translate(Vector2.left * _speed * Time.smoothDeltaTime);
       
    }

    //damage method for boss
    private void GetDamaged()
    {
        //take 20 damage from health
        _health = _health - 20;
        _uiManager.UpdateBossHealthText(_health);
        StartCoroutine(FlashWhenDamaged());


        //when health reaches 0, scorpion boss dies
        if(_health < 1)
        {
            canMove = false;
            _anim.SetBool("HasDied", true);
           //_anim.Play("Die animation");
            Destroy(this.gameObject, 1f);
            _health = 0;
        }

    }

    //when boss makes collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the collision is with the flame bullet then destroy the bullet, and damage the boss
        if(collision.tag == Tags.flameBulletTag)
        {
            Destroy(collision.gameObject);
            GetDamaged();
            
        }
        else if(collision.tag == Tags.playerTag)
        {
            AttackPlayer();
        }
    }

    //coroutine to flicker when scorpion takes damage
    IEnumerator FlashWhenDamaged()
    {
        hasBeenDamaged = true;
        if(hasBeenDamaged)
        {
            render.enabled = true;
            yield return new WaitForSeconds(0.1f);
            render.enabled = false;
            yield return new WaitForSeconds(0.1f);
        }
        render.enabled = true;
        
    }

    private void AttackPlayer()
    {
        _anim.SetBool("Attack", true);
         
    } 


} //end of class
