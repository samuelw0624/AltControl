using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{

    public Transform player;
    Vector3 newPosition;

    private void LateUpdate()
    {
        //lock z-axis
        newPosition = player.position;
        
        newPosition.z = transform.position.z;

        transform.position = newPosition;
        
        transform.rotation = Quaternion.Euler(0,0,0);
    }



}
