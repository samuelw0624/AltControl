using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField]
    public float cameraMoveSpeed;
    private Func<Vector3> GetCameraFollowPositionFunc;
    public void Setup(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }
    void Update()
    {
        if(GameManager.instance.currentScene.name == "Level_02" || GameManager.instance.currentScene.name == "Level_03" || GameManager.instance.currentScene.name == "Level_04")
        {
            SpeedControl();
        }

        //camera follow with player in certain speed
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;

        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);

        transform.position = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;
    }


    void SpeedControl()
    {
        if (EnterShop.instance.isPurchased1)
        {
            cameraMoveSpeed = 5;
        }
        else
        {
            cameraMoveSpeed = 4;
        }
    }


}
