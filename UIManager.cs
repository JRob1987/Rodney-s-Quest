using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image _playerImage;
    [SerializeField] private Image _coinImage;
    [SerializeField] private Image _bossImage;
    [SerializeField] private Text _playerLivesText;
    [SerializeField] private Text _coinsText;
    [SerializeField] private Text _bossHealthText;
    [SerializeField] private Text _levelCompleteText;
    // Start is called before the first frame update
    void Start()
    {
        _bossImage.enabled = false;
        _bossHealthText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //update player lives UI text
    public void UpdatePlayerLivesUIText(int lives)
    {
        _playerLivesText.text = "X: " + lives.ToString();
    }

    //update coins collected UI text
    public void UpdateCoinsCollectedText(int coins)
    {
        _coinsText.text = "X: " + coins.ToString();
    }

    //update boss health UI text
    public void UpdateBossHealthText(int health)
    {
        
        _bossHealthText.text = "Healh: " + health.ToString();

    }

    public void EnableBossUI()
    {
        _bossImage.enabled = true;
        _bossHealthText.enabled = true;
    }

    public void DisplayLevelCompleteText()
    {
        StartCoroutine(BlinkingLevelCompleteText());
    }

    IEnumerator BlinkingLevelCompleteText()
    {
        while(true)
        {
            _levelCompleteText.text = "";
            yield return new WaitForSeconds(0.5f);
            _levelCompleteText.text = "Scorpion has been defeated! Moving on to Level 2!";
            yield return new WaitForSeconds(0.5f);

        }
    }

}
