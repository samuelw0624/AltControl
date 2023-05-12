using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drill : MonoBehaviour
{
    //reference variables
    PlayerOneController p1Script;

    public enum DrillType
    {
        CrossDrill,
        FlatDrill,
        SpiralDrill
    }

    public DrillType currentDrill;

    private void Awake()
    {
        p1Script = PlayerOneController.instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        //standard starting drill
        currentDrill = DrillType.CrossDrill;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchDrill();
        HandleDrills();
    }

    #region Drill Methods
    void SwitchDrill()
    {
        //Debug.Log("current drill type is " + currentDrill);

        if (Keyboard.current[Key.Digit0].wasPressedThisFrame)
        {
            currentDrill = DrillType.CrossDrill;
        }
        if (Keyboard.current[Key.Digit9].wasPressedThisFrame)
        {
            currentDrill = DrillType.FlatDrill;
        }
        if (Keyboard.current[Key.Digit8].wasPressedThisFrame)
        {
            currentDrill = DrillType.SpiralDrill;
        }
    }

    void HandleDrills()
    {
        CheckDrillStatus();
        if (p1Script.isInDrillSlot)
        {
            //each drill case checks whether the current screw matched the current drill
            switch (currentDrill)
            {
                case DrillType.CrossDrill:
                    if (PlayerOneController.instance.currentScrew == PlayerOneController.ScrewType.CrossScrew)
                    {
                        p1Script.FixSign();
                    }
                    break;
                case DrillType.FlatDrill:
                    if (PlayerOneController.instance.currentScrew == PlayerOneController.ScrewType.FlatScrew)
                    {
                        p1Script.FixSign();
                    }
                    break;
                case DrillType.SpiralDrill:
                    if (PlayerOneController.instance.currentScrew == PlayerOneController.ScrewType.SpiralScrew)
                    {
                        p1Script.FixSign();
                    }
                    break;
            }
        }
        else
        {
            //Debug.Log("drill is not in slot to repair");
            //UX functions
        }
    }

    void CheckDrillStatus()
    {
        //Debug.Log("drill in slot is " + isInDrillSlot);

        if (Keyboard.current[Key.P].wasPressedThisFrame && (p1Script.signOnLeft || p1Script.signOnRight))
        {
            p1Script.isInDrillSlot = true;
        }
        else if (Keyboard.current[Key.L].wasPressedThisFrame && (p1Script.signOnLeft || p1Script.signOnRight))
        {
            p1Script.isInDrillSlot = false;
        }
    }
    #endregion
}
