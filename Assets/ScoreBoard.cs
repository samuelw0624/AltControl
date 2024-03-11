using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard instance;

    [SerializeField]
    public int originalScore;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        RecordScore();
    }


    void RecordScore()
    {
        if(GameManager.instance.currentScene.name == "Level_Tutorial_01" || GameManager.instance.currentScene.name == "Level_01" || GameManager.instance.currentScene.name == "Level_02" || GameManager.instance.currentScene.name == "Level_03" || GameManager.instance.currentScene.name == "Level_04")
        {
            if (PlayerOneController.instance.gameEnd == true)
            {
                originalScore = ScoreManager.instance.score;
            }
        }
    }
}
