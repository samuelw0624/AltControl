using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalconAttack : MonoBehaviour
{
    public static FalconAttack instance;

    [Header("Movement")]
    [SerializeField]
    private Transform playerPos;
    [SerializeField]
    private Transform oriPos;
    [SerializeField]
    private Transform homePos;
    [SerializeField]
    private Transform headPosition;
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
    private Transform oriParent;
    [SerializeField]
    public bool isStunning;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private bool attackEnds;
    [SerializeField]
    private GameObject rotateText;
    [SerializeField]
    private float seekingTargetTimer;
    [SerializeField]
    private bool wasHome;
    [SerializeField]
    private bool goingHome;
    [SerializeField]
    private float homeTimer;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer.instance.gameStart && !PlayerOneController.instance.gameEnd)
        {
            if (!PlayerOneController.instance.isFreezed)
            {
                InitiateAttack();
                //if (!isStunning)
                //{
                //    InitiateAttack();
                //    DetectStun();
                //}

                if (rotateText != null)
                {
                    WarningText();
                }

            }

        }

    }

    private void FixedUpdate()
    {
        if (Timer.instance.gameStart && !PlayerOneController.instance.gameEnd)
        {
            if (!PlayerOneController.instance.isFreezed)
            {
                if (isAttacking)
                {
                    Attack();
                }

            }

        }


    }

    void WarningText()
    {
        if (isStunning)
        {
            rotateText.SetActive(true);
        }
        else
        {
            rotateText.SetActive(false);
        }
    }

    void InitiateAttack()
    {

        if (FalconArea.instance.withinAttackRange && !readyToAttack)
        {
            transform.position = Vector2.MoveTowards(transform.position, oriPos.position, normalSpeed * Time.deltaTime);
            
            timer = 0;
            seekingTargetTimer = 0;

            readyToAttack = true;
       
            anim.SetBool("Attack", false);

            print("1");

        }

        if (goingHome)
        {
            GoBackHome();
            print("GoingHome");
        }


        if (readyToAttack && !goingHome)
        {
            timer += Time.deltaTime;
            
            //if(homePos.position == oriPos.position)
            //{
            //    wasHome = true;
            //}
            //else
            //{
            //    wasHome = false;
            //}
            print("2");
        }

        if (timer >= attackTimer)
        {
            isAttacking = true;
            direction = playerPos.position - transform.position;
            print("ReadytoAttack");
        }

    }

    void Attack()
    {      
        if (seekingTargetTimer >= 5)
        {
            //attackEnds = true;
            goingHome = true;
            homeTimer = 0;
            readyToAttack = false;
            isAttacking = false;
            print("BackHome");
        }
        else
        {
            falconAudio.Play();
            timer = 0;
            if (StunFunction.instance.isStuned)
            {
                anim.SetBool("Attack", true);
                transform.position = Vector2.MoveTowards(transform.position, headPosition.position, attackSpeed * Time.deltaTime);
                isStunning = true;
            }
            else
            {
                falconAudio.Play();
                transform.Translate(direction.normalized * attackSpeed * Time.deltaTime);
                
                seekingTargetTimer += Time.deltaTime;
                print("SeekingTarget");
            }

        }
    }

    void GoBackHome()
    {
        if(homeTimer >= 5)
        {
            goingHome = false;
        }
        else
        {
            homeTimer += Time.deltaTime;
        }

        transform.position = Vector2.MoveTowards(transform.position, oriPos.position, normalSpeed * Time.deltaTime);
    }

    public void StopStun()
    {
        //this.transform.parent = oriParent;
        isStunning = false;
        goingHome = true;
        homeTimer = 0;
        readyToAttack = false;
        isAttacking = false;
        print("StopStun");

    }






}
