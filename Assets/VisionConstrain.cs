using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionConstrain : MonoBehaviour
{

    [SerializeField]
    public bool insideSteam = false;
    [SerializeField]
    private GameObject smokeUI;

    private void Awake()
    {
        
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

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerCollider")
        {
            insideSteam = false;
            smokeUI.SetActive(false);
 
        }
    }
}
