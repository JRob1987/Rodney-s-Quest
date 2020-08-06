//keeps track of audio in game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private AudioSource _levelSong;
    [SerializeField] private AudioClip _shootFireballClip;
    [SerializeField] private AudioClip _playerJump;
    [SerializeField] private AudioClip _coinCollect;
    [SerializeField] private AudioClip _enemyDestroyedClip;
    [SerializeField] private AudioClip _castleClip;
    [SerializeField] private AudioClip _gameOverClip;

    
    //shoot fireball
    public void ShootFireBallSound()
    {
        AudioSource.PlayClipAtPoint(_shootFireballClip, transform.position);
    }

    //player jump sound
    public void PlayerJumpSound()
    {
        AudioSource.PlayClipAtPoint(_playerJump, transform.position);
    }

    //coin collect sound
    public void CoinCollectSound()
    {
        AudioSource.PlayClipAtPoint(_coinCollect, transform.position);
    }

    //enemy destroyed sound

    public void EnemyDestroyedSound()
    {
        AudioSource.PlayClipAtPoint(_enemyDestroyedClip, transform.position);
    }


    //player reaches castle
    public void PlayerReachesCastleSound()
    {
        AudioSource.PlayClipAtPoint(_castleClip, transform.position);
    }

    public void StopLevelSong()
    {
        _levelSong.Stop();
    }

    public void GameOverSound()
    {
        AudioSource.PlayClipAtPoint(_gameOverClip, transform.position);
    }

}
