using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraTwo : MonoBehaviour
{
    //variables
    private AudioSource _source;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _shootFlameBulletSound;
    [SerializeField] private AudioClip _coinCollectSound;
   

    // Start is called before the first frame update
    void Start()
    {
        _source = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //player jump sound
    public void PlayerJumpAudio()
    {
        AudioSource.PlayClipAtPoint(_jumpSound, transform.position);
    }

    //shoot flame bullet
    public void ShootFlameBulletAudio()
    {
        AudioSource.PlayClipAtPoint(_shootFlameBulletSound, transform.position);
    }

    public void CollectCoinsAudio()
    {
        AudioSource.PlayClipAtPoint(_coinCollectSound, transform.position);
    }


    

} //end of class
