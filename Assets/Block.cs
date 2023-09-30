using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject block;
    int i;
    Vector3 blockPos;
    Vector3 myPos;

    Vector3 dir;

    bool isCollided;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDirection();

    }


    #region Detect Direction Between Player and blocks
    void CalculateDirection()
    {
        blockPos = block.transform.position;
        myPos = this.transform.position;

        dir = blockPos.normalized - myPos.normalized;
        //float distance = Vector3.Distance(blockPos, myPos);

        Debug.Log(dir);
    }

    void DetectDirection()
    {
        
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "block")
        {
            isCollided = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "block")
        {
            isCollided = false;
        }
    }


}
