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

    //load level 1 scene with its build index
    public void LoadLevelOneScene()
    {
        SceneManager.LoadScene(1);
    }

    //load level 2 with its build index
    public void LoadLevelTwoScene()
    {
        SceneManager.LoadScene(2);
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
        Application.OpenURL("https://jrdoggett.carbonmade.com/");
    }
}
