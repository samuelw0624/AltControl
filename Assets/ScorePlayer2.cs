using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePlayer2 : MonoBehaviour
{
    public Text scoreText;
    int score = 0;
    public static ScorePlayer2 instance;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        scoreText.text = "Coin:" + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Add score
    public void AddPoint(int value)
    {
        score += value;
        scoreText.text = "Coin:" + score.ToString();
        Debug.Log("Coin + 5");
    }
    #endregion

    #region Subtract score
    public void ReducePoint(int value)
    {
        score -= value;
        scoreText.text = "Coin:" + score.ToString();
        Debug.Log("Coin - 3");
    }
    #endregion
}
