using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour
{
    public static SlowDown instance;

    [SerializeField]
    public bool insideSteam = false;
    [SerializeField]
    private GameObject smokeUI;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (smokeUI != null)
        {
            smokeUI.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerCollider")
        {
            insideSteam = true;
            smokeUI.SetActive(true);
            Debug.Log("insideSteam = " + insideSteam);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerCollider")
        {
            insideSteam = false;
            smokeUI.SetActive(false);
            //Debug.Log("insideSteam = " + insideSteam);
        }
    }

}
