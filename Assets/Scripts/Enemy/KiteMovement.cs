using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteMovement : MonoBehaviour
{
    float accelerationTime = 2f;
    [SerializeField]
    float maxSpeed = 1f;
    Vector2 movement;

    float timeLeft;
    Rigidbody2D rb;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float angle;
    [SerializeField]
    private float movingForwardSpeed;

    [SerializeField]
    private float x;
    [SerializeField]
    private float y;
    [SerializeField]
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerOneController>().transform;

    }

    // Update is called once per frame
    void Update()
    {
        //Random speed with accelerationRate
        //timeLeft -= Time.deltaTime;


        //movement = new Vector2(x, y);
        //if (timeLeft <= 0)
        //{
        //    //movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        //    timeLeft += accelerationTime;
        //}



    }

    void FixedUpdate()
    {


        //x = this.transform.position.x + radius * Mathf.Cos(angle);
        //y = this.transform.position.y + radius * Mathf.Sin(angle);

        //Apply movement to the RB
        //transform.Translate(Vector2.left * movingForwardSpeed * Time.deltaTime, Space.Self);
        //rb.AddForce(movement * maxSpeed);
        angle += speed * Time.deltaTime;
        Vector3 direction = player.position - this.transform.position;
        direction = Quaternion.Euler(0, 0, 60) * direction;
        float distanceThisFrame = speed * Time.deltaTime;
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);

    }

    private void LateUpdate()
    {


    }




}
