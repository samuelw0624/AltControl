using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public void LoadMainGame()
    {
        SceneManager.LoadScene(3);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("TitlePage");
    }
}
