using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainThree : MonoBehaviour
{
    //variables
    [SerializeField] private AudioClip _shoot;
    [SerializeField] private AudioClip _coins;
    [SerializeField] private AudioClip _castleFinish;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip _gameOverSound;
    [SerializeField] private AudioClip _explosionSound;

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

    //castle finish audio
    public void CastleFinish()
    {
        AudioSource.PlayClipAtPoint(_castleFinish, transform.position);
    }
    
    //damaged audio
    public void DamageAudio()
    {
        AudioSource.PlayClipAtPoint(_damageSound, transform.position);
    }

    //game over audio when player dies
    public void GameOverAudio()
    {
        AudioSource.PlayClipAtPoint(_gameOverSound, transform.position);
    }

    //explosion audio
    public void ExplosionAudio()
    {
        AudioSource.PlayClipAtPoint(_explosionSound, transform.position);
    }

}
