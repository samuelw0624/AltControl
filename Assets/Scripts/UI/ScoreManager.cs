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

    public int score = 500;

    [SerializeField]
    public Text progressionText1;
    [SerializeField]
    public bool tutorialLevelIsSetUp;
    [SerializeField]
    public bool Level1IsSetUp;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //print original coin value
        scorePlayer1.text = "Coin:" + score.ToString();
        scorePlayer2.text = "Coin:" + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        ProgressionUI();
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
    public void AddPoint(int value)
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
