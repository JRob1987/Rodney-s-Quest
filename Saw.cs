using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Saw : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 4f;
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private float _degrees = 360f;

    //reference variables
    private LevelThreePlayer _player;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<LevelThreePlayer>();
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SawMovement();
        
    }

    //saw movement
    private void SawMovement()
    {     
        transform.Translate(Vector3.up * _movementSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.back, _degrees * _rotationSpeed * Time.deltaTime);
        Destroy(this.gameObject, 1.5f);
               
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == Level3Tags.LevelThreePlayer)
        {
            _player.PlayerDamaged();
            Destroy(this.gameObject);
        }
    }







} //end class
