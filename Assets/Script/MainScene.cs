using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public void MainToInGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
