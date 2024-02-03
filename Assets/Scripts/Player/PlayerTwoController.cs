using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerTwoController : MonoBehaviour
{
    //ladder variables
    [SerializeField] float ladderHeight = 10f;
    [SerializeField] float heightChanges;
    //Set ladderHeight
    //height rate changes

    PlayerOneController player1;


    //ladder movement variables
    [SerializeField] float moveLadderSpeed;
    [SerializeField] float moveLadderDist;
    bool moveLeft = false;
    bool moveRight = false;
    Rigidbody ladderRb;

    //public GameObject ladder;
    Vector3 rotation;
    float value;
    bool rotateLeft;
    bool rotateRight;
    bool rotateFast;
    bool rotateNormal;

    //Ladder heights 
    public GameObject[] ladderObj;
    public int numOfLadder;

    public GameObject pivotPoint;
    public GameObject player;
    float minY;
    float maxY;
    [SerializeField]
    GameObject normalLeft;
    [SerializeField]
    GameObject normaRight;
    [SerializeField]
    GameObject fastLeft;
    [SerializeField]
    GameObject fastRight;
    [SerializeField]
    GameObject neutral;

    [SerializeField]
    Graphic leftRotation;
    [SerializeField]
    Graphic rightRotation;
    Color pressColor;
    Color releaseColor;

    [SerializeField]
    private int randValue;
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private float multipleValue;
    [SerializeField]
    private bool isRight;
    [SerializeField]
    private bool isLeft;
    [SerializeField]
    private bool isTriggered;

    private int windValue;
    [SerializeField]
    private float startTime;

    // Start is called before the first frame update

    void Awake()
    {
        player1 = PlayerOneController.instance;

    }
    void Start()
    {
        pressColor = Color.red;
        releaseColor = Color.white;

        currentState = LadderStates.Neutral;
        ladderHeight = this.transform.localScale.y;
        ladderRb = this.GetComponent<Rigidbody>();

        player1 = player.GetComponent<PlayerOneController>();

        //set default Rotation Speed 

        moveLadderSpeed = 0;
        moveLadderDist = 1f;
        maxY = 3;
        numOfLadder = 1;

        value = Random.Range(1, 10);

    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.instance.gameStart)
        {
            SpeedAdjust();
            LadderRotate();
            LadderHight();
        }


        //EventTrigger();
       
      

        //Restrict the player's maximum height for climbing
        //Vector3 newPosition = player.transform.position;
        //newPosition.y = Mathf.Clamp(newPosition.y, -2, maxY);
        //player.transform.position = newPosition;


    }

    void FixedUpdate()
    {
        //compare character's height to the ladder's height 


        //ladder movement at axis.x
        //ladderRb.MovePosition(ladderRb.position + movement * moveLadderSpeed * Time.fixedDeltaTime);

        if (Timer.instance.gameStart)
        {
            LadderHeightSwitch();
            MoveHorizontally();

            RandomTilt();
            SetCurrentState();
        }

        //WindStart();

    }

    #region LadderHeightControl&Movement

    void LadderHight()
    {
        // five stages of ladder heitgh adjustment
        //if (Keyboard.current[Key.Z].wasPressedThisFrame)   
        //{
        //    numOfLadder = 5;
        //}
        //else if (Keyboard.current[Key.X].wasPressedThisFrame)
        //{
        //    numOfLadder = 4;
        //}
        //else if (Keyboard.current[Key.C].wasPressedThisFrame)
        //{
        //    numOfLadder = 3;
        //}
        //else if (Keyboard.current[Key.V].wasPressedThisFrame)
        //{
        //    numOfLadder = 2;
        //}
        //else if (Keyboard.current[Key.Space].wasPressedThisFrame)
        //{
        //    numOfLadder = 1;
        //}
        if(Input.GetKey(KeyCode.Z))
        {
            numOfLadder = 5;
        }
        else if (Input.GetKey(KeyCode.X))
        {
            numOfLadder = 4;
        }
        else if (Input.GetKey(KeyCode.C))
        {
            numOfLadder = 3;
        }
        else if (Input.GetKey(KeyCode.V))
        {
            numOfLadder = 2;
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            numOfLadder = 1;
        }
        //this.transform.localScale = new Vector3(transform.localScale.x, heightChanges, transform.localScale.z);
        //ladderHeight = heightChanges;
    }



    void LadderHeightSwitch()
    {
        if (numOfLadder == 1)
        {
            ladderObj[0].gameObject.SetActive(false);
            ladderObj[1].gameObject.SetActive(false);
            ladderObj[2].gameObject.SetActive(false);
            ladderObj[3].gameObject.SetActive(false);

            //maxY = 3;
        }
        else if (numOfLadder == 2)
        {
            ladderObj[0].gameObject.SetActive(true);
            ladderObj[1].gameObject.SetActive(false);
            ladderObj[2].gameObject.SetActive(false);
            ladderObj[3].gameObject.SetActive(false);

            //maxY = 6f;
        }
        else if (numOfLadder == 3)
        {
            ladderObj[1].gameObject.SetActive(true);
            ladderObj[2].gameObject.SetActive(false);
            ladderObj[3].gameObject.SetActive(false);

            //maxY = 9F;
        }
        else if (numOfLadder == 4)
        {
            ladderObj[2].gameObject.SetActive(true);
            ladderObj[3].gameObject.SetActive(false);

            //maxY = 12f;

        }
        else if (numOfLadder == 5)
        {
            ladderObj[3].gameObject.SetActive(true);

            //maxY = 15F;
        }

    }


    void SpeedAdjust()
    {
        // three levels of speed: slow, normal, fast
        //if (Keyboard.current[Key.Digit1].wasPressedThisFrame)
        if(Input.GetKey(KeyCode.Alpha1))
        {
            moveLeft = true;
            moveLadderSpeed = 3f;

            rotateFast = true;
            rotateNormal = false;


        }
        //if (Keyboard.current[Key.Digit2].wasPressedThisFrame)
        if (Input.GetKey(KeyCode.Alpha2))
        {
            moveLeft = true;
            moveLadderSpeed = 1f;

            rotateFast = false;
            rotateNormal = true;


        }
        //if (Keyboard.current[Key.Digit3].wasPressedThisFrame)
        if (Input.GetKey(KeyCode.Alpha3))
        {
            moveLeft = false;
            moveRight = false;
            moveLadderSpeed = 0f;
            value = Random.Range(1, 10);

            rotateFast = false;
            rotateNormal = false;

            //StopCoroutine(MoveToHorizontal(MoveToNewPos(0)));


        }
        //if (Keyboard.current[Key.Digit4].wasPressedThisFrame)
        if (Input.GetKey(KeyCode.Alpha4))
        {
            moveRight = true;
            moveLadderSpeed = 1f;

            rotateFast = false;
            rotateNormal = true;


        }
        //if (Keyboard.current[Key.Digit5].wasPressedThisFrame)
        if (Input.GetKey(KeyCode.Alpha5))
        {
            moveRight = true;
            moveLadderSpeed = 3f;

            rotateFast = true;
            rotateNormal = false;



        }


    }

    private void MoveHorizontally()
    {
        if (moveLeft)
        {
            pivotPoint.transform.Translate(Vector3.left * moveLadderSpeed * Time.deltaTime);

        }
        if (moveRight)
        {
            pivotPoint.transform.Translate(Vector3.right * moveLadderSpeed * Time.deltaTime);
        }

    }

    void LadderRotate()
    {

        //if (Keyboard.current[Key.E].wasPressedThisFrame)
        if (Input.GetKey(KeyCode.E))
        {
            rotateLeft = true;
            rotateRight = false;
            leftRotation.color = pressColor;
            rightRotation.color = releaseColor;
        }

        //if (Keyboard.current[Key.Q].wasPressedThisFrame)
        if (Input.GetKey(KeyCode.Q))
        {
            rotateRight = true;
            rotateLeft = false;
            rightRotation.color = pressColor;
            leftRotation.color = releaseColor;
        }
    }


    #endregion

    #region Tilt Logics

    void LadderTilt(float rotationValue)
    {
        //rotation.z = rotationValue;
        this.transform.Rotate(Vector3.forward, rotationValue * Time.fixedDeltaTime);
    }

    void RandomTilt()
    {

        if (value > 0 && value <= 5 && !moveLeft && !moveRight && !rotateLeft && !rotateRight && !rotateFast && !rotateNormal)
        {
            if (numOfLadder == 1)
            {
                currentSpeed = 3;
                LadderTilt(currentSpeed);
            }
            else if (numOfLadder == 2)
            {
                currentSpeed = 4;
                LadderTilt(currentSpeed);
            }
            else if (numOfLadder == 3)
            {
                currentSpeed = 5;
                LadderTilt(currentSpeed);
            }
            else if (numOfLadder == 4)
            {
                currentSpeed = 7;
                LadderTilt(currentSpeed);
            }
            else if (numOfLadder == 5)
            {
                currentSpeed = 10;
                LadderTilt(currentSpeed);
            }
        }
        else if (value <= 10 && value > 5 && !moveLeft && !moveRight && !rotateLeft && !rotateRight && !rotateFast && !rotateNormal)
        {
            if (numOfLadder == 1)
            {
                currentSpeed = 3;
                LadderTilt(-currentSpeed);
            }
            else if (numOfLadder == 2)
            {
                currentSpeed = 4;
                LadderTilt(-currentSpeed);
            }
            else if (numOfLadder == 3)
            {
                currentSpeed = 5;
                LadderTilt(-currentSpeed);
            }
            else if (numOfLadder == 4)
            {
                currentSpeed = 7;
                LadderTilt(-currentSpeed);
            }
            else if (numOfLadder == 5)
            {
                currentSpeed = 10;
                LadderTilt(-currentSpeed);
            }
        }

        else if (rotateLeft && !rotateFast && !rotateNormal)
        {
            if (numOfLadder == 1)
            {
                currentSpeed = 3;
                LadderTilt(currentSpeed);
            }
            else if (numOfLadder == 2)
            {
                currentSpeed = 4;
                LadderTilt(currentSpeed);
            }
            else if (numOfLadder == 3)
            {
                currentSpeed = 5;
                LadderTilt(currentSpeed);
            }
            else if (numOfLadder == 4)
            {
                currentSpeed = 7;
                LadderTilt(currentSpeed);
            }
            else if (numOfLadder == 5)
            {
                currentSpeed = 10;
                LadderTilt(currentSpeed);
            }
        }
        else if (rotateRight && !rotateFast && !rotateNormal)
        {
            if (numOfLadder == 1)
            {
                currentSpeed = 3;
                LadderTilt(-currentSpeed);
            }
            else if (numOfLadder == 2)
            {
                currentSpeed = 4;
                LadderTilt(-currentSpeed);
            }
            else if (numOfLadder == 3)
            {
                currentSpeed = 5;
                LadderTilt(-currentSpeed);
            }
            else if (numOfLadder == 4)
            {
                currentSpeed = 7;
                LadderTilt(-currentSpeed);
            }
            else if (numOfLadder == 5)
            {
                currentSpeed = 10;
                LadderTilt(-currentSpeed);
            }
        }

        else if (rotateLeft && rotateFast)
        {
            if (numOfLadder == 1)
            {
                currentSpeed = 5;
                LadderTilt(currentSpeed);
            }
            else if (numOfLadder == 2)
            {
                currentSpeed = 7;
                LadderTilt(currentSpeed);
            }
            else if (numOfLadder == 3)
            {
                currentSpeed = 10;
                LadderTilt(currentSpeed);
            }
            else if (numOfLadder == 4)
            {
                currentSpeed = 12;
                LadderTilt(currentSpeed);
            }
            else if (numOfLadder == 5)
            {
                currentSpeed = 15;
                LadderTilt(currentSpeed);
            }
        }
        else if (rotateLeft && rotateNormal)
        {
            if (numOfLadder == 1)
            {
                currentSpeed = 3;
                LadderTilt(currentSpeed);
            }
            else if (numOfLadder == 2)
            {
                currentSpeed = 5;
                LadderTilt(currentSpeed);
            }
            else if (numOfLadder == 3)
            {
                currentSpeed = 7;
                LadderTilt(currentSpeed);
            }
            else if (numOfLadder == 4)
            {
                currentSpeed = 9;
                LadderTilt(currentSpeed);
            }
            else if (numOfLadder == 5)
            {
                currentSpeed = 10;
                LadderTilt(currentSpeed);
            }
        }
        else if (rotateRight && rotateFast)
        {
            if (numOfLadder == 1)
            {
                currentSpeed = 5;
                LadderTilt(-currentSpeed);
            }
            else if (numOfLadder == 2)
            {
                currentSpeed = 7;
                LadderTilt(-currentSpeed);
            }
            else if (numOfLadder == 3)
            {
                currentSpeed = 10;
                LadderTilt(-currentSpeed);
            }
            else if (numOfLadder == 4)
            {
                currentSpeed = 12;
                LadderTilt(-currentSpeed);
            }
            else if (numOfLadder == 5)
            {
                currentSpeed = 15;
                LadderTilt(-currentSpeed);
            }
        }
        else if (rotateRight && rotateNormal)
        {
            if (numOfLadder == 1)
            {
                currentSpeed = 3;
                LadderTilt(-currentSpeed);
            }
            else if (numOfLadder == 2)
            {
                currentSpeed = 5;
                LadderTilt(-currentSpeed);
            }
            else if (numOfLadder == 3)
            {
                currentSpeed = 7;
                LadderTilt(-currentSpeed);
            }
            else if (numOfLadder == 4)
            {
                currentSpeed = 9;
                LadderTilt(-currentSpeed);
            }
            else if (numOfLadder == 5)
            {
                currentSpeed = 10;
                LadderTilt(-currentSpeed);
            }
        }
    }

    #endregion


    #region Ladder UI Status

    public enum LadderStates
    {
        Fast_Right,
        Normal_Right,
        Neutral,
        Normal_Left,
        Fast_Left
    }

    public LadderStates currentState;

    void SetCurrentState()
    {
        if (moveLeft && rotateFast)
        {
            currentState = LadderStates.Fast_Left;

            normalLeft.SetActive(false);
            normaRight.SetActive(false);
            fastRight.SetActive(false);
            neutral.SetActive(false);
            fastLeft.SetActive(true);
        }

        if (moveLeft && rotateNormal)
        {
            currentState = LadderStates.Normal_Left;

            normalLeft.SetActive(true);
            normaRight.SetActive(false);
            fastRight.SetActive(false);
            neutral.SetActive(false);
            fastLeft.SetActive(false);
        }
        if (moveRight && rotateFast)
        {
            currentState = LadderStates.Fast_Right;

            normalLeft.SetActive(false);
            normaRight.SetActive(false);
            fastRight.SetActive(true);
            neutral.SetActive(false);
            fastLeft.SetActive(false);
        }

        if (moveRight && rotateNormal)
        {
            currentState = LadderStates.Normal_Right;

            normalLeft.SetActive(false);
            normaRight.SetActive(true);
            fastRight.SetActive(false);
            neutral.SetActive(false);
            fastLeft.SetActive(false);
        }
        if (!moveRight && !moveLeft)
        {
            currentState = LadderStates.Neutral;

            normalLeft.SetActive(false);
            normaRight.SetActive(false);
            fastRight.SetActive(false);
            neutral.SetActive(true);
            fastLeft.SetActive(false);
        }

    }
    #endregion

    #region Wind Event
    private void EventTrigger()
    {
        if (!isTriggered)
        {
            StartCoroutine(RandomNumber(5, 100));
        }



        if (isTriggered)
        {
            if(windValue ==1)
            {
                isRight = true;
                isLeft = false;
            } else if(windValue == 2)
            {
                isLeft = true;
                isRight = false;
            }
        }
    }

    IEnumerator RandomNumber(float min, float max)
    {
        yield return new WaitForSeconds(Random.Range(min, max));
        randValue = Random.Range(1, 100);
        if (randValue < 3)
        {
            isTriggered = true;
            windValue = randValue;
        }

    }


    void WindStart()
    {
        currentSpeed *= multipleValue;
        if (isLeft)
        {
            startTime = Time.time;
            if(Time.time - startTime < 5)
            {
                LadderTilt(-currentSpeed);
                print("Wind!!!");
            }
            else
            {
                isTriggered = false;
            }

        }

        if (isRight)
        {
            startTime = Time.time;
            if (Time.time - startTime < 5)
            {
                LadderTilt(currentSpeed);
                print("Wind!!!");
            }
            else
            {
                isTriggered = false;
            }

        }
    }
}

    #endregion


