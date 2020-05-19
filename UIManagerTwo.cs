using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerTwo : MonoBehaviour
{
    [SerializeField] private Image _playerImage;
    [SerializeField] private Text _playerLivesText;
    [SerializeField] private Image _coinsImage;
    [SerializeField] private Text _coinsText;
    //[SerializeField] private Image _ghostPawnImage;
    //[SerializeField] private Text _ghostLivesText;
    [SerializeField] private Image _bossImage;
    [SerializeField] private Text _bossHealthText;
    [SerializeField] private Text _levelCompleted;

    private PlayerTwo player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerTwo>();
    }


    // Start is called before the first frame update
    void Start()
    {
        _playerLivesText.text = "x:3";
        _coinsText.text = "x:";
        _bossImage.enabled = false;
        _bossHealthText.enabled = false;
        _levelCompleted.enabled = false;
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
        _levelCompleted.enabled = true;
        StartCoroutine(BlinkingLevelCompleteText());
    }

    IEnumerator BlinkingLevelCompleteText()
    {
        while (true)
        {
            _levelCompleted.text = "";
            yield return new WaitForSeconds(0.5f);
            _levelCompleted.text = "Ghost has been defeated! Moving on to Level 3!";
            yield return new WaitForSeconds(0.5f);

        }
    }


}
