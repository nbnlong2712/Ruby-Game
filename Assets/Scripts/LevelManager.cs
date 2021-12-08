using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadLevel2()
    {
        FindObjectOfType<Player>().ResetHealth();
        FindObjectOfType<Player>().ResetAmmo();
        SceneManager.LoadScene("Level 2");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        Invoke("WaitAndLoad", 2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void WaitAndLoad()
    {
        SceneManager.LoadScene("Over");
    }
}
