using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;

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

    public GameObject[] leftPosUIArray = new GameObject[5];
    public GameObject[] rightPoseUIArray = new GameObject[5];

    /*04 = left hand at top, right hand at bottom
    40 = left hand at bottom, right hand at top
    */
    bool performed04, performed40;

    /*movement variables, change in inspector, don't change here
     * smaller speed value = faster lerp
     */
    [SerializeField] public float moveDist = 1.1f;
    [SerializeField] float moveSpeed;
    [SerializeField] float moveOriSpeed = 5;
    [SerializeField] float moveSlowSpeed = 3;
    //slide varibales
    bool slideCountDown;
    //timer varibale used to store how much time had passed
    float timer;
    //slideHoldTime is a constant of how many seconds the player must hold the position
    [SerializeField] float slideHoldTime;
    [SerializeField] float gravityMod;

    //drill variables
    //public bool isInDrillSlot;

    //repair variables
    SphereCollider repairCollide;
    //repair radius, change in inspector
    [SerializeField] float detectionRadius = 5f;
    //sign positional bool, currently unsed
    public bool signOnLeft, signOnRight;
    //variables to get closest sign
    public List<GameObject> spotsToFix = new List<GameObject>();
    public GameObject closestSpot;

    //light emission control variables
    LightControl lightcontrolRef;
    AudioSource repairAudio;
    public AudioClip repairClip;

    //follow ladder position
    public Transform[] target;
    public float offset;
    public PlayerTwoController player2;

    public int num;

    public bool reachMax1, reachMax2, reachMax3, reachMax4, reachMax5;



    [SerializeField] SlowDown sD;

    [Header("Animation")]
    [SerializeField]
    public Animator anim;
    public bool isMoving;
    [SerializeField]
    public Transform player2Pos;
    float distance;
    float LateDis;
    [SerializeField]
    bool handsOff = false;

    [SerializeField]
    GameObject p1Screen;
    [SerializeField]
    GameObject p2Screen;
    [SerializeField]
    TMP_Text p1Text;
    [SerializeField]
    TMP_Text p2Text;
    [SerializeField]
    float warningTimer = 5f;
    [SerializeField]
    float timerValue;

    public enum ScrewType
    {
        CrossScrew,
        FlatScrew,
        HexScrew,
        None
    }

    public ScrewType currentScrew;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
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
        repairCollide = this.GetComponent<SphereCollider>();
        repairCollide.radius = detectionRadius;
        repairAudio = this.GetComponent<AudioSource>();

        leftHandOffLadder = true;
        rightHandOffLadder = true;

        moveSpeed = moveOriSpeed;

        distance = Vector2.Distance(this.transform.position, player2Pos.transform.position);

    }

    #region Update Methods
    // Update is called once per frame
    void Update()
    {
        CheckLeftHand();
        CheckRightHand();

        CheckLeftRail();
        CheckRightRail();

        UpdateClosestSpot();

        SlideDown();

        //FollowLadder();

        DetectPlayerPosition();
        DetectReachMaxHeight();

        ConfineLadderHeight();
        Warn();
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
            handsOff = true;
            //SceneManager.LoadScene("GameOver");
        }

        ClimbUp();
    }
    #endregion

    #region Rail Position Logic

    void CheckLeftHand()
    {
        //Debug.Log("Left hand off ladder is " + leftHandOffLadder);

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
        //Debug.Log("Right hand off ladder is " + rightHandOffLadder);

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
            leftPosUIArray[i].SetActive(true);
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
            leftPosUIArray[i].SetActive(i != toSetTrue);
        }
    }

    void ResetRightBool()
    {
        for (int i = 0; i < rightBoolArray.Length; i++)
        {
            rightBoolArray[i] = false;
            rightPoseUIArray[i].SetActive(true);
        }
    }
    //same as the ResetLeftBool
    void ResetRightBool(int toSetTrue)
    {
        for (int i = 0; i < rightBoolArray.Length; i++)
        {
            rightBoolArray[i] = (i == toSetTrue);
            rightPoseUIArray[i].SetActive(i != toSetTrue);
        }
    }
    #endregion

    #region Movement

    void ClimbUp()
    {
        if (!performed04 && !leftHandOffLadder && !rightHandOffLadder && !gameOver && !reachMax1 && !reachMax2 && !reachMax3 && !reachMax4 && !reachMax5)
        {
            if (leftBoolArray[0] && rightBoolArray[4] )
            {
                //StartCoroutine(MoveToNewPos(NewPosition()));
                MovePlayer();
                performed04 = true;
                performed40 = false;
            }
        }

        if (!performed40 && !leftHandOffLadder && !rightHandOffLadder && !gameOver && !reachMax1 && !reachMax2 && !reachMax3 && !reachMax4 && !reachMax5)
        {
            if (leftBoolArray[4] && rightBoolArray[0])
            {
                //StartCoroutine(MoveToNewPos(NewPosition()));
                MovePlayer();
                performed40 = true;
                performed04 = false;
            }
        }
    }

    void SlideDown()
    {
        Vector3 newLocalPos1 = this.transform.localPosition;
        Debug.Log(newLocalPos1);

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
            Vector3 newLocalPos = this.transform.localPosition;
            newLocalPos.y -= (moveSpeed * 0.1f) * Time.deltaTime;
            this.transform.localPosition = newLocalPos;
            if (newLocalPos.y <= 0.04f)
            {
                newLocalPos.y = 0.04f;
                StopSlide();
                return; // Exit the function early
            }
        }
        else if (!leftBoolArray[2] || !rightBoolArray[2])
        {
            StopSlide();
            UpdateClosestSpot();
        }
    }

    void StopSlide()
    {
        slideCountDown = false;
        timer = 0f;
        //Vector3 newLocalPos = this.transform.localPosition;
        //newLocalPos.y = 1;
    }

    //not in use
    IEnumerator MoveToNewPos(Vector3 newPos)
    {
        //check for closest sign each time player moves
        UpdateClosestSpot();

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

    //not in use
    Vector3 NewPosition()
    {
        transform.position += this.transform.up * moveDist;
        //Vector3 newPosition = transform.position + new Vector3(0, moveDist, 0);
        Vector3 newPosition = transform.position;

        return newPosition;
    }

    void MovePlayer()
    {
        anim.SetBool("isMoving", true);
        isMoving = true;
        MoveSpeedControl();
        Vector3 newLocalPos = this.transform.localPosition;
        newLocalPos.y += moveSpeed * Time.deltaTime;
        this.transform.localPosition = newLocalPos;
    }

    void MoveSpeedControl()
    {
        if (!sD.insideSteam)
        {
            moveSpeed = moveOriSpeed;
            //Debug.Log("moveSpeed = " + moveSpeed);
        }
        else
        {
            moveSpeed = moveSlowSpeed;
            //Debug.Log("moveSpeed = " + moveSpeed);
        }
    }
    #endregion

    #region Repair Methods
    private void OnTriggerEnter(Collider other)
    {
        GameObject enteredSpot = other.gameObject;
        //if the sign is trigger range has the word sign in object name, and this sign has not yet been added to the list of signs in range
        if (other.gameObject.name.Contains("sign") && !spotsToFix.Contains(enteredSpot))
        {
            //Debug.Log("current screw type is " + currentScrew);

            //add sign to signs list
            spotsToFix.Add(enteredSpot);
            UpdateClosestSpot();
            //convert sign position to local position relative to player
            //Vector3 signLocalPos = this.transform.InverseTransformPoint(closestSign.gameObject.transform.position);
            //if (signLocalPos.x < 0)
            //{
            //    signOnLeft = true;
            //    Debug.Log("left sign");
            //}
            //else if (signLocalPos.x > 0)
            //{
            //    signOnRight = true;
            //    Debug.Log("right sign");
            //}
        }
        else
        {
            //Debug.Log("no sign to repair");
            return;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        GameObject exitedSpot = other.gameObject;
        //same logic as enter
        if (other.gameObject.name.Contains("sign") && spotsToFix.Contains(exitedSpot))
        {
            spotsToFix.Remove(exitedSpot);
            UpdateClosestSpot();
        }

    }

    void UpdateClosestSpot()
    {
        //if(lightcontrolRef != null)
        //{
        //    lightcontrolRef.isSelected = false;
        //}
        closestSpot = null;

        float closestDist = Mathf.Infinity;
        Vector3 playerPos = this.transform.position;

        //if there are signs in the signs to fix list
        if (spotsToFix.Count != 0)
        {
            //iterate through each sign
            foreach (GameObject spot in spotsToFix)
            {
                //spot.GetComponent<LightControl>().isSelected = false;
                //get the distance from that sign to the player
                float distance = Vector3.Distance(spot.transform.position, playerPos);
                // if the new distance is shorter than from the previous sign or from the initial value
                if (distance < closestDist)
                {
                    //the closest dist is the new dist
                    closestDist = distance;
                    closestSpot = spot;
                    //closestSpot.GetComponent<LightControl>().isSelected = true;
                    

                    //if (closestSpot.gameObject.GetComponent<LightControl>() != null)
                    //{
                    //    lightcontrolRef = closestSpot.gameObject.GetComponent<LightControl>();
                    //    break;
                    //}

                    //set the scew type to the closest sign
                    SetScrewType();

                    //get light control ref to toggle VFX and sign fix status
                    foreach (Transform signPart in closestSpot.transform)
                    {
                        if (signPart.gameObject.GetComponent<LightControl>() != null)
                        {
                            lightcontrolRef = signPart.gameObject.GetComponent<LightControl>();
                            //lightcontrolRef.isSelected = true;
                            //lightcontrolRef.selectionRing.SetActive(true);
                            break;
                        }
                    }
                }
            }
        }
    }

    //void DrawLine()
    //{
    //    lineRend.startWidth = lineWidth;
    //    lineRend.endWidth = lineWidth;

    //    Vector3[] positions = new Vector3[2];
    //    positions[0] = this.transform.position;
    //    positions[1] = closestSpot.transform.position;

    //    lineRend.positionCount = positions.Length;
    //    lineRend.SetPositions(positions);
    //}

    void SetScrewType()
    {
        // if there is a closest sign
        if(closestSpot != null)
        {
            //check what kind of sign it is
            if (closestSpot.gameObject.CompareTag("cross"))
            {
                currentScrew = ScrewType.CrossScrew;
            }
            if (closestSpot.gameObject.CompareTag("flat"))
            {
                currentScrew = ScrewType.FlatScrew;
            }
            if (closestSpot.gameObject.CompareTag("hex"))
            {
                currentScrew = ScrewType.HexScrew;
            }
        }
    }

    public void FixSign()
    {
        if (closestSpot != null && (leftHandOffLadder || rightHandOffLadder))
        {
            repairAudio.PlayOneShot(repairClip);
            //change fix status in light control ref
            lightcontrolRef.isFixed = true;
            //removed the closest sign that was just fixed
            spotsToFix.Remove(closestSpot);
            UpdateClosestSpot();
            //Destroy(closestSign);
            //add score function
            ScoreManager.instance.AddPoint(5);

            //repair animation
        }
        //if (signOnRight && (leftHandOffLadder || rightHandOffLadder) && Keyboard.current[Key.S].wasPressedThisFrame)
        //{
        //    Collider signCollider = closestSign.gameObject.GetComponent<BoxCollider>();
        //    signCollider.enabled = false;

        //    Debug.Log("sign repaired on the right");
        //    //repair animation
        //}
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
        this.transform.rotation = ladder.transform.rotation;

        //transform.LookAt(target[player2.num].position);

        //player's position and rotation follows to the ladder
        //this.transform.position = new Vector3(target[player2.num].position.x + offset, this.transform.position.y, ladder.transform.position.z) ;
        //transform.position = target.position + offset;
        //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        //Quaternion newRotation = Quaternion.Euler(0, 0, ladder.transform.rotation.eulerAngles.z);
        //transform.rotation = newRotation;


    }

    #endregion

    #region Height confine
    void DetectPlayerPosition()
    {
        if (this.transform.position.y <= target[0].transform.position.y && player2.numOfLadder ==1)
        {
            num = 0;
            reachMax2 = false;
            reachMax3 = false;
            reachMax4 = false;
            reachMax5 = false;
            //Debug.Log("num = " + num);
            //Debug.Log("targerPosition = " + target[0].transform.position.y);

        }
        else if (this.transform.position.y > target[0].transform.position.y && this.transform.position.y <= target[1].transform.position.y - offset && player2.numOfLadder == 2)
        {
            num = 1;
            reachMax1 = false;
            reachMax3 = false;
            reachMax4 = false;
            reachMax5 = false;
            //Debug.Log("num = " + num);
            //Debug.Log("targerPosition = " + target[1]);
        }
        else if (this.transform.position.y > target[1].transform.position.y && this.transform.position.y <= target[2].transform.position.y - offset && player2.numOfLadder == 3)
        {
            num = 2;
            reachMax1 = false;
            reachMax2 = false;
            reachMax4 = false;
            reachMax5 = false;
            //Debug.Log("num = " + num);
            //Debug.Log("targerPosition = " + target[2]);
        }
        else if (this.transform.position.y > target[2].transform.position.y && this.transform.position.y <= target[3].transform.position.y - offset && player2.numOfLadder == 4)
        {
            num = 3;
            reachMax1 = false;
            reachMax2 = false;
            reachMax3 = false;
            reachMax5 = false;
            Debug.Log("num = " + num);
            //Debug.Log("targerPosition = " + target[3]);
        }
        else if (this.transform.position.y > target[3].transform.position.y && this.transform.position.y <= target[4].transform.position.y - offset && player2.numOfLadder == 5)
        {
            num = 4;
            reachMax1 = false;
            reachMax2 = false;
            reachMax3 = false;
            reachMax4 = false;
            //Debug.Log("num = " + num);
            //Debug.Log("targerPosition = " + target[4]);
        }
    }


    void DetectReachMaxHeight()
    {
        if (this.transform.position.y >= target[0].transform.position.y && player2.numOfLadder == 1)
        {
            reachMax1 = true;
            //Debug.Log("reachMax1: " + reachMax1);

        }
        else if (this.transform.position.y >= target[1].transform.position.y && player2.numOfLadder == 2)
        {
            reachMax2 = true;
            //Debug.Log("reachMax2: " + reachMax2);
        }
        else if (this.transform.position.y >= target[2].transform.position.y && player2.numOfLadder == 3) 
        {
            reachMax3 = true;
            //Debug.Log("reachMax3: " + reachMax3);
        }
        else if (this.transform.position.y >= target[3].transform.position.y && player2.numOfLadder == 4)
        {
            reachMax4 = true;
            //Debug.Log("reachMax4: " + reachMax4);
        }
        else if (this.transform.position.y >= target[4].transform.position.y && player2.numOfLadder == 5)
        {
            reachMax5 = true;
            //Debug.Log("reachMax5: " + reachMax5);
        }
    }
    #endregion

    void ConfineLadderHeight()
    {
        float offSet = - 0.1f;
        if (num > 0 && transform.position.y > target[0].transform.position.y && player2.numOfLadder == 1)
        {
            Vector3 charPos = target[0].transform.position;
            charPos.x = target[0].transform.position.x - offSet;
            charPos.y = target[0].transform.position.y;
            charPos.z = target[0].transform.position.z + offSet;
            transform.position = charPos;
        } 
        else if(num > 1 && transform.position.y > target[1].transform.position.y && player2.numOfLadder == 2)
        {
            Vector3 charPos = target[1].transform.position;
            charPos.x = target[1].transform.position.x - offSet; 
            charPos.y = target[1].transform.position.y;
            charPos.z = target[0].transform.position.z + offSet;
            transform.position = charPos;
        }
        else if (num > 2 && transform.position.y > target[2].transform.position.y && player2.numOfLadder == 3)
        {
            Vector3 charPos = target[2].transform.position;
            charPos.x = target[2].transform.position.x - offSet;
            charPos.y = target[2].transform.position.y;
            charPos.z = target[0].transform.position.z + offSet;
            transform.position = charPos;
        }
        else if (num > 3 && transform.position.y > target[3].transform.position.y && player2.numOfLadder == 4)
        {
            Vector3 charPos = target[3].transform.position;
            charPos.x = target[3].transform.position.x - offSet;
            charPos.y = target[3].transform.position.y;
            charPos.z = target[0].transform.position.z + offSet;
            transform.position = charPos;
        }
        else if (num > 4 && transform.position.y > target[4].transform.position.y && player2.numOfLadder == 5)
        {
            Vector3 charPos = target[4].transform.position;
            charPos.x = target[4].transform.position.x - offSet;
            charPos.y = target[4].transform.position.y;
            charPos.z = target[0].transform.position.z + offSet;
            transform.position = charPos;
        }
    }


    #region Warning
    void Warn()
    {
        if (handsOff)
        {
            warningTimer -= Time.deltaTime;
            p1Screen.SetActive(true);
            p2Screen.SetActive(true);

            if (warningTimer <= 0)
            {
                //Debug.Log("Game Over - HandsOff");
                gameOver = true;
                handsOff = false;
                warningTimer = timerValue;
                SceneManager.LoadScene("GameOver");
            }else if (!leftHandOffLadder || !rightHandOffLadder)
            {
                handsOff = false;
                warningTimer = timerValue;
                p1Screen.SetActive(false);
                p2Screen.SetActive(false);
            }
        }
       
        p1Text.text = (warningTimer).ToString("0");
        p2Text.text = (warningTimer).ToString("0");
    }
    #endregion
}