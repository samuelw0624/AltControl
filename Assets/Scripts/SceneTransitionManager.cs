using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public void LoadMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("TitlePage");
    }
}
