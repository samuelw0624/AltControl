using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SceneTransitionManager : MonoBehaviour
{
    Scene currentScene;
    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "TitlePage")
        {
            if(Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Alpha9))
            {
                LoadMainGame();
            }
        }
        if (currentScene.name == "GameOver")
        {
            if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Alpha9))
            {
                RestartGame();
            }
        }
    }
    public void LoadMainGame()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("TitlePage");
    }
}
