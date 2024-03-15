using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.UI;

public class PlayerOneController : MonoBehaviour
{
    public static PlayerOneController instance { get; private set; }
    //reference variables
    Rigidbody playerRb;
    GameObject ladder;

    //hand position variables
    bool leftHandOffLadder = true;
    bool rightHandOffLadder = true;
    //checks if the player grabbed the ladder for the frst time
    [SerializeField]
    public bool gameStart1, gameStart2, gameOver, isDead;

    /*rail spot booleans, 5 spots on each side, 0 = uppper most position, 4 = lower most position 
    */
    [SerializeField]
    bool[] leftBoolArray = new bool[5];
    [SerializeField]
    bool[] rightBoolArray = new bool[5];

    public GameObject[] leftPosUIArray = new GameObject[5];
    public GameObject[] rightPoseUIArray = new GameObject[5];

    /*04 = left hand at top, right hand at bottom
    40 = left hand at bottom, right hand at top
    */
    [SerializeField]
    bool performed04, performed40;

    /*movement variables, change in inspector, don't change here
     * smaller speed value = faster lerp
     */
    [SerializeField] public float moveDist = 1.1f;
    [SerializeField] float moveSpeed;
    [SerializeField] float moveOriSpeed;
    [SerializeField] float moveBoostedSpeed;
    [SerializeField] float moveSlowSpeed;
    [SerializeField] float gravityMod;
    [SerializeField] float slideDownSpeed;
    //drill variables
    //public bool isInDrillSlot;

    //repair variables
    SphereCollider repairCollide;
    //repair radius, change in inspector
    [SerializeField] float detectionRadius = 5f;
    //sign positional bool, currently unsed
    public bool signOnLeft, signOnRight;
    //variables to get closest sign


    //light emission control variables
    [SerializeField]
    LightControl lightcontrolRef;
    public AudioSource repairAudio;
    [SerializeField]
    public AudioClip repairClip;


    [Header("Repair Spot")]
    //follow ladder position
    public Transform[] target;
    public float offset;
    public PlayerTwoController player2;
    [SerializeField]
    private GameObject minimapIcon;
    [SerializeField]
    public List<GameObject> spotsToFix = new List<GameObject>();
    [SerializeField]
    public GameObject closestSpot;

    [SerializeField]
    public int num;


    [Header("Animation")]
    [SerializeField]
    public Animator anim;
    public bool isMoving;
    [SerializeField]
    public Transform player2Pos;
    float distance;
    float LateDis;
    [SerializeField]
    public bool handsOff = false;
    [SerializeField]
    public bool isClimbing;

    [SerializeField]
    GameObject p1Screen;
    [SerializeField]
    GameObject p2Screen;
    [SerializeField]
    TMP_Text p1Text;
    [SerializeField]
    TMP_Text p2Text;
    [SerializeField]
    public float warningTimer = 5f;
    [SerializeField]
    float timerValue;
    [SerializeField]
    AudioSource audio;

    [Header("Slide Down")]
    [SerializeField]
    private bool canSlideDown;
    [SerializeField]
    private float timer;
    [SerializeField] 
    private float slideHoldTime;
    [SerializeField] 
    private bool slideCountDown;

    [Header("Shop")]
    [SerializeField]
    public bool hasChanged;
    [SerializeField]
    public bool isFreezed;
    [SerializeField]
    public int numItem;
    [SerializeField]
    public bool isBoosted;
    [SerializeField]
    public float timerBoosted;


    [Header("Winning Condition")]
    [SerializeField]
    public int numberOfSignhasBeenFixed;
    [SerializeField]
    public int totalAmountSignNeedToBeFixed;
    [SerializeField]
    private GameObject scoreBoardUI;
    [SerializeField]
    private GameObject scoreBoardUI1;
    [SerializeField]
    private GameObject scoreBoardSelection1_1;
    [SerializeField]
    private GameObject scoreBoardSelection1_2;
    [SerializeField]
    private GameObject scoreBoardSelection1_3;
    [SerializeField]
    private GameObject scoreBoardSelection2_1;
    [SerializeField]
    private GameObject scoreBoardSelection2_2;
    [SerializeField]
    private GameObject scoreBoardSelection2_3;
    [SerializeField]
    private Text gradeText;
    [SerializeField]
    private Text gradeText1;
    [SerializeField]
    private Text bonusText;
    [SerializeField]
    private Text bonusText1;
    [SerializeField]
    public bool gameEnd;
    [SerializeField]
    public bool scoreBoardIsTurnnedOn;
    [SerializeField]
    private AudioSource winningSound;
    [SerializeField]
    private float bonusCoin;
    [SerializeField]
    private float finalScore;

    [Header("Shop")]
    [SerializeField]
    public bool delayShopUI;
    [SerializeField]
    public bool delayScoreboardUI;

    public enum ScrewType
    {
        CrossScrew,
        FlatScrew,
        HexScrew,
        SuperDrill,
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

        moveOriSpeed = moveSpeed;

        distance = Vector2.Distance(this.transform.position, player2Pos.transform.position);

    }

    #region Update Methods
    // Update is called once per frame
    void Update()
    {
        MoveSpeedControl();
        if (Timer.instance.gameStart && !gameEnd && !Timer.instance.inTutorial && GameManager.instance.currentScene.name == "Level_Tutorial_01")
        {
            if (!isFreezed)
            {
                SlideDown();
                Warn();
            }
            CheckLeftHand();
            CheckRightHand();

            CheckLeftRail();
            CheckRightRail();

            UpdateClosestSpot();
            //FollowLadder();
            DetectPlayerPosition();
            //DetectReachMaxHeight();
            
            ConfineLadderHeight();

            
            ClimbUp();
        }

        if(Timer.instance.gameStart && !gameEnd && GameManager.instance.currentScene.name != "Level_Tutorial_01")
        {
            if (!isFreezed)
            {
                SlideDown();
                Warn();
                print("slide");

            }
            CheckLeftHand();
            CheckRightHand();

            CheckLeftRail();
            CheckRightRail();

            UpdateClosestSpot();
            //FollowLadder();
            DetectPlayerPosition();
            //DetectReachMaxHeight();

            ConfineLadderHeight();
            ClimbUp();
        }

    }

    private void FixedUpdate()
    {
        if (Timer.instance.gameStart && !gameEnd && !Timer.instance.inTutorial && GameManager.instance.currentScene.name == "Level_Tutorial_01")
        {

            if (!isFreezed)
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

                }
                else if (leftHandOffLadder && rightHandOffLadder && gameStart1 && gameStart2)
                {
                    handsOff = true;
                    anim.SetBool("isHanging", true);
                    //SceneManager.LoadScene("GameOver");
                }


            }

        }

        if(Timer.instance.gameStart && !gameEnd && GameManager.instance.currentScene.name != "Level_Tutorial_01")
        {
            if (!isFreezed)
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

                }
                else if (leftHandOffLadder && rightHandOffLadder && gameStart1 && gameStart2)
                {
                    handsOff = true;
                    anim.SetBool("isHanging", true);
                    //SceneManager.LoadScene("GameOver");
                }


            }
        }



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
        //if (Keyboard.current[Key.A].wasPressedThisFrame && !leftHandOffLadder)
        if(Input.GetKey(KeyCode.A) && !leftHandOffLadder)
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
        //if (Keyboard.current[Key.D].wasPressedThisFrame && !rightHandOffLadder)
        if(Input.GetKey(KeyCode.D) && !rightHandOffLadder)
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
        if (!gameOver)
        {
            //if (Keyboard.current[Key.T].wasPressedThisFrame)
            //{
            //    ResetLeftBool(0);
            //}
            //if (Keyboard.current[Key.Y].wasPressedThisFrame)
            //{
            //    ResetLeftBool(1);
            //}
            //if (Keyboard.current[Key.G].wasPressedThisFrame)
            //{
            //    ResetLeftBool(2);
            //}
            //if (Keyboard.current[Key.H].wasPressedThisFrame)
            //{
            //    ResetLeftBool(3);
            //}
            //if (Keyboard.current[Key.B].wasPressedThisFrame)
            //{
            //    ResetLeftBool(4);
            //}
            if (Input.GetKeyDown(KeyCode.T))
            {
                ResetLeftBool(0);
                EnterShop.instance.SelectItem();

                if (EnterShop.instance.firstEnter)
                {
                    EnterShop.instance.selectionSound.Play();
                }

            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                ResetLeftBool(1);
                EnterShop.instance.SelectItem();

                if (EnterShop.instance.firstEnter)
                {
                    EnterShop.instance.selectionSound.Play();
                }
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                ResetLeftBool(2);
                EnterShop.instance.SelectItem();

                if (EnterShop.instance.firstEnter)
                {
                    EnterShop.instance.selectionSound.Play();
                }
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                ResetLeftBool(3);
                EnterShop.instance.SelectItem();

                if (EnterShop.instance.firstEnter)
                {
                    EnterShop.instance.selectionSound.Play();
                }
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                ResetLeftBool(4);
                EnterShop.instance.SelectItem();

                if (EnterShop.instance.firstEnter)
                {
                    EnterShop.instance.selectionSound.Play();
                }
            }
        }

    }

    void CheckRightRail()
    {
        if (!gameOver)
        {
            //if (Keyboard.current[Key.O].wasPressedThisFrame)
            //{
            //    ResetRightBool(0);
            //}
            //if (Keyboard.current[Key.I].wasPressedThisFrame)
            //{
            //    ResetRightBool(1);
            //}
            //if (Keyboard.current[Key.K].wasPressedThisFrame)
            //{
            //    ResetRightBool(2);
            //}
            //if (Keyboard.current[Key.J].wasPressedThisFrame)
            //{
            //    ResetRightBool(3);
            //}
            //if (Keyboard.current[Key.M].wasPressedThisFrame)
            //{
            //    ResetRightBool(4);
            //}
            if (Input.GetKey(KeyCode.O))
            {
                ResetRightBool(0);
                EnterShop.instance.SelectItem();

                if (EnterShop.instance.firstEnter)
                {
                    EnterShop.instance.selectionSound.Play();
                }

            }
            if (Input.GetKey(KeyCode.I))
            {
                ResetRightBool(1);
                EnterShop.instance.SelectItem();

                if (EnterShop.instance.firstEnter)
                {
                    EnterShop.instance.selectionSound.Play();
                }

            }
            if (Input.GetKey(KeyCode.K))
            {
                ResetRightBool(2);
                EnterShop.instance.SelectItem();

                if (EnterShop.instance.firstEnter)
                {
                    EnterShop.instance.selectionSound.Play();
                }

            }
            if (Input.GetKey(KeyCode.J))
            {
                ResetRightBool(3);
                EnterShop.instance.SelectItem();

                if (EnterShop.instance.firstEnter)
                {
                    EnterShop.instance.selectionSound.Play();
                }

            }
            if (Input.GetKey(KeyCode.M))
            {
                ResetRightBool(4);
                EnterShop.instance.SelectItem();

                if (EnterShop.instance.firstEnter)
                {
                    EnterShop.instance.selectionSound.Play();
                }

            }
        }
    }

    //set entire array to false;
    void ResetLeftBool()
    {
        for (int i = 0; i < leftBoolArray.Length; i++)
        {
            leftBoolArray[i] = false;
            //leftPosUIArray[i].SetActive(true);
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
            //rightPoseUIArray[i].SetActive(true);
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
        if (!performed04 && !leftHandOffLadder && !rightHandOffLadder && !gameOver && !FalconAttack.instance.isStunning)

        {
            if (leftBoolArray[0] && rightBoolArray[4])
            {
                MovePlayer();
                // no animation execute
                performed40 = false;
                performed04 = true;
            }
            else if (!leftBoolArray[0] || !rightBoolArray[4])
            {
                if (!isClimbing)
                {
                    isMoving = false;
                    anim.SetFloat("MoveSpeed", 0);
                }
            }
        }
        else if (!performed40 && !leftHandOffLadder && !rightHandOffLadder && !gameOver && !FalconAttack.instance.isStunning)
        {
            if (leftBoolArray[4] && rightBoolArray[0])
            {
                MovePlayer();
                performed04 = false;
                performed40 = true;
            }
            else if (!leftBoolArray[4] || !rightBoolArray[0])
            {
                if (!isClimbing)
                {
                    isMoving = false;
                    anim.SetFloat("MoveSpeed", 0);
                }

            }
        }

        if (leftHandOffLadder || rightHandOffLadder)
        {
            isMoving = false;
            anim.SetFloat("MoveSpeed", 0);
        }

    }



    void SlideDown()
    {
        Vector3 newLocalPos1 = this.transform.localPosition;
        //Debug.Log(newLocalPos1);

        //at hand position 2,2, start slideCoundDown
        if (leftBoolArray[4] && rightBoolArray[4] && !slideCountDown && !FalconAttack.instance.isStunning)
        {
            slideCountDown = true;
            timer = 0f;
        }

        if (slideCountDown && canSlideDown)
        {
            timer += Time.deltaTime;
        }

        //enable modified gravity when hold threashold is reached
        if (leftBoolArray[4] && rightBoolArray[4] && timer >= slideHoldTime)
        {
            Vector3 newLocalPos = this.transform.localPosition;
            newLocalPos.y -= (slideDownSpeed * 0.1f) * Time.deltaTime;
            this.transform.localPosition = newLocalPos;
            anim.SetBool("isSliding", true);
            if (newLocalPos.y <= 0.045f)
            {
                newLocalPos.y = 0.045f;
                StopSlide();
                return; // Exit the function early
            }

        }
        else if (!leftBoolArray[4] || !rightBoolArray[4])
        {
            StopSlide();
            UpdateClosestSpot();
        }
    }

    void StopSlide()
    {
        slideCountDown = false;
        timer = 0f;
        anim.SetBool("isSliding", false);
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
        isMoving = true;
        isClimbing = true;
        //MoveSpeedControl();
        anim.SetFloat("MoveSpeed", moveSpeed);
        StartCoroutine(DoClimb());
        StartCoroutine(StopClimbingAnimation());
        //print("MoveSpeed" + moveSpeed)

    }

    IEnumerator StopClimbingAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        anim.SetFloat("MoveSpeed", 0);
        isClimbing = false;
        print("stop");
    }

    IEnumerator DoClimb()
    {
        yield return new WaitForSeconds(0.2f);
        Vector3 newLocalPos = this.transform.localPosition;
        newLocalPos.y += moveSpeed * Time.deltaTime;
        this.transform.localPosition = newLocalPos;
    }

    void MoveSpeedControl()
    {

        if (EnterShop.instance.isPurchased3)
        {
            moveSpeed = moveBoostedSpeed;
        }
            //Debug.Log("moveSpeed = " + moveSpeed);
        
        //else
        //{
        //    moveSpeed = moveSlowSpeed;
        //    //Debug.Log("moveSpeed = " + moveSpeed);
        //}
    }
    #endregion

    #region Repair Methods
    private void OnTriggerStay(Collider other)
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
            Vector3 signLocalPos = this.transform.InverseTransformPoint(closestSpot.gameObject.transform.position);
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
        //if (lightcontrolRef != null)
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
                            minimapIcon = closestSpot.transform.Find("Sign").gameObject;
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
            if (EnterShop.instance.isPurchased2)
            {
                //check what kind of sign it is
                if (closestSpot.gameObject.CompareTag("cross") || closestSpot.gameObject.CompareTag("flat") || closestSpot.gameObject.CompareTag("hex"))
                {
                    currentScrew = ScrewType.SuperDrill;
                }
            }
            else
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
    }

    public void FixSign()
    {
        if (closestSpot != null && (leftHandOffLadder || rightHandOffLadder))
        {
            if(minimapIcon != null )
            {
                repairAudio.PlayOneShot(repairClip);
                //change fix status in light control ref
                lightcontrolRef.isFixed = true;
                //removed the closest sign that was just fixed
                spotsToFix.Remove(closestSpot);
                closestSpot.gameObject.GetComponent<BoxCollider>().enabled = false;
                minimapIcon.GetComponent<SpriteRenderer>().enabled = false;
                UpdateClosestSpot();
                //Destroy(closestSign);
                //add score function
                StartCoroutine(FixingAnimation());
                //print("11");
                ScoreManager.instance.AddPoint(100);
                numberOfSignhasBeenFixed += 1;
                //print("fixed");

                ScoreManager.instance.progressionText1.text = numberOfSignhasBeenFixed.ToString() + " / " + totalAmountSignNeedToBeFixed.ToString();
                 
                if (numberOfSignhasBeenFixed >= totalAmountSignNeedToBeFixed && !scoreBoardIsTurnnedOn)
                {

                    winningSound.Play();
                    StartCoroutine(ScoreBoard());
                    Timer.instance.AssignGrade();

                    if (Timer.instance.isGradeA)
                    {
                        if(GameManager.instance.currentScene.name == "Level_Tutorial_01")
                        {
                            bonusCoin = 300;
                            finalScore = ScoreManager.instance.score + bonusCoin;
                        }

                        if (GameManager.instance.currentScene.name == "Level_01" || GameManager.instance.currentScene.name == "Level_02" || GameManager.instance.currentScene.name == "Level_03")
                        {
                            bonusCoin = 400;
                            finalScore = ScoreManager.instance.score + bonusCoin;
                        }

                        if(GameManager.instance.currentScene.name == "Level_04")
                        {
                            bonusCoin = 600;
                            finalScore = ScoreManager.instance.score + bonusCoin;
                        }

                        gradeText.text = "A";
                        gradeText1.text = "A";
                        //bonusText.text = "+500";
                        //bonusText1.text = "+500";
                        bonusText.text = bonusCoin.ToString();
                        bonusText1.text = bonusCoin.ToString();
                        ScoreManager.instance.AddPoint(finalScore);
                    }
                    if (Timer.instance.isGradeB)
                    {
                        if (GameManager.instance.currentScene.name == "Level_Tutorial_01")
                        {
                            bonusCoin = 450;
                            finalScore = ScoreManager.instance.score + bonusCoin;
                        }

                        if (GameManager.instance.currentScene.name == "Level_01" || GameManager.instance.currentScene.name == "Level_02" || GameManager.instance.currentScene.name == "Level_03")
                        {
                            bonusCoin = 600;
                            finalScore = ScoreManager.instance.score + bonusCoin;
                        }

                        if (GameManager.instance.currentScene.name == "Level_04")
                        {
                            bonusCoin = 900;
                            finalScore = ScoreManager.instance.score + bonusCoin;
                        }
                        gradeText.text = "B";
                        gradeText1.text = "B";
                        //bonusText.text = "+300";
                        //bonusText1.text = "+300";
                        bonusText.text = bonusCoin.ToString();
                        bonusText1.text = bonusCoin.ToString();
                        ScoreManager.instance.AddPoint(finalScore);
                    }
                    if (Timer.instance.isGradeC)
                    {
                        if (GameManager.instance.currentScene.name == "Level_Tutorial_01")
                        {
                            bonusCoin = 390;
                            finalScore = ScoreManager.instance.score + bonusCoin;
                        }

                        if (GameManager.instance.currentScene.name == "Level_01" || GameManager.instance.currentScene.name == "Level_02" || GameManager.instance.currentScene.name == "Level_03")
                        {
                            bonusCoin = 520;
                            finalScore = ScoreManager.instance.score + bonusCoin;
                        }

                        if (GameManager.instance.currentScene.name == "Level_04")
                        {
                            bonusCoin = 780;
                            finalScore = ScoreManager.instance.score + bonusCoin;
                        }

                        gradeText.text = "C";
                        gradeText1.text = "C";
                        //bonusText.text = "+200";
                        //bonusText1.text = "+200";
                        bonusText.text = bonusCoin.ToString();
                        bonusText1.text = bonusCoin.ToString();
                        ScoreManager.instance.AddPoint(finalScore);
                    }

                    gameEnd = true;
                    StartCoroutine(DelayShopFunction());
                }
            }
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

    IEnumerator ScoreBoard()
    {
        yield return new WaitForSeconds(0.5f);
        scoreBoardUI.SetActive(true);
        scoreBoardUI1.SetActive(true);
        scoreBoardIsTurnnedOn = true;
    }

    IEnumerator FixingAnimation()
    {
        anim.SetBool("isFixing", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isFixing", false);
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
        if (this.transform.position.y < target[0].transform.position.y && player2.numOfLadder == 1)
        {
            num = 0;
        }

        else if (this.transform.position.y > target[0].transform.position.y && this.transform.position.y < target[1].transform.position.y - offset && player2.numOfLadder == 2)
        {
            num = 1;
        }
        else if (this.transform.position.y > target[1].transform.position.y && this.transform.position.y < target[2].transform.position.y - offset && player2.numOfLadder == 3)
        {
            num = 2;
        }
        else if (this.transform.position.y > target[2].transform.position.y && this.transform.position.y < target[3].transform.position.y - offset && player2.numOfLadder == 4)
        {
            num = 3;
        }
        else if (this.transform.position.y > target[3].transform.position.y && this.transform.position.y < target[4].transform.position.y - offset && player2.numOfLadder == 5)
        {
            num = 4;
        }

    }

    void ConfineLadderHeight()
    {
        num = player2.numOfLadder - 1;

        if (num >= 0 && transform.position.y >= target[0].transform.position.y && player2.numOfLadder == 1)
        {
            Vector3 charPos = target[0].transform.position;
            charPos.x = target[0].transform.position.x;
            charPos.y = target[0].transform.position.y;
            //charPos.z = target[0].transform.position.z ;
            transform.position = charPos;
        }
        else if (num >= 1 && transform.position.y >= target[1].transform.position.y && player2.numOfLadder == 2)
        {
            Vector3 charPos = target[1].transform.position;
            charPos.x = target[1].transform.position.x;
            charPos.y = target[1].transform.position.y;
            //charPos.z = target[0].transform.position.z;
            transform.position = charPos;
        }
        else if (num >= 2 && transform.position.y >= target[2].transform.position.y && player2.numOfLadder == 3)
        {
            Vector3 charPos = target[2].transform.position;
            charPos.x = target[2].transform.position.x;
            charPos.y = target[2].transform.position.y;
            //charPos.z = target[0].transform.position.z;
            transform.position = charPos;
        }
        else if (num >= 3 && transform.position.y >= target[3].transform.position.y && player2.numOfLadder == 4)
        {
            Vector3 charPos = target[3].transform.position;
            charPos.x = target[3].transform.position.x;
            charPos.y = target[3].transform.position.y;
            //charPos.z = target[0].transform.position.z;
            transform.position = charPos;
        }
        else if (num >= 4 && transform.position.y >= target[4].transform.position.y && player2.numOfLadder == 5)
        {
            Vector3 charPos = target[4].transform.position;
            charPos.x = target[4].transform.position.x;
            charPos.y = target[4].transform.position.y;
            //charPos.z = target[0].transform.position.z;
            transform.position = charPos;
        }
        
        if(transform.position.y <= target[5].transform.position.y)
        {
            Vector3 charPos = target[5].transform.position;
            charPos.x = target[5].transform.position.x;
            charPos.y = target[5].transform.position.y;
            //charPos.z = target[0].transform.position.z;
            transform.position = charPos;
            canSlideDown = false;
        }
        else
        {
            canSlideDown = true;
        }


    }

    #endregion

    #region Warning
    public void Warn()
    {

        if (handsOff)
        {
            if(GameManager.instance.currentScene.name == "Level_Tutorial_01")
            {
                if (Timer.instance.inTutorial)
                {
                    p1Screen.SetActive(false);
                    p2Screen.SetActive(false);
                }
                else
                {
                    warningTimer -= Time.deltaTime;
                    p1Screen.SetActive(true);
                    p2Screen.SetActive(true);
                }
            }
            else
            {
                warningTimer -= Time.deltaTime;
                p1Screen.SetActive(true);
                p2Screen.SetActive(true);
            }


            audio.Play();

            p1Text.text = (warningTimer).ToString("0");
            p2Text.text = (warningTimer).ToString("0");
            

            if (warningTimer <= 0 || timerBoosted <= 0)
            {
                //Debug.Log("Game Over - HandsOff");
                gameOver = true;
                handsOff = false;
                anim.SetTrigger("GameOver");
                //warningTimer = timerValue;
                StartCoroutine(FallingAnim());
                p1Text.text = "0";
                p2Text.text = "0";
            }
            else if (!leftHandOffLadder || !rightHandOffLadder)
            {
                handsOff = false;
                warningTimer = timerValue;
                anim.SetBool("isHanging", false);
                p1Screen.SetActive(false);
                p2Screen.SetActive(false);
            }
        }
       

        if (isDead)
        {
            anim.SetTrigger("GameOver");
            StartCoroutine(FallingAnim());
        }
    }

    IEnumerator FallingAnim()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOver");
    }
    #endregion

    IEnumerator DelayShopFunction()
    {
        yield return new WaitForSeconds(3f);
        delayScoreboardUI = true;

        if(GameManager.instance.currentScene.name != "Level_04")
        {
            scoreBoardSelection1_1.SetActive(true);
            scoreBoardSelection1_2.SetActive(true);
            scoreBoardSelection1_3.SetActive(true);

            scoreBoardSelection2_1.SetActive(true);
            scoreBoardSelection2_2.SetActive(true);
            scoreBoardSelection2_3.SetActive(true);
        }
        else
        {
            //scoreBoardSelection1_1.SetActive(true);
            scoreBoardSelection1_2.SetActive(true);
            scoreBoardSelection1_3.SetActive(true);

            //scoreBoardSelection2_1.SetActive(true);
            scoreBoardSelection2_2.SetActive(true);
            scoreBoardSelection2_3.SetActive(true);
        }

    }
}
