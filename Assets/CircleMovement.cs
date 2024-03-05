using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    [SerializeField]
    Transform rotationCenter;
    [SerializeField]
    float rotationRadius, angularSpeed;

    float posX, posY, angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Timer.instance.gameStart && !PlayerOneController.instance.gameEnd)
        {
            posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
            posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;

            transform.position = new Vector2(posX, posY);
            angle = angle + Time.deltaTime * angularSpeed;

            if (angle >= 360f)
            {
                angle = 0f;
            }
        }
        
    }
}
