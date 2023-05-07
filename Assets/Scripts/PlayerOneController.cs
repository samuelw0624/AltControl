using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOneController : MonoBehaviour
{
    Rigidbody playerRb;
    bool leftHandOnLadder, rightHandOnLadder;
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
    bool canSlide;
    float timer;
    [SerializeField] float slideHoldTime;
    [SerializeField] float gravityMod;


    //repair variables
    //repair radius, change in inspector
    [SerializeField] float detectionRadius = 5f;
    //sign positional bool
    bool signOnLeft, signOnRight;
    GameObject signToFix;

    //ladder
    GameObject ladder;

    public enum DrillType
    {
        CrossDrill,
        FlatDrill,
        SpiralDrill
    }
    public DrillType currentDrill;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = this.GetComponent<Rigidbody>();
        //standard starting drill
        currentDrill = DrillType.SpiralDrill;
        ladder = GameObject.FindWithTag("Ladder");

    }

    #region Update Methods
    // Update is called once per frame
    void Update()
    {
        CheckLeftRail();
        CheckRightRail();
        FixSign();
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

        if (!leftHandOnLadder && !rightHandOnLadder && firstOnLeft && firstOnRight)
        {
            ResetLeftBool();
            ResetRightBool();
            Debug.Log("You fall to death");
        }

        CheckLeftHand();
        CheckRightHand();
        ClimbUp();

    }
    #endregion

    #region Rail Position Logic

    void CheckLeftHand()
    {
        /*first check if left hand had ever been placed on the ladder
         * then checks if the hand is grabbed on currently*/
        if (Keyboard.current[Key.A].isPressed && !firstOnLeft)
        {
            firstOnLeft = true;
            leftHandOnLadder = true;
        }
        else if (Keyboard.current[Key.A].isPressed)
        {
            leftHandOnLadder = true;
        }
        else
        {
            leftHandOnLadder = false;
            ResetLeftBool();
        }
    }

    void CheckRightHand()
    {
        //same as left hand
        if (Keyboard.current[Key.D].isPressed && !firstOnRight)
        {
            firstOnRight = true;
            rightHandOnLadder = true;
        }
        else if (Keyboard.current[Key.D].isPressed)
        {
            rightHandOnLadder = true;
        }
        else
        {
            rightHandOnLadder = false;
            ResetRightBool();
        }
    }

    /*rail positionis checked every frame. When the key input corresponding to a 
     * position is recieved, all other position is reset to false except the current one.
     */
    void CheckLeftRail()
    {
        if (leftHandOnLadder)
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
        if (rightHandOnLadder)
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
                StartCoroutine(MoveToNewPos(NewPosition()));

                performed04 = true;
                performed40 = false;
            }
        }

        if (!performed40)
        {
            if (leftBoolArray[4] && rightBoolArray[0])
            {
                StartCoroutine(MoveToNewPos(NewPosition()));

                performed40 = true;
                performed04 = false;
            }
        }
    }

    void SlideDown()
    {
        Debug.Log(Physics.gravity.y);
        if (leftBoolArray[2] && rightBoolArray[2] && !canSlide)
        {
            canSlide = true;
            timer = 0f;
        }

        if (canSlide)
        {
            timer += Time.deltaTime;
        }

        if (canSlide && timer >= slideHoldTime && leftBoolArray[2] && rightBoolArray[2])
        {
            playerRb.useGravity = true;
            Physics.gravity = new Vector3(0, gravityMod, 0);
        } else if (!leftBoolArray[2] ||  !rightBoolArray[2])
        {
            playerRb.useGravity = false;
            playerRb.velocity = Vector3.zero;
            canSlide = false;
        }
    }

    IEnumerator MoveToNewPos(Vector3 newPos)
    {
        float elapsedTime = 0f;
        Vector3 startingPos = this.transform.position;
        //Debug.Log("startingPos.y is" + startingPos.y);
        while(elapsedTime < moveSpeed)
        {
            this.transform.position = Vector3.Lerp(startingPos, newPos, elapsedTime / moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        this.transform.position = newPos;
        
    }

    Vector3 NewPosition()
    {
        Vector3 newPosition = transform.position + new Vector3(0, moveDist, 0);

        return newPosition;
    }
    #endregion

    #region Repair
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("sign"))
        {
            signToFix = other.gameObject;
            //convert sign position to local position relative to player
            Vector3 signLocalPos = this.transform.InverseTransformPoint(other.gameObject.transform.position);
            if (signLocalPos.x < 0)
            {
                signOnLeft = true;

            } else if (signLocalPos.x > 0)
            {
                signOnRight = true;
            }
        } else
        {
            Debug.Log("no sign to repair");
        }
    }

    void FixSign()
    {
        if (signOnLeft && !leftHandOnLadder && signToFix.CompareTag("sign") && Keyboard.current[Key.S].wasPressedThisFrame)
        {
            signToFix.tag = "fixedSign";
            Debug.Log("sign repaired on the left");
        }
        if (signOnRight && !rightHandOnLadder && signToFix.CompareTag("sign") && Keyboard.current[Key.F].wasPressedThisFrame)
        {
            signToFix.gameObject.tag = "fixedSign";
            Debug.Log("sign repaired on the right");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    #endregion  
}
