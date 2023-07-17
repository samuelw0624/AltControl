using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Controller : MonoBehaviour
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


            Debug.Log("1");

        }
        if (Keyboard.current[Key.Digit2].wasPressedThisFrame)
        {
            moveLeft = true;
            moveLadderSpeed = 1f;
       
            Debug.Log("2");

        }
        if (Keyboard.current[Key.Digit3].wasPressedThisFrame)
        {
            moveLeft = false;
            moveRight = false;
            moveLadderSpeed = 0f;
            value = Random.Range(1, 10);

            //StopCoroutine(MoveToHorizontal(MoveToNewPos(0)));


            Debug.Log("3");

        }
        if (Keyboard.current[Key.Digit4].wasPressedThisFrame)
        {
            moveRight = true;
            moveLadderSpeed = 1f;
       

            Debug.Log("4");

        }
        if (Keyboard.current[Key.Digit5].wasPressedThisFrame)
        {
            moveRight = true;
            moveLadderSpeed = 3f;
       

            Debug.Log("5");

        }
    }

    private void MoveHorizontally()
    {
        if (moveLeft)
        {
            transform.Translate(Vector3.left * moveLadderSpeed * Time.deltaTime);
            LadderTilt(-2);

        }
        if (moveRight)
        {
            transform.Translate(Vector3.right * moveLadderSpeed * Time.deltaTime);
            LadderTilt(2);
        }

    }

    void LadderRotate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ladder.transform.Rotate(0f, 0f, 6f);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ladder.transform.Rotate(0f, 0f, -6f);
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

        if(value > 0 && value <= 5 && !moveLeft && !moveRight)
        {
            LadderTilt(1);
        } else if (value <= 10 && value > 5 && !moveLeft && !moveRight)
        {
            LadderTilt(-1);
        }
    }

    #endregion
}
