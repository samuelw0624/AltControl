using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceAnimationControl : MonoBehaviour
{
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = this.GetComponent<Animator>();
        StartCoroutine(StartAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(0.31f);

        ani.SetBool("isArrived", true);
    }
}
