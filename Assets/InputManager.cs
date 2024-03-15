using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public bool LeftRotate;
    public bool RightRotate;

    public bool leftFast;
    public bool leftNormal;
    public bool neutral;
    public bool rightFast;
    public bool rightNormal;

    public int numOfLadder;
    // Getter for the singleton instance
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("InputManager");
                instance = obj.AddComponent<InputManager>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    // Define an event to be triggered when a key is pressed
    public delegate void KeyPressedEventHandler(KeyCode keyCode);
    public event KeyPressedEventHandler OnKeyPressed;
    public event KeyPressedEventHandler OnKeyPressed1;
    public event KeyPressedEventHandler OnKeyPressed2;

    // Update is called once per frame
    void Update()
    {
        // Example: Detect key presses and trigger the event
        if (Input.GetKey(KeyCode.Q))
        {
            OnKeyPressed?.Invoke(KeyCode.Q);
            LeftRotate = false;
            RightRotate = true;
            //print("QisPressed");
        }

        if (Input.GetKey(KeyCode.E))
        {
            RightRotate = false;
            LeftRotate = true;
            OnKeyPressed?.Invoke(KeyCode.E);
            //print("EisPressed");
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            leftFast = true;
            leftNormal = false;
            neutral = false;
            rightFast = false;
            rightNormal = false;
            OnKeyPressed1?.Invoke(KeyCode.Alpha1);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            leftFast = false;
            leftNormal = true;
            neutral = false;
            rightFast = false;
            rightNormal = false;
            OnKeyPressed1?.Invoke(KeyCode.Alpha2);
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            leftFast = false;
            leftNormal = false;
            neutral = true;
            rightFast = false;
            rightNormal = false;
            OnKeyPressed1?.Invoke(KeyCode.Alpha3);
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            leftFast = false;
            leftNormal = false;
            neutral = false;
            rightFast = false;
            rightNormal = true;
            OnKeyPressed1?.Invoke(KeyCode.Alpha4);
        }

        if (Input.GetKey(KeyCode.Alpha5))
        {
            leftFast = false;
            leftNormal = false;
            neutral = false;
            rightFast = true;
            rightNormal = false;
            OnKeyPressed1?.Invoke(KeyCode.Alpha5);
        }

        if (Input.GetKey(KeyCode.Z))
        {
            numOfLadder = 5;
            OnKeyPressed2?.Invoke(KeyCode.Z);
        }
        else if (Input.GetKey(KeyCode.X))
        {
            numOfLadder = 4;
            OnKeyPressed2?.Invoke(KeyCode.X);
        }
        else if (Input.GetKey(KeyCode.C))
        {
            numOfLadder = 3;
            OnKeyPressed2?.Invoke(KeyCode.C);
        }
        else if (Input.GetKey(KeyCode.V))
        {
            numOfLadder = 2;
            OnKeyPressed2?.Invoke(KeyCode.V);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            numOfLadder = 1;
            OnKeyPressed2?.Invoke(KeyCode.Space);
        }

    }
}

