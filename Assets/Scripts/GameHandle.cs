using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandle : MonoBehaviour
{
    public CameraFollowPlayer cameraFollow;
    public Transform playerTransform;

    private void Start()
    {
        cameraFollow.Setup(() => playerTransform.position);
    }

}
