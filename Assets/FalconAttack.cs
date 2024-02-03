using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalconAttack : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField]
    private Transform playerPos;
    [SerializeField]
    private Transform oriPos;
    [SerializeField]
    private float normalSpeed;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float timer;
    [SerializeField]
    private float attackTimer;
    [SerializeField]
    private bool readyToAttack;
    [SerializeField]
    private AudioSource falconAudio;
    [SerializeField]
    private bool isAttacking;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private float waitingTime;

    [Header("Stun Function")]
    [SerializeField]
    private float waitingStunTime;
    [SerializeField]
    private Transform oriParent;
    [SerializeField]
    private bool isStunning;

    private void Awake()
    {
        transform.position = oriPos.position;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InitiateAttack();
        DetectStun();
    }

    private void FixedUpdate()
    {
        if (isAttacking)
        {
            Attack();
        }

    }

    void InitiateAttack()
    {
        if (FalconArea.instance.withinAttackRange && !readyToAttack)
        {
            transform.position = Vector2.MoveTowards(transform.position, oriPos.position, normalSpeed * Time.deltaTime);
            
            timer = 0;
            readyToAttack = true;
            isStunning = false;

        }



        if (readyToAttack)
        {
            timer += Time.deltaTime;
        }

        if (timer >= attackTimer)
        {
            isAttacking = true;
            direction = playerPos.position - transform.position;
        }
    }

    void Attack()
    {
        if (isStunning)
        {
            Stun();
        }
        else
        {
            falconAudio.Play();

            transform.Translate(direction.normalized * attackSpeed * Time.deltaTime);

            timer = 0;

            StartCoroutine(StopAttack());
        }
    }

    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(waitingTime);
        readyToAttack = false;
        isAttacking = false;

    }

    private void DetectStun()
    {
        if (StunFunction.instance.isStuned)
        {
            isStunning = true;

        }
    }

    void Stun()
    {
        this.transform.parent = playerPos;

        StartCoroutine(StopStun());
    }

    IEnumerator StopStun()
    {
        yield return new WaitForSeconds(waitingStunTime);
        this.transform.parent = oriParent;

    }
}
