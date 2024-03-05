using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer2 : MonoBehaviour
{
    [SerializeField]
    private float followSpeed = 2f;
    [SerializeField]
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, this.transform.position.z);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }


}
