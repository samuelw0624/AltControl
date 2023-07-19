using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    int score = 0;
    //create add score instance
    public static ScoreManager instance;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        scoreText.text = "POINTS:" + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Add score
    public void AddPoint(int value)
    {
        score += value;
        scoreText.text = "POINTS:" + score.ToString();
        Debug.Log("Point + 1");
    }

    #endregion
}