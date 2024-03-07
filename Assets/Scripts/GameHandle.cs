using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandle : MonoBehaviour
{
    public CameraFollowPlayer cameraFollow;
    public Transform playerTransform;
    public Transform holderTransform;
    private bool camIsSet;

    private void Start()
    {
        cameraFollow.Setup(() => playerTransform.position);
    }

    private void Update()
    {
        //if (Timer.instance.inTutorial && !camIsSet)
        //{
        //    cameraFollow.Setup(() => holderTransform.position);
        //    camIsSet = true;
        //}
    }

}
