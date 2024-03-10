using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void GoToNextLevel()
    {
        if(GameManager.instance.currentScene.name == "Level_Tutorial_01")
        {
            //loading scene
            SceneManager.LoadScene("Level_01");
        }
    }
}
