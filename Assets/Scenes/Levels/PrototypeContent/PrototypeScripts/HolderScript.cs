using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderScript : MonoBehaviour
{
    public float horizontalSpeed = 5f;
    public float verticalDistance = 5f;

    void Update()
    {
        // Move the cube to the left when the A key is pressed
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime);
        }

        // Move the cube to the right when the D key is pressed
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime);
        }

        // Move the cube up by verticalDistance units when the W key is pressed
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.Translate(Vector3.up * verticalDistance);
        }

        // Move the cube down by verticalDistance units when the S key is pressed
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Translate(Vector3.down * verticalDistance);
        }
    }
}
