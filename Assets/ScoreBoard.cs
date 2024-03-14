using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard instance;

    [SerializeField]
    public float originalScore;

    [SerializeField]
    public bool item1WasPurchased;
    [SerializeField]
    public bool item2WasPurchased;
    [SerializeField]
    public bool item3WasPurchased;

    [SerializeField]
    public float TutorialScore;
    [SerializeField]
    public float Level01Score;
    [SerializeField]
    public float Level02Score;
    [SerializeField]
    public float Level03Score;


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

            if (GameManager.instance.currentScene.name == "Level_Tutorial_01")
            {
                if (PlayerOneController.instance.gameEnd)
                {
                    TutorialScore = ScoreManager.instance.score_levelTutorial;
                }
            }

            if (GameManager.instance.currentScene.name == "Level_01")
            {
                if (PlayerOneController.instance.gameEnd)
                {
                    Level01Score = ScoreManager.instance.score_level01;
                }
            }

            if (GameManager.instance.currentScene.name == "Level_02")
            {
                if (PlayerOneController.instance.gameEnd)
                {
                    Level02Score = ScoreManager.instance.score_level02;
                }
            }

            if (GameManager.instance.currentScene.name == "Level_03")
            {
                if (PlayerOneController.instance.gameEnd)
                {
                    Level03Score = ScoreManager.instance.score_level03;
                }
            }
        }

    }

    void ItemRecord()
    {
        if (GameManager.instance.currentScene.name == "Level_02" || GameManager.instance.currentScene.name == "Level_03" || GameManager.instance.currentScene.name == "Level_04")
        {
            if (PlayerOneController.instance.gameEnd == true)
            {
                item1WasPurchased = EnterShop.instance.isPurchased1;
                item2WasPurchased = EnterShop.instance.isPurchased2;
                item3WasPurchased = EnterShop.instance.isPurchased3;
            }
        }
    }
}
