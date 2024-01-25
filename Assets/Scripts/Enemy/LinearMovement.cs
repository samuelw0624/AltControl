using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LinearMovement : MonoBehaviour
{

    [Header("Waypoints")]
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private float movingSpeed;
    [SerializeField]
    private float waitTimeBetweenWaypoints;
    [SerializeField]
    private int targetPoint;
    [SerializeField]
    private bool timeToMove;
    [SerializeField]
    private float moveTimer;



    // Start is called before the first frame update
    void Start()
    {
        targetPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position == wayPoints[targetPoint].position)
        {
            moveTimer = 0;
            IncreaseTargetPoint();

        }

        moveTimer += Time.deltaTime;

        if (moveTimer >= waitTimeBetweenWaypoints)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[targetPoint].position, movingSpeed * Time.deltaTime);

        }
    }
    void IncreaseTargetPoint()
    {
        targetPoint++;

        if (targetPoint >= wayPoints.Length)
        {
            targetPoint = 0;
        }

    }









}
