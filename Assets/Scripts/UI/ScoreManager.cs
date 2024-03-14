using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scorePlayer1;
    public Text scorePlayer2;

    public AudioSource source;
    public AudioClip coinCollected;
    public AudioClip coinLosted;

    public float score;
    public float score_levelTutorial;
    public float score_level01;
    public float score_level02;
    public float score_level03;

    [SerializeField]
    public Text progressionText1;
    [SerializeField]
    public bool tutorialLevelIsSetUp;
    [SerializeField]
    public bool Level1IsSetUp;
    [SerializeField]
    public bool isRead;
    [SerializeField]
    public bool restartGame;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //print original coin value
        //scorePlayer1.text = "Coin:" + score.ToString();
        //scorePlayer2.text = "Coin:" + score.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        ProgressionUI();
        ReadScore();
    }

    void ReadScore()
    {
        if (GameManager.instance.currentScene.name == "Level_01" || GameManager.instance.currentScene.name == "Level_02" || GameManager.instance.currentScene.name == "Level_03" || GameManager.instance.currentScene.name == "Level_04")
        {
            if (!isRead && !restartGame)
            {
                score = ScoreBoard.instance.originalScore;
                isRead = true;
                print("Original Score");
                scorePlayer1.text = "Coin:" + score.ToString();
                scorePlayer2.text = "Coin:" + score.ToString();
            }

            if(GameManager.instance.currentScene.name == "Level_01")
            {
                if (PlayerOneController.instance.gameEnd)
                {
                    score_level01 = score;
                }

                if (!isRead && restartGame)
                {
                    score = ScoreBoard.instance.TutorialScore;
                    isRead = true;
                    scorePlayer1.text = "Coin:" + score.ToString();
                    scorePlayer2.text = "Coin:" + score.ToString();
                }
            }

            if (GameManager.instance.currentScene.name == "Level_02")
            {
                if (PlayerOneController.instance.gameEnd)
                {
                    score_level02 = score;
                }

                if (!isRead && restartGame)
                {
                    score = ScoreBoard.instance.Level01Score;
                    isRead = true;
                    scorePlayer1.text = "Coin:" + score.ToString();
                    scorePlayer2.text = "Coin:" + score.ToString();
                }
            }

            if (GameManager.instance.currentScene.name == "Level_03")
            {
                if (PlayerOneController.instance.gameEnd)
                {
                    score_level03 = score;
                }

                if (!isRead && restartGame)
                {
                    score = ScoreBoard.instance.Level02Score;
                    isRead = true;
                    scorePlayer1.text = "Coin:" + score.ToString();
                    scorePlayer2.text = "Coin:" + score.ToString();
                }
            }

            if (GameManager.instance.currentScene.name == "Level_04")
            {
                if (!isRead && restartGame)
                {
                    score = ScoreBoard.instance.Level03Score;
                    isRead = true;
                    scorePlayer1.text = "Coin:" + score.ToString();
                    scorePlayer2.text = "Coin:" + score.ToString();
                }
            }
        }
        else if (GameManager.instance.currentScene.name == "Level_Tutorial_01")
        {
            if (PlayerOneController.instance.gameEnd)
            {
                score_levelTutorial = score;
            }

            if (!isRead)
            {
                score = 0;
                ScoreBoard.instance.originalScore = 0;
                isRead = true;
                print("zero score");
                scorePlayer1.text = "Coin:" + score.ToString();
                scorePlayer2.text = "Coin:" + score.ToString();
            }

        }

    }
    void ProgressionUI()
    {
        if (GameManager.instance.currentScene.name == "Level_Tutorial_01" && !tutorialLevelIsSetUp)
        {
            PlayerOneController.instance.numberOfSignhasBeenFixed = 0;
            PlayerOneController.instance.totalAmountSignNeedToBeFixed = 3;

            progressionText1.text = PlayerOneController.instance.numberOfSignhasBeenFixed.ToString() + " / " + PlayerOneController.instance.totalAmountSignNeedToBeFixed.ToString();
            tutorialLevelIsSetUp = true; 
        }
        
        if (GameManager.instance.currentScene.name == "Level_01" && !Level1IsSetUp)
        {
            PlayerOneController.instance.numberOfSignhasBeenFixed = 0;
            PlayerOneController.instance.totalAmountSignNeedToBeFixed = 4;

            progressionText1.text = PlayerOneController.instance.numberOfSignhasBeenFixed.ToString() + " / " + PlayerOneController.instance.totalAmountSignNeedToBeFixed.ToString();
            Level1IsSetUp = true;
        }
    }

    #region Add score
    public void AddPoint(float value)
    {
        //collect coin function
        score += value;
        scorePlayer1.text = "Coin:" + score.ToString();
        scorePlayer2.text = "Coin:" + score.ToString();
        //source.PlayOneShot(coinCollected);
        //Debug.Log("Coin + 5");
    }
    #endregion

    #region Subtract score
    public void ReducePoint(int value)
    {
        //lose coin function
        score -= value;
        scorePlayer1.text = "Coin:" + score.ToString();
        scorePlayer2.text = "Coin:" + score.ToString();
        //source.PlayOneShot(coinLosted);
        //Debug.Log("Coin - 3");
    }
    #endregion
}
