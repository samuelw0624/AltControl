using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectKey : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectInput();
    }
    void DetectInput()
    {
        //if (Input.GetKey(KeyCode.Q) || Input.GetKeyUp(KeyCode.E))
        //{
        //    Debug.Log("Q pressed");
        //}
        //if (Input.GetKey(KeyCode.E) ||Input.GetKeyUp(KeyCode.Q))
        //{
        //    Debug.Log("E pressed");
        //}

        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Q pressed");
        }
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("E pressed");
        }

    }
}
