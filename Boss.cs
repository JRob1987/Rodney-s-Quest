using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    //variables
    private Vector3 movementDirection = Vector3.left;
    private Vector3 originPosition;
    private Vector3 movePosition;
    private bool canMove = false;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private int health = 300;
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


    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
        originPosition.x += 4f;

        movePosition = transform.position;
        movePosition.x -= 4f;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x >= 135)
        {
            uiManager.EnableBossUI();
            Movement();
            StartCoroutine(FireBallShoot(Random.Range(0, 4)));
        }
        
    }

    private void Movement()
    {
        if (canMove)
        {
            transform.Translate(movementDirection * _speed * Time.smoothDeltaTime);
            if (transform.position.x >= originPosition.x)
            {
                movementDirection = Vector3.left;
                ChangeDirection(7f);
                ghostFireBall.SetActive(true);
            }
            else if (transform.position.x <= movePosition.x)
            {
                movementDirection = Vector3.right;
                ChangeDirection(-7f);
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
            //Instantiate(ghostFireBall, transform.position + Vector3.left, Quaternion.identity);
            Instantiate(ghostFireBall, new Vector3(transform.position.x, transform.position.y - 1.5f, 0) + Vector3.left, Quaternion.identity);



        }


    }

    //boss's damage system
    void Damaged()
    {
        StartCoroutine(FlashWhenDamaged(0.1f));
        health -= 50;
        uiManager.UpdateBossHealthText(health);
        if (health < 1)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == LevelTwoTags.PlayerTwo)
        {
            player.PlayerDamaged();
        }
        else if (collision.tag == Tags.flameBulletTag)
        {
            main.GetDamagedSound();
            Destroy(collision.gameObject);
            Damaged();
        }
    }

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
