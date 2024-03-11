using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevDebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SkipLevel();
    }


    void SkipLevel()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if(GameManager.instance.currentScene.name == "Level_Tutorial_01")
            {
                SceneManager.LoadScene("LoadingLevel_01");
            }
            if (GameManager.instance.currentScene.name == "LoadingLevel_01")
            {
                SceneManager.LoadScene("Level_01");
            }
            if (GameManager.instance.currentScene.name == "Level_01")
            {
                SceneManager.LoadScene("LoadingLevel_02");
            }
            if (GameManager.instance.currentScene.name == "LoadingLevel_02")
            {
                SceneManager.LoadScene("Level_02");
            }
            if (GameManager.instance.currentScene.name == "Level_02")
            {
                SceneManager.LoadScene("LoadingLevel_03");
            }
            if (GameManager.instance.currentScene.name == "LoadingLevel_03")
            {
                SceneManager.LoadScene("Level_03");
            }
            if (GameManager.instance.currentScene.name == "Level_03")
            {
                SceneManager.LoadScene("LoadingLevel_04");
            }
            if (GameManager.instance.currentScene.name == "LoadingLevel_03")
            {
                SceneManager.LoadScene("Level_04");
            }
            if (GameManager.instance.currentScene.name == "Level_04")
            {
                SceneManager.LoadScene("TitlePage");
            }


        }
    }
}
