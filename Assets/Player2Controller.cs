using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Controller : MonoBehaviour
{
    //ladder variables
    [SerializeField] float ladderHeight;
    [SerializeField] float heightChanges;
    //Set ladderHeight
    //height rate changes

    PlayerOneController player1;
    GameObject player;


    //ladder movement variables
    [SerializeField] float moveLadderSpeed = 5.0f;
    [SerializeField] float moveLadderDist;
    Rigidbody rb;

    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        ladderHeight = this.transform.localScale.y;
        rb = this.GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        player1 = player.GetComponent<PlayerOneController>();
    }

    // Update is called once per frame
    void Update()
    {

        LadderFunction();
        SpeedAdjust();

        movement.x = Input.GetAxisRaw("HorizontalInput");

        /*
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("moveLadderSpeed =" + moveLadderSpeed);
            Vector3 movement = new Vector3(this.transform.position.x * -moveLadderSpeed, this.transform.position.y, this.transform.position.z);
            this.transform.position = movement;
            Debug.Log("Left");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("moveLadderSpeed =" + moveLadderSpeed);
            Vector3 movement = new Vector3(this.transform.position.x * moveLadderSpeed, this.transform.position.y, this.transform.position.z);
            this.transform.position = movement;
            Debug.Log("Right");
        }
        */

    }

    private void FixedUpdate()
    {
        //compare character's height to the ladder's height 
        if (player.transform.position.y <= ladderHeight - 2.0f)
        {
            player1.moveDist = 0.5f;
        }
        else
        {
            player1.moveDist = 0f;
        }

        rb.MovePosition(rb.position + movement * moveLadderSpeed * Time.fixedDeltaTime);
    }

    #region Ladder
    void LadderFunction()
    {
        // five stages of ladder heitgh adjustment
        if (Keyboard.current[Key.Z].wasPressedThisFrame)
        {
            heightChanges = 5;
        }
        if (Keyboard.current[Key.X].wasPressedThisFrame)
        {
            heightChanges = 4;
        }
        if (Keyboard.current[Key.C].wasPressedThisFrame)
        {
            heightChanges = 3;
        }
        if (Keyboard.current[Key.V].wasPressedThisFrame)
        {
            heightChanges = 2;
        }
        if (Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            heightChanges = 1;
        }
        this.transform.localScale = new Vector3(transform.localScale.x, heightChanges, transform.localScale.z);
        ladderHeight = heightChanges;
    }

    void SpeedAdjust()
    {
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
}
