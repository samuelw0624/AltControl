using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollowPlayer : MonoBehaviour
{
    private Func<Vector3> GetCameraFollowPositionFunc;
    public void Setup(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }
    void Update()
    {
        //camera follow with player in certain speed
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;

        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);
        float cameraMoveSpeed = 2f;

        transform.position = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;
    }



}
