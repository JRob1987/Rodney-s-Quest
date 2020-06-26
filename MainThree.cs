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
    [SerializeField] private AudioClip _bossMusic;
    [SerializeField] private AudioSource _levelThreeMusic;
    

   

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

    //boss entrance music
    public void BossAudio()
    {
        AudioSource.PlayClipAtPoint(_bossMusic, transform.position);
    }

    //stop boss music
    public void StopLevelThreeMusic()
    {
        _levelThreeMusic.Stop();
    }

    

}
