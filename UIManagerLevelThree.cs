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

    public void DisplayGameOverText()
    {
        _gameOverText.enabled = true;
    }

    IEnumerator BlinkingLevelCompleteText()
    {
        while (true)
        {
            _levelCompleted.text = "";
            yield return new WaitForSeconds(0.5f);
            _levelCompleted.text = "Mantis has been defeated! Moving on to Level 4!";
            yield return new WaitForSeconds(0.5f);

        }
    }

    public void HideImagesAndText()
    {
        _mantisImage.enabled = false;
        _bossHealthText.enabled = false;
        _playerImage.enabled = false;
        _playerLivesText.enabled = false;
        _coinImage.enabled = false;
        _coinsCollectedText.enabled = false;
    }





} //end of class
