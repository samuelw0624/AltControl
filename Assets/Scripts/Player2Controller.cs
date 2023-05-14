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
    [SerializeField] float moveLadderSpeed = 5.0f;
    [SerializeField] float moveLadderDist;
    Rigidbody ladderRb;

    Vector3 movement;
    Vector3 rotation;
    float rotationValue;
    bool isTiltingLeft;
    bool isTiltingRight;

    [SerializeField] float initialRotationSpeed = 10f;
    [SerializeField] float acceleratingRate = 0.1f;
    [SerializeField] float maxRotationSpeed = 50;
    [SerializeField] float currentRotationSpeed;


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
        currentRotationSpeed = initialRotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        LadderFunction();
        SpeedAdjust();

        movement.x = Input.GetAxisRaw("HorizontalInput");
        rotation.z = Input.GetAxisRaw("Rotation");

        rotationValue = this.transform.localRotation.z;
        Debug.Log("Rotation Value is " + this.transform.localRotation.z);

        Tilt();
        Rotate();
        AdjustRotation();

        currentRotationSpeed = Mathf.Min(currentRotationSpeed, maxRotationSpeed);
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
        ladderRb.MovePosition(ladderRb.position + movement * moveLadderSpeed * Time.fixedDeltaTime);
        
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, 0f, rotation.z * moveLadderSpeed) * Time.fixedDeltaTime);
        ladderRb.MoveRotation(ladderRb.rotation * deltaRotation);


    }

    #region Ladder
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
            moveLadderSpeed = 3f;
            Debug.Log("1");
        }
        if (Keyboard.current[Key.Digit2].wasPressedThisFrame)
        {
            moveLadderSpeed = 5f;
            Debug.Log("2");
        }
        if (Keyboard.current[Key.Digit3].wasPressedThisFrame)
        {
            moveLadderSpeed = 7f;
            Debug.Log("3");
        }
    }

    #endregion

    #region Tilt Logics
    void Tilt()
    {
        if(rotationValue > -90)
        {
            rotationValue = rotationValue + 90;
            isTiltingRight = true;
            isTiltingLeft = false;

        }

        if(rotationValue > 90)
        {
            rotationValue = rotationValue - 90;
            isTiltingLeft = true;
            isTiltingRight = false;
        }
    }

    void Rotate()
    {
        if (rotationValue <= 0)
        {
            transform.Rotate(Vector3.back, currentRotationSpeed * Time.deltaTime);
        }
        else if(rotationValue > 0)
        {
            transform.Rotate(-Vector3.back, currentRotationSpeed * Time.deltaTime);
        }
    }


    void AdjustRotation()
    {
        if (rotation.z != 0)
        {
            currentRotationSpeed = 0;
        }
        else
        {
            currentRotationSpeed = initialRotationSpeed;
            currentRotationSpeed += acceleratingRate * Time.deltaTime;
        }
    }
    #endregion
}
