using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{
    [SerializeField]
    private Slider p1Slider;
    [SerializeField]
    private Slider p2Slider;
    [SerializeField]
    private float sliderTimer;
    [SerializeField]
    private bool stopTimer;
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


    // Start is called before the first frame update
    void Start()
    {
        //SetTimer(120);
        p1Slider.maxValue = maxTimer;
        p1Slider.value = sliderTimer;

        p2Slider.maxValue = maxTimer;
        p2Slider.value = sliderTimer;

        grandient.Evaluate(1f);
        StartTimerAction();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void StartTimerAction()
    {
        StartCoroutine(StartTimerTicker());
    }

    IEnumerator StartTimerTicker()
    {
        while(stopTimer == false)
        {
            sliderTimer += Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
            p1Slider.value = sliderTimer;
            p1Fill.color = grandient.Evaluate(p1Slider.normalizedValue);
            p2Slider.value = sliderTimer;
            p2Fill.color = grandient.Evaluate(p1Slider.normalizedValue);
            p1Text.text = $"{sliderTimer / 60:00} : {sliderTimer % 60:00}";
            p2Text.text = $"{sliderTimer / 60:00} : {sliderTimer % 60:00}";
        }

    }

}
