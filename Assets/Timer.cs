using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{
    [SerializeField]
    private Image fill;
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private Image p1Fill;
    [SerializeField]
    private TMP_Text p1Text;
    [SerializeField]
    public int duration;
    [SerializeField]
    public int remainingDuration;

    // Start is called before the first frame update
    void Start()
    {
        SetTimer(120);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SetTimer(int second)
    {
        remainingDuration = second;
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while(remainingDuration >= 0)
        {
            text.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
            p1Text.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
            fill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
            p1Fill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1F);
        }

        TimesUp();
    }

    private void TimesUp()
    {
        print("Game Over");
    }


}
