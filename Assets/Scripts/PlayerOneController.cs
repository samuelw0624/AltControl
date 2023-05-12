using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOneController : MonoBehaviour
{
    //reference variables
    Rigidbody playerRb;
    GameObject ladder;

    //hand position variables
    bool leftHandOffLadder = true;
    bool rightHandOffLadder = true;
    //checks if the player grabbed the ladder for the frst time
    bool firstOnLeft, firstOnRight;

    /*rail spot booleans, 5 spots on each side, 0 = uppper most position, 4 = lower most position 
    */
    bool[] leftBoolArray = new bool[5];
    bool[] rightBoolArray = new bool[5];

    /*04 = left hand at top, right hand at bottom
    40 = left hand at bottom, right hand at top
    */
    bool performed04, performed40;

    /*movement variables, change in inspector, don't change here
     * smaller speed value = faster lerp
     */
    [SerializeField] public float moveDist = 0.5f;
    [SerializeField] float moveSpeed = 5f;

    //slide varibales
    bool slideCountDown;
    //timer varibale used to store how much time had passed
    float timer;
    //slideHoldTime is a constant of how many seconds the player must hold the position
    [SerializeField] float slideHoldTime;
    [SerializeField] float gravityMod;

    //drill variables
    bool isInDrillSlot;

    //repair variables
    //repair radius, change in inspector
    [SerializeField] float detectionRadius = 5f;
    //sign positional bool
    bool signOnLeft, signOnRight;
    GameObject signToFix;

    //game state
    bool gameOver;

    public enum DrillType
    {
        CrossDrill,
        FlatDrill,
        SpiralDrill
    }
    public enum ScrewType
    {
        CrossScrew,
        FlatScrew,
        SpiralScrew,
    }

    public DrillType currentDrill;
    public ScrewType currentScrew;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = this.GetComponent<Rigidbody>();
        ladder = GameObject.FindWithTag("Ladder");

        leftHandOffLadder = true;
        rightHandOffLadder = true;

        //standard starting drill
        currentDrill = DrillType.CrossDrill;

    }

    #region Update Methods
    // Update is called once per frame
    void Update()
    {
        CheckLeftHand();
        CheckRightHand();

        CheckLeftRail();
        CheckRightRail();

        SwitchDrill();
        HandleDrills();

        SlideDown();

        //player's position and rotation follows to the ladder
        this.transform.position = new Vector3(ladder.transform.position.x, this.transform.position.y, this.transform.position.z);

        Quaternion newRotation = Quaternion.Euler(0, 0, ladder.transform.rotation.eulerAngles.z);
        transform.rotation = newRotation;

    }

    private void FixedUpdate()
    {
        var keyboard = Keyboard.current;
        //check if there is a keyboard
        if (keyboard == null)
        {
            return;
        }

        if (!leftHandOffLadder && !rightHandOffLadder && firstOnLeft && firstOnRight)
        {
            gameOver = false;

        }else if(leftHandOffLadder && rightHandOffLadder && firstOnLeft && firstOnRight)
        {
            gameOver = true;
            Debug.Log("Game Over is " + gameOver);
        }

        ClimbUp();
    }
    #endregion

    #region Rail Position Logic

    void CheckLeftHand()
    {
        //Debug.Log("Left hand off ladder is " + leftHandOffLadder);

        /*first check if left hand had ever been placed on the ladder
         * then checks if the hand is grabbed on currently*/
        if (Keyboard.current[Key.W].wasPressedThisFrame && !firstOnLeft)
        {
            firstOnLeft = true;
            leftHandOffLadder = false;
        }
        else if (Keyboard.current[Key.W].wasPressedThisFrame)
        {
            leftHandOffLadder = false;
        }
        if (Keyboard.current[Key.A].wasPressedThisFrame)
        {
            leftHandOffLadder = true;
            ResetLeftBool();
        }
    }

    void CheckRightHand()
    {
        //Debug.Log("Right hand off ladder is " + rightHandOffLadder);

        //same as left hand
        if (Keyboard.current[Key.R].wasPressedThisFrame && !firstOnRight)
        {
            firstOnRight = true;
            rightHandOffLadder = false;
        }
        else if (Keyboard.current[Key.R].wasPressedThisFrame)
        {
            leftHandOffLadder = false;
        }
        if (Keyboard.current[Key.D].wasPressedThisFrame)
        {
            rightHandOffLadder = true;
            ResetRightBool();
        }
    }

    /*rail positionis checked every frame. When the key input corresponding to a 
     * position is recieved, all other position is reset to false except the current one.
     */
    void CheckLeftRail()
    {
        if (!leftHandOffLadder)
        {
            if (Keyboard.current[Key.T].wasPressedThisFrame)
            {
                ResetLeftBool(0);
            }
            if (Keyboard.current[Key.Y].wasPressedThisFrame)
            {
                ResetLeftBool(1);
            }
            if (Keyboard.current[Key.G].wasPressedThisFrame)
            {
                ResetLeftBool(2);
            }
            if (Keyboard.current[Key.H].wasPressedThisFrame)
            {
                ResetLeftBool(3);
            }
            if (Keyboard.current[Key.B].wasPressedThisFrame)
            {
                ResetLeftBool(4);
            }
        }
    }

    void CheckRightRail()
    {
        if (!rightHandOffLadder)
        {
            if (Keyboard.current[Key.O].wasPressedThisFrame)
            {
                ResetRightBool(0);
            }
            if (Keyboard.current[Key.I].wasPressedThisFrame)
            {
                ResetRightBool(1);
            }
            if (Keyboard.current[Key.K].wasPressedThisFrame)
            {
                ResetRightBool(2);
            }
            if (Keyboard.current[Key.J].wasPressedThisFrame)
            {
                ResetRightBool(3);
            }
            if (Keyboard.current[Key.M].wasPressedThisFrame)
            {
                ResetRightBool(4);
            }
        }
    }

    //set entire array to false;
    void ResetLeftBool()
    {
        for (int i = 0; i < leftBoolArray.Length; i++)
        {
            leftBoolArray[i] = false;
        }
    }
    /*overload function of ResetLeftBool(), each step set the value of index i
     * in the array to the outcome of whether i == toSetTrue is true or false
     */
    void ResetLeftBool(int toSetTrue)
    {
        for (int i = 0; i < leftBoolArray.Length; i++)
        {
            leftBoolArray[i] = (i == toSetTrue);
        }
    }

    void ResetRightBool()
    {
        for (int i = 0; i < rightBoolArray.Length; i++)
        {
            rightBoolArray[i] = false;
        }
    }
    //same as the ResetLeftBool
    void ResetRightBool(int toSetTrue)
    {
        for (int i = 0; i < rightBoolArray.Length; i++)
        {
            rightBoolArray[i] = (i == toSetTrue);
        }
    }
    #endregion

    #region Movement

    void ClimbUp()
    {
        if (!performed04)
        {
            if (leftBoolArray[0] && rightBoolArray[4])
            {
                StartCoroutine(MoveToNewPos(GetNewPos()));

                performed04 = true;
                performed40 = false;
            }
        }

        if (!performed40)
        {
            if (leftBoolArray[4] && rightBoolArray[0])
            {
                StartCoroutine(MoveToNewPos(GetNewPos()));

                performed40 = true;
                performed04 = false;
            }
        }
    }

    void SlideDown()
    {
        //Debug.Log(timer);

        //at hand position 2,2, start slideCoundDown
        if (leftBoolArray[2] && rightBoolArray[2] && !slideCountDown)
        {
            slideCountDown = true;
            timer = 0f;
        }

        if (slideCountDown)
        {
            timer += Time.deltaTime;
        }

        //enable modified gravity when hold threashold is reached
        if (leftBoolArray[2] && rightBoolArray[2] && timer >= slideHoldTime)
        {
            Physics.gravity = new Vector3(0, gravityMod, 0);
            playerRb.useGravity = true;
        }
        else if (!leftBoolArray[2] || !rightBoolArray[2])
        {
            slideCountDown = false;
            timer = 0f;

            //reset velocity and disable gravity when position != 2,2
            playerRb.useGravity = false;
            playerRb.velocity = Vector3.zero;
        }
    }

    IEnumerator MoveToNewPos(Vector3 newPos)
    {
        float elapsedTime = 0f;
        Vector3 startingPos = this.transform.position;
        //Debug.Log("startingPos.y is" + startingPos.y);
        while (elapsedTime < moveSpeed)
        {
            this.transform.position = Vector3.Lerp(startingPos, newPos, elapsedTime / moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        this.transform.position = newPos;

    }

    Vector3 GetNewPos()
    {
        Vector3 newPosition = transform.position + new Vector3(0, moveDist, 0);
        return newPosition;
    }
    #endregion

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
        if (isInDrillSlot)
        {
            //each drill case checks whether the current screw matched the current drill
            switch (currentDrill)
            {
                case DrillType.CrossDrill:
                    if (currentScrew == ScrewType.CrossScrew)
                    {
                        FixSign();
                    }
                    break;
                case DrillType.FlatDrill:
                    if (currentScrew == ScrewType.FlatScrew)
                    {
                        FixSign();
                    }
                    break;
                case DrillType.SpiralDrill:
                    if (currentScrew == ScrewType.SpiralScrew)
                    {
                        FixSign();
                    }
                    break;
            }
        }
    }

    void CheckDrillStatus()
    {
        //Debug.Log("drill in slot is " + isInDrillSlot);

        if (Keyboard.current[Key.P].wasPressedThisFrame && (signOnLeft || signOnRight))
        {
            isInDrillSlot = true;
        }
        else if (Keyboard.current[Key.L].wasPressedThisFrame && (signOnLeft || signOnRight))
        {
            isInDrillSlot = false;
        }
    }
    #endregion

    #region Repair Methods
    private void OnTriggerEnter(Collider other)
    {
        //if the sign is trigger range has the word sign in object name
        if (other.gameObject.name.Contains("sign"))
        {
            //check what kind of sign it is
            if (other.gameObject.CompareTag("cross"))
            {
                currentScrew = ScrewType.CrossScrew;
            }
            if (other.gameObject.CompareTag("flat"))
            {
                currentScrew = ScrewType.FlatScrew;
            }
            if (other.gameObject.CompareTag("spiral"))
            {
                currentScrew = ScrewType.SpiralScrew;
            }

            //Debug.Log("current screw type is " + currentScrew);

            signToFix = other.gameObject;
            //convert sign position to local position relative to player
            Vector3 signLocalPos = this.transform.InverseTransformPoint(other.gameObject.transform.position);
            if (signLocalPos.x < 0)
            {
                signOnLeft = true;
                //Debug.Log("left sign");

            }
            else if (signLocalPos.x > 0)
            {
                signOnRight = true;
                //Debug.Log("right sign");
            }
        }else
        {
            //Debug.Log("no sign to repair");
        }
    }

    void FixSign()
    {
        if (signOnLeft && leftHandOffLadder && Keyboard.current[Key.S].wasPressedThisFrame)
        {
            //disable sign collider upon fix
            Collider signCollider = signToFix.gameObject.GetComponent<BoxCollider>();
            signCollider.enabled = false;
            
            Debug.Log("sign repaired on the left");
        }
        if (signOnRight && rightHandOffLadder && Keyboard.current[Key.S].wasPressedThisFrame)
        {
            Collider signCollider = signToFix.gameObject.GetComponent<BoxCollider>();
            signCollider.enabled = false;

            Debug.Log("sign repaired on the right");
        }
    }

    //visualize player fix trigger range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    #endregion  
}
