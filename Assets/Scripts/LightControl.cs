using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class LightControl : MonoBehaviour
{
    GameObject sign;
    //public GameObject selectionRing;
    Color emissiveColor;

    public float onIntensity;
    public float offIntensity;

    public float blinkInterval;
    public bool isFixed;

    public bool isSelected;
    public GameObject selectionRing;
    float countdownTime = 0.5f;

    List<GameObject> repairSpotChildren = new List<GameObject>();

    void Start()
    {
        sign = this.gameObject;
        emissiveColor = sign.GetComponent<Renderer>().material.GetColor("_EmissionColor");

        sign.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * offIntensity);
        StartCoroutine(BlinkCoroutine());
        GetChilds();
        //selectionRing = repairSpotChildren[repairSpotChildren.Count -1];
    }

    private void Update()
    {
        if(isFixed)
        {
            sign.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * onIntensity);
            for (int i = 0; i < repairSpotChildren.Count; i++)
            {
                repairSpotChildren[i].gameObject.SetActive(false);
            }
        }
        ToggleSelectionRing();
    }

    IEnumerator BlinkCoroutine()
    {
        while (!isFixed)
        {
            sign.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * onIntensity);
            yield return new WaitForSeconds(blinkInterval);
            sign.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * offIntensity);
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    void GetChilds()
    {
        int childCount = sign.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = sign.transform.GetChild(i);
            repairSpotChildren.Add(child.gameObject);
        }
    }

    void ToggleSelectionRing()
    {
        if(!isSelected)
        {
            selectionRing.SetActive(false);
        }else if(isSelected)
        {
            selectionRing.SetActive(true);
            StartCoroutine(ResetSelectionRing());
        }
    }

    IEnumerator ResetSelectionRing()
    {
        float timeLeft = countdownTime;
        while (timeLeft > 0f)
        {
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }
        SelectionSwitch();
    }

    void SelectionSwitch()
    {
        isSelected = false;
    }
}
