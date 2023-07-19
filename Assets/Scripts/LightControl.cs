using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class LightControl : MonoBehaviour
{
    GameObject sign;
    Color emissiveColor;

    public float onIntensity;
    public float offIntensity;

    public float blinkInterval;
    public bool isFixed;

    List<GameObject> particleEffects = new List<GameObject>();

    void Start()
    {
        sign = this.gameObject;
        emissiveColor = sign.GetComponent<Renderer>().material.GetColor("_EmissionColor");

        sign.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * offIntensity);
        StartCoroutine(BlinkCoroutine());
        GetChilds();
    }

    private void Update()
    {
        if(isFixed)
        {
            sign.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissiveColor * onIntensity);
            for (int i = 0; i < particleEffects.Count; i++)
            {
                particleEffects[i].gameObject.SetActive(false);
            }
        }
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
            particleEffects.Add(child.gameObject);
        }
    }
}