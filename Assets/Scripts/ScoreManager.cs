using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scorePlayer1;
    public Text scorePlayer2;

    public AudioSource source;
    public AudioClip coinCollected;
    public AudioClip coinLosted;

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
        //print original coin value
        scorePlayer1.text = "Coin:" + score.ToString();
        scorePlayer2.text = "Coin:" + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Add score
    public void AddPoint(int value)
    {
        //collect coin function
        score += value;
        scorePlayer1.text = "Coin:" + score.ToString();
        scorePlayer2.text = "Coin:" + score.ToString();
        source.PlayOneShot(coinCollected);
        Debug.Log("Coin + 5");
    }
    #endregion

    #region Subtract score
    public void ReducePoint(int value)
    {
        //lose coin function
        score -= value;
        scorePlayer1.text = "Coin:" + score.ToString();
        scorePlayer2.text = "Coin:" + score.ToString();
        source.PlayOneShot(coinLosted);
        Debug.Log("Coin - 3");
    }
    #endregion
}
