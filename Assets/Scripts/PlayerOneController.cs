using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerOneController : MonoBehaviour
{
    public static PlayerOneController instance {get; private set;}
    //reference variables
    Rigidbody playerRb;
    GameObject ladder;

    //hand position variables
    bool leftHandOffLadder = true;
    bool rightHandOffLadder = true;
    //checks if the player grabbed the ladder for the frst time
    bool gameStart1, gameStart2, gameOver;

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
    public bool isInDrillSlot;

    //repair variables
    //repair radius, change in inspector
    [SerializeField] float detectionRadius = 5f;
    //sign positional bool
    public bool signOnLeft, signOnRight;
    GameObject signToFix;

    public enum ScrewType
    {
        CrossScrew,
        FlatScrew,
        SpiralScrew,
        None
    }

    public ScrewType currentScrew;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRb = this.GetComponent<Rigidbody>();
        ladder = GameObject.FindWithTag("Ladder");

        leftHandOffLadder = true;
        rightHandOffLadder = true;
    }

    #region Update Methods
    // Update is called once per frame
    void Update()
    {
        CheckLeftHand();
        CheckRightHand();

        CheckLeftRail();
        CheckRightRail();

        SlideDown();

        FollowLadder();
    }

    private void FixedUpdate()
    {
        var keyboard = Keyboard.current;
        //check if there is a keyboard
        if (keyboard == null)
        {
            return;
        }

        if (!leftHandOffLadder && !rightHandOffLadder && gameStart1 && gameStart2)
        {
            gameOver = false;

        }else if(leftHandOffLadder && rightHandOffLadder && gameStart1 && gameStart2)
        {
            gameOver = true;
            Debug.Log("Game Over is " + gameOver);
            //scene transiton functions
        }

        ClimbUp();
    }
    #endregion

    #region Rail Position Logic

    void CheckLeftHand()
    {
        Debug.Log("Left hand off ladder is " + leftHandOffLadder);

        bool anyPosTrue = leftBoolArray.Any(boolValue => boolValue);
        /*first check if left hand had ever been placed on the ladder
         * then checks if the hand is grabbed on currently*/
        if (anyPosTrue && leftHandOffLadder && !gameStart1)
        {
            gameStart1 = true;
            leftHandOffLadder = false;
        }
        else if (anyPosTrue && leftHandOffLadder)
        {
            leftHandOffLadder = false;
        }
        if (Keyboard.current[Key.A].wasPressedThisFrame && !leftHandOffLadder)
        {
            leftHandOffLadder = true;
            ResetLeftBool();
        }
    }

    void CheckRightHand()
    {
        Debug.Log("Right hand off ladder is " + rightHandOffLadder);

        bool anyPosTrue = rightBoolArray.Any(boolValue => boolValue);
        //same as left hand
        if (anyPosTrue && rightHandOffLadder && !gameStart2)
        {
            gameStart2 = true;
            rightHandOffLadder = false;
        }
        else if (anyPosTrue && rightHandOffLadder)
        {
            rightHandOffLadder = false;
        }
        if (Keyboard.current[Key.D].wasPressedThisFrame && !rightHandOffLadder)
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
        if(!gameOver)
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
        if(!gameOver)
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
        if (!performed04 && !leftHandOffLadder && !rightHandOffLadder && !gameOver)
        {
            if (leftBoolArray[0] && rightBoolArray[4])
            {
                StartCoroutine(MoveToNewPos(NewPosition()));

                performed04 = true;
                performed40 = false;
            }
        }

        if (!performed40 && !leftHandOffLadder && !rightHandOffLadder && !gameOver)
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

    Vector3 NewPosition()
    {
        Vector3 newPosition = transform.position + new Vector3(0, moveDist, 0);
        return newPosition;
    }
    #endregion

    #region Repair Methods
    private void OnTriggerEnter(Collider other)
    {
        //if the sign is trigger range has the word sign in object name
        if (other.gameObject.name.Contains("sign"))
        {
            Debug.Log("current screw type is " + currentScrew);

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

            signToFix = other.gameObject;
            //convert sign position to local position relative to player
            Vector3 signLocalPos = this.transform.InverseTransformPoint(other.gameObject.transform.position);
            if (signLocalPos.x < 0)
            {
                signOnLeft = true;
                Debug.Log("left sign");
            }
            else if (signLocalPos.x > 0)
            {
                signOnRight = true;
                Debug.Log("right sign");
            }
        }
        else
        {
            //Debug.Log("no sign to repair");
        }
    }

    public void FixSign()
    {
        if (signOnLeft && leftHandOffLadder && Keyboard.current[Key.S].wasPressedThisFrame)
        {
            //disable sign collider upon fix
            Collider signCollider = signToFix.gameObject.GetComponent<BoxCollider>();
            signCollider.enabled = false;
            
            Debug.Log("sign repaired on the left");
            //repair animation
        }
        if (signOnRight && rightHandOffLadder && Keyboard.current[Key.S].wasPressedThisFrame)
        {
            Collider signCollider = signToFix.gameObject.GetComponent<BoxCollider>();
            signCollider.enabled = false;

            Debug.Log("sign repaired on the right");
            //repair animation
        }
    }

    //visualize player fix trigger range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    #endregion

    #region Rotation and Position Synchronization
    public void FollowLadder()
    {

        //player's position and rotation follows to the ladder
        this.transform.position = new Vector3(ladder.transform.position.x, this.transform.position.y, this.transform.position.z);

        Quaternion newRotation = Quaternion.Euler(0, 0, ladder.transform.rotation.eulerAngles.z);
        transform.rotation = newRotation;
    }

    #endregion
}
