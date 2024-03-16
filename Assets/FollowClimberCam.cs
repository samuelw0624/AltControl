using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowClimberCam : MonoBehaviour
{
    [SerializeField]
    private Transform climber;
    [SerializeField]
    private float offsetX;
    [SerializeField]
    private float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(climber.position.x - offsetX, climber.position.y - offsetY, this.transform.position.z);
    }
}
