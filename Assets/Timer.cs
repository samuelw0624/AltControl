using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer instance { get; private set; }
    [SerializeField]
    private Slider p1Slider;
    [SerializeField]
    private Slider p2Slider;
    [SerializeField]
    public float slideTimer;
    [SerializeField]
    public bool stopTimer;
    [SerializeField]
    private float maxTimer;
    [SerializeField]
    private Gradient grandient;
    [SerializeField]
    private Image p2Fill;
    [SerializeField]
    private Image p1Fill;
    [SerializeField]
    private TMP_Text p1Text;
    [SerializeField]
    private TMP_Text p2Text;
    [SerializeField]
    private Text startTimer1;
    [SerializeField]
    private Text startTimer3;
    [SerializeField]
    private Text startTimer2;
    [SerializeField]
    private Text startTimer4;
    [SerializeField]
    public bool gameStart;
    [SerializeField]
    private TMP_Text startTimerText1;
    [SerializeField]
    private TMP_Text startTimerText2;
    [SerializeField]
    private float startTimer;
    [SerializeField]
    private GameObject p1StartTimer;
    [SerializeField]
    private GameObject p2StartTimer;
    [SerializeField]
    private float timerValue;
    [SerializeField]
    private AudioSource audio;

    [SerializeField]
    private GameObject clock;
    [SerializeField]
    public bool isGradeA;
    [SerializeField]
    public bool isGradeB;
    [SerializeField]
    public bool isGradeC;
    [SerializeField]
    public bool isGradeD;
    [SerializeField]
    public bool isGradeE;
    [SerializeField]
    public bool isGradeF;

    [Header("Tutorial")]
    [SerializeField]
    private AudioSource t_audio;
    [SerializeField]
    public bool inTutorial;
    [SerializeField]
    public GameManager gameManager;
    [SerializeField]
    public bool timerStart;
    Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //SetTimer(120);
        p1Slider.maxValue = maxTimer;
        p1Slider.value = slideTimer;

        p2Slider.maxValue = maxTimer;
        p2Slider.value = slideTimer;

        grandient.Evaluate(1f);

        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "TestLevel_01" || currentScene.name == "Level_01" || currentScene.name == "Level_02" || currentScene.name == "Level_03" || currentScene.name == "Level_04")
        {
            if (!gameStart)
            {
                StartCoroutine(StartTimer());
                audio.Play();
                print("Timer Alarm");
            }
        }

        if (currentScene.name == "Level_Tutorial_01")
        {
            inTutorial = true;
        }




        }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.instance.tutorialEnd && !gameStart && !timerStart)
        {
            StartCoroutine(StartTimer());
            audio.Play();
            print("Timer Alarm");
            timerStart = true;
        }      
    }


    private void SetTimer(int second)
    {
        //remainingDuration = second;
        //StartCoroutine(StartTimer());
    }

    //private IEnumerator StartTimer()
    //{
    //    while(remainingDuration >= 0)
    //    {
    //        text.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
    //        p1Text.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
    //        fill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
    //        p1Fill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
    //        remainingDuration--;
    //        yield return new WaitForSeconds(1F);
    //    }

    //    TimesUp();
    //}

    //private void TimesUp()
    //{
    //    print("Game Over");
    //}

    public void StartTimerAction()
    {

        StartCoroutine(StartTimerTicker());

    }

    IEnumerator StartTimerTicker()
    {
        while(stopTimer == false && !PlayerOneController.instance.gameEnd)
        {

            slideTimer += Time.deltaTime;
            yield return new WaitForSeconds(0.001f);

            p1Slider.value = slideTimer;
            p1Fill.color = grandient.Evaluate(p1Slider.normalizedValue);
            p2Slider.value = slideTimer;
            p2Fill.color = grandient.Evaluate(p1Slider.normalizedValue);

            if (KiteEffect.instance.kiteAttack)
            {
                AddTimer();
            }
            p1Text.text = $"{slideTimer / 60:00} : {slideTimer % 60:00}";
            p2Text.text = $"{slideTimer / 60:00} : {slideTimer % 60:00}";
            //startTimer1.text = $"{slideTimer / 60:00} : {slideTimer % 60:00}";
            //startTimer2.text = $"{slideTimer / 60:00} : {slideTimer % 60:00}";
        }

    }


    public void AddTimer()
    {
        slideTimer += 5f;
        KiteEffect.instance.kiteAttack = false;
        StartCoroutine(ClockEffect());
        print("Attack");
        
    }
    IEnumerator ClockEffect()
    {
        clock.SetActive(true);
        yield return new WaitForSeconds(3f);
        clock.SetActive(false);
    }


    IEnumerator StartTimer()
    {
        while (gameStart == false)
        {
            startTimer -= Time.deltaTime;
            yield return new WaitForSeconds(0.001f);

            //print("Timer");
            p1StartTimer.SetActive(true);
            p2StartTimer.SetActive(true);
            //startTimer = timerValue;

            startTimer1.text = (startTimer).ToString("0");
            startTimer2.text = (startTimer).ToString("0");
            startTimer3.text = (startTimer).ToString("0");
            startTimer4.text = (startTimer).ToString("0");
            
            if (startTimer <= 0)
            {
                p1StartTimer.SetActive(false);
                p2StartTimer.SetActive(false);

                if(t_audio != null)
                {
                    t_audio.Play();
                }

                gameStart = true;
                StartTimerAction();
            }
        }

        yield return gameStart;
    }

    #region Winning Grade
    public void AssignGrade()
    {
        if(slideTimer <= 120)
        {
            isGradeA = true;
        } 
        else if(slideTimer > 120 && slideTimer <= 180)
        {
            isGradeB = true;
        }
        else if (slideTimer > 180 && slideTimer <= 240)
        {
            isGradeC = true;
        }
        else if (slideTimer > 240 && slideTimer <= 300)
        {
            isGradeD = true;
        }
        else if (slideTimer > 300 && slideTimer <= 360)
        {
            isGradeE = true;
        }
        else if (slideTimer > 360)
        {
            isGradeF= true;
        }

    }


    #endregion

}
