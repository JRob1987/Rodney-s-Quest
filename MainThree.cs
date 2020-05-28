using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainThree : MonoBehaviour
{
    //variables
    [SerializeField] private AudioClip _shoot;
    [SerializeField] private AudioClip _coins;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //plays sound when player shoots flame bullet
    public void ShootFlameBulletAudio()
    {
        AudioSource.PlayClipAtPoint(_shoot, transform.position);
    }

    //coin collect audio
    public void CoinCollect()
    {
        AudioSource.PlayClipAtPoint(_coins, transform.position);
    }
}
