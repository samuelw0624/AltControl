using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public int num;
    
    


    // Start is called before the first frame update

    void Awake()
    {
        player1 = PlayerOneController.instance;

    }
    void Start()
    {
        ladderHeight = this.transform.localScale.y;
        ladderRb = this.GetComponent<Rigidbody>();

        //player1 = player.GetComponent<PlayerOneController>();

        //set default Rotation Speed 

        moveLadderSpeed = 0;
        moveLadderDist = 1f;
        maxY = 3;

        value = Random.Range(1, 10);
    }

    // Update is called once per frame
    void Update()
    {

        SpeedAdjust();
        LadderRotate();
        LadderHight();

        //Restrict the player's maximum height for climbing
        Vector3 newPosition = player.transform.position;
        newPosition.y = Mathf.Clamp(newPosition.y, -2, maxY);
        player.transform.position = newPosition;


    }

    private void FixedUpdate()
    {
        //compare character's height to the ladder's height 


        //ladder movement at axis.x
        //ladderRb.MovePosition(ladderRb.position + movement * moveLadderSpeed * Time.fixedDeltaTime);

        LadderHeightSwitch();
        MoveHorizontally();

        RandomTilt();
        DetectPlayerPosition();



    }

    #region LadderHeightControl&Movement
    
    void LadderHight()
    {
        // five stages of ladder heitgh adjustment
        if (Keyboard.current[Key.Z].wasPressedThisFrame)
        {
            numOfLadder = 5;
        }
        if (Keyboard.current[Key.X].wasPressedThisFrame)
        {
            numOfLadder = 4;
        }
        if (Keyboard.current[Key.C].wasPressedThisFrame)
        {
            numOfLadder = 3;
        }
        if (Keyboard.current[Key.V].wasPressedThisFrame)
        {
            numOfLadder = 2;
        }
        if (Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            numOfLadder = 1;
        }
        //this.transform.localScale = new Vector3(transform.localScale.x, heightChanges, transform.localScale.z);
        ladderHeight = heightChanges;
    }

    void DetectPlayerPosition()
    {
        if (player1.transform.position.y <= 2.84)
        {
            num = 0;
        }
        else if(player1.transform.position.y > 2.84 && player1.transform.position.y <= 6)
        {
            num = 1;
        }
        else if (player1.transform.position.y > 6 && player1.transform.position.y <= 9)
        {
            num = 2;
        }
        else if (player1.transform.position.y > 9 && player1.transform.position.y <= 12)
        {
            num = 3;
        }
        else if (player1.transform.position.y > 12 && player1.transform.position.y <= 15)
        {
            num = 4;
        }
    }

    void LadderHeightSwitch()
    {
        if (numOfLadder == 1)
        {
            ladderObj[0].gameObject.SetActive(false);
            ladderObj[1].gameObject.SetActive(false);
            ladderObj[2].gameObject.SetActive(false);
            ladderObj[3].gameObject.SetActive(false);

            maxY = 3;
        } 
        else if (numOfLadder == 2)
        {
            ladderObj[0].gameObject.SetActive(true);
            ladderObj[1].gameObject.SetActive(false);
            ladderObj[2].gameObject.SetActive(false);
            ladderObj[3].gameObject.SetActive(false);

            maxY = 6f;
        } 
        else if (numOfLadder == 3)
        {
            ladderObj[1].gameObject.SetActive(true);
            ladderObj[2].gameObject.SetActive(false);
            ladderObj[3].gameObject.SetActive(false);

            maxY = 9F;
        }
        else if (numOfLadder == 4)
        {
            ladderObj[2].gameObject.SetActive(true);
            ladderObj[3].gameObject.SetActive(false);

            maxY = 12f;

        }
        else if (numOfLadder == 5)
        {
            ladderObj[3].gameObject.SetActive(true);

            maxY = 15F;
        }

    }
    

    void SpeedAdjust()
    {
        // three levels of speed: slow, normal, fast
        if (Keyboard.current[Key.Digit1].wasPressedThisFrame)
        {
            moveLeft = true;
            moveLadderSpeed = 3f;
            
            rotateFast = true;
            rotateNormal = false;


        }
        if (Keyboard.current[Key.Digit2].wasPressedThisFrame)
        {
            moveLeft = true;
            moveLadderSpeed = 1f;

            rotateFast = false;
            rotateNormal = true;


        }
        if (Keyboard.current[Key.Digit3].wasPressedThisFrame)
        {
            moveLeft = false;
            moveRight = false;
            moveLadderSpeed = 0f;
            value = Random.Range(1, 10);

            rotateFast = false;
            rotateNormal = false;

            //StopCoroutine(MoveToHorizontal(MoveToNewPos(0)));


        }
        if (Keyboard.current[Key.Digit4].wasPressedThisFrame)
        {
            moveRight = true;
            moveLadderSpeed = 1f;

            rotateFast = false;
            rotateNormal = true;


        }
        if (Keyboard.current[Key.Digit5].wasPressedThisFrame)
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
        if (Keyboard.current[Key.E].wasPressedThisFrame)
        {
            rotateLeft = true;
            rotateRight = false;
        }


        if (Keyboard.current[Key.Q].wasPressedThisFrame)
        {
            rotateRight = true;
            rotateLeft = false;
        }
    }


    #endregion

    #region Tilt Logics

    void LadderTilt(float rotationValue)
    {
        rotation.z = rotationValue;
        this.transform.Rotate(rotation * Time.fixedDeltaTime);
    }

    void RandomTilt()
    {

        if(value > 0 && value <= 5 && !moveLeft && !moveRight && !rotateLeft && !rotateRight && !rotateFast && !rotateNormal)
        {
            LadderTilt(5);
        } 
        else if (value <= 10 && value > 5 && !moveLeft && !moveRight && !rotateLeft && !rotateRight && !rotateFast && !rotateNormal)
        {
            LadderTilt(-5);
        } 
        
        else if (rotateLeft && !rotateFast && !rotateNormal)
        {
            LadderTilt(5);
        } 
        else if (rotateRight && !rotateFast && !rotateNormal) 
        {
            LadderTilt(-5);
        } 
        
        else if (rotateLeft && rotateFast)
        {
            LadderTilt(10);
        } 
        else if (rotateLeft && rotateNormal)
        {
            LadderTilt(10);
        } 
        else if (rotateRight && rotateFast)
        {
            LadderTilt(-10);
        }
        else if (rotateRight && rotateNormal)
        {
            LadderTilt(-10);
        }
    }

    #endregion
}
