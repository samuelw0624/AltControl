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

    public GameObject ladder;
    Vector3 rotation;
    float value;
    bool rotateLeft;
    bool rotateRight;
    bool rotateFast;
    bool rotateNormal;

    //bool gameStart;

    

    //[SerializeField] float initialRotationSpeed = 10f;
    //[SerializeField] float acceleratingRate = 0.1f;
    //[SerializeField] float maxRotationSpeed = 50;
    //[SerializeField] float currentRotationSpeed;


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

        value = Random.Range(1, 10);
    }

    // Update is called once per frame
    void Update()
    {

        LadderFunction();
        SpeedAdjust();
        LadderRotate();
        RandomTilt();



    }

    private void FixedUpdate()
    {
        //compare character's height to the ladder's height 
        if (player1.transform.position.y <= ladderHeight - 2.0f)
        {
            player1.moveDist = 0.5f;
        }
        else
        {
            player1.moveDist = 0f;
        }

        //ladder movement at axis.x
        //ladderRb.MovePosition(ladderRb.position + movement * moveLadderSpeed * Time.fixedDeltaTime);


        MoveHorizontally();

    }

    #region LadderHeightControl&Movement
    
    void LadderFunction()
    {
        // five stages of ladder heitgh adjustment
        if (Keyboard.current[Key.Z].wasPressedThisFrame)
        {
            heightChanges = 15;
        }
        if (Keyboard.current[Key.X].wasPressedThisFrame)
        {
            heightChanges = 13;
            ScoreManager.instance.AddPoint(1);
        }
        if (Keyboard.current[Key.C].wasPressedThisFrame)
        {
            heightChanges = 10;
        }
        if (Keyboard.current[Key.V].wasPressedThisFrame)
        {
            heightChanges = 7;
        }
        if (Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            heightChanges = 5;
        }
        this.transform.localScale = new Vector3(transform.localScale.x, heightChanges, transform.localScale.z);
        ladderHeight = heightChanges;
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



            Debug.Log("1");

        }
        if (Keyboard.current[Key.Digit2].wasPressedThisFrame)
        {
            moveLeft = true;
            moveLadderSpeed = 1f;

            rotateFast = false;
            rotateNormal = true;

            Debug.Log("2");

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


            Debug.Log("3");

        }
        if (Keyboard.current[Key.Digit4].wasPressedThisFrame)
        {
            moveRight = true;
            moveLadderSpeed = 1f;

            rotateFast = false;
            rotateNormal = true;


            Debug.Log("4");

        }
        if (Keyboard.current[Key.Digit5].wasPressedThisFrame)
        {
            moveRight = true;
            moveLadderSpeed = 3f;

            rotateFast = true;
            rotateNormal = false;


            Debug.Log("5");

        }
    }

    private void MoveHorizontally()
    {
        if (moveLeft)
        {
            transform.Translate(Vector3.left * moveLadderSpeed * Time.deltaTime);

        }
        if (moveRight)
        {
            transform.Translate(Vector3.right * moveLadderSpeed * Time.deltaTime);
        }

    }

    void LadderRotate()
    {
        if (Keyboard.current[Key.Q].wasPressedThisFrame)
        {
            rotateLeft = true;
            rotateRight = false;
        }


        if (Keyboard.current[Key.E].wasPressedThisFrame)
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
        ladder.transform.Rotate(rotation * Time.fixedDeltaTime);
    }

    void RandomTilt()
    {

        if(value > 0 && value <= 5 && !moveLeft && !moveRight && !rotateLeft && !rotateRight)
        {
            LadderTilt(1);
        } else if (value <= 10 && value > 5 && !moveLeft && !moveRight && !rotateLeft && !rotateRight)
        {
            LadderTilt(-1);
        } else if (rotateLeft && !rotateFast && !rotateNormal)
        {
            LadderTilt(1);
        } else if (rotateRight && !rotateFast && !rotateNormal)
        {
            LadderTilt(-1);
        } else if (rotateLeft && rotateFast)
        {
            LadderTilt(3);
            //Debug.Log("rotateLeft + fast");
        } else if (rotateLeft && rotateNormal)
        {
            LadderTilt(2);
            //Debug.Log("rotateLeft + normal");
        } else if (rotateRight && rotateFast)
        {
            LadderTilt(-3);
            //Debug.Log("rotateRight + fast");
        }
        else if (rotateRight && rotateNormal)
        {
            LadderTilt(-2);
            //Debug.Log("rotateRight + normal");
        }
    }

    #endregion
}
