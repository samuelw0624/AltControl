using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalconArea : MonoBehaviour
{
    public static FalconArea instance;
    [SerializeField]
    public bool withinAttackRange;



    private void Start()
    {
        instance = this;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            withinAttackRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            withinAttackRange = false;
        }
    }

}
