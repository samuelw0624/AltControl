using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimberScript : MonoBehaviour
{
   public float speed = 5f;
  
      void Update()
      {
          // Move the cube up when the up arrow key is pressed
          if (Input.GetKey(KeyCode.UpArrow))
          {
              transform.Translate(Vector3.up * speed * Time.deltaTime);
          }
  
          // Move the cube down when the down arrow key is pressed
          if (Input.GetKey(KeyCode.DownArrow))
          {
              transform.Translate(Vector3.down * speed * Time.deltaTime);
          }
      }
}
