using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerLevelThree : MonoBehaviour
{
    //variables
    [SerializeField] private Image _playerImage;
    [SerializeField] private Image _coinImage;
    [SerializeField] private Text _playerLivesText;
    [SerializeField] private Text _coinsCollectedText;
    [SerializeField] private Text _bossHealthText;
    [SerializeField] private Image _mantisImage;
    [SerializeField] private Text _levelCompleted;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _gamePausedText;


    private void Awake()
    {
        _mantisImage.enabled = false;
        _bossHealthText.enabled = false;
        _levelCompleted.enabled = false;
        _gameOverText.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerLivesText.text = "x : 3";
        _coinsCollectedText.text = "x: 0";
        _bossHealthText.text = "x: 350";
       
      
    }



    
    public void UpdateCoinCollectedUI(int coins)
    {
        _coinsCollectedText.text = "x: " + coins.ToString();
    }

    public void UpdatePlayerLivesUI(int lives)
    {
        _playerLivesText.text = "x: " + lives.ToString();
    }

    public void UpdateBossHealthUI(int health)
    {
        _bossHealthText.text = "x: " + health.ToString();
    }

    public void EnableBossUI()
    {
        _mantisImage.enabled = true;
        _bossHealthText.enabled = true;
    }

    public void DisplayLevelCompleteText()
    {
        _levelCompleted.enabled = true;
        StartCoroutine(BlinkingLevelCompleteText());
    }

   

    IEnumerator BlinkingLevelCompleteText()
    {
        while (true)
        {
            _levelCompleted.text = "";
            yield return new WaitForSeconds(0.5f);
            _levelCompleted.text = "Mantis has been defeated!";
            yield return new WaitForSeconds(0.5f);

        }
    }

    public void DisplayGameOverText()
    {
        _gameOverText.enabled = true;
        StartCoroutine(BlinkingGameOverText());
    }

    IEnumerator BlinkingGameOverText()
    {
        while (true)
        {
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "Game Over!!";
            yield return new WaitForSeconds(0.5f);

        }
    }

    public void DisplayGamePausedText()
    {
        _gamePausedText.text = "Game Paused. Click the R key to return to game play.";
    }

    public void ClearGamePausedText()
    {
        _gamePausedText.text = "";
    }





} //end of class
