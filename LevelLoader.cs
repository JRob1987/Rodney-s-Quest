using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    
    //load main menu with its build index
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
     

    //loads the Keyboard Controls scene by its build index
    public void LoadControlsScene()
    {
        SceneManager.LoadScene(5);
    }

    //load level 1 by its index after clicking the play button on the main menu
    public void LoadLevelOneScene()
    {
        SceneManager.LoadScene(1);
    }

    //loads game play scenes 2-4 by their build index
    public void LoadGamePlayScenes(int index)
    {
        SceneManager.LoadScene(index);
    }     
    

    //quit stand alone application
    public void QuitApplication()
    {
        Application.Quit();
    }

    //quit WebGL application
    public void QuitWebGL()
    {
        //opens to my portfolio page
        Application.OpenURL("https://justindoggett.carbonmade.com/");
    }
}
