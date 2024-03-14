using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class BoundaryController : MonoBehaviour
{
    [SerializeField]
    private PlayerTwoController p2c;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            print("Ladder");
            p2c.WarningTab1.SetActive(true);
            p2c.WarningTab2.SetActive(true);
            p2c.isOutOfBoundary = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            p2c.isOutOfBoundary = false;
        }
    }
}
