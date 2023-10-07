using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour
{
    [SerializeField]
    public bool insideSteam = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerCollider")
        {
            insideSteam = true;
            Debug.Log("insideSteam = " + insideSteam);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerCollider")
        {
            insideSteam = false;
            Debug.Log("insideSteam = " + insideSteam);
        }
    }

}
