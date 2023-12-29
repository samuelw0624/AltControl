using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SceneTransitionManager : MonoBehaviour
{
    Scene currentScene;
    AudioSource startSound;

    private void Start()
    {
        startSound = this.GetComponent<AudioSource>();
    }
    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "TitlePage")
        {
            if(Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Alpha9))
            {
                startSound.Play();
                StartCoroutine(StartLoading());
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

    IEnumerator StartLoading()
    {
        yield return new WaitForSeconds(1.5f);
        LoadMainGame();
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
