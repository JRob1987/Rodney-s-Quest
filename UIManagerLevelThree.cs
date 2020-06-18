using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerLevelThree : MonoBehaviour
{
    //variables
    [SerializeField] private Text _playerLivesText;
    [SerializeField] private Text _coinsCollectedText;


    // Start is called before the first frame update
    void Start()
    {
        _playerLivesText.text = "x : 3";
        _coinsCollectedText.text = "x: 0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCoinCollectedUI(int coins)
    {
        _coinsCollectedText.text = "x: " + coins.ToString();
    }

    public void UpdatePlayerLivesUI(int lives)
    {
        _playerLivesText.text = "x: " + lives.ToString();
    }




} //end of class
