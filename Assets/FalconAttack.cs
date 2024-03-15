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


    private void Awake()
    {
        transform.position = oriPos.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer.instance.gameStart && !PlayerOneController.instance.gameEnd)
        {
            if (!PlayerOneController.instance.isFreezed)
            {
                InitiateAttack();
                DetectStun();
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
                Attack();
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
            readyToAttack = true;
            isStunning = false;

            anim.SetBool("Attack", false);

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
        if(isAttacking)
        {

            if (isStunning)
            {
                Stun();
                StopCoroutine(BackHome());
            }
            else
            {
                falconAudio.Play();

                transform.Translate(direction.normalized * attackSpeed * Time.deltaTime);

                timer = 0;

                StartCoroutine(BackHome());

            }
        }


        if (attackEnds)
        {
            StartCoroutine(StopAttack());
        } 


    }

    IEnumerator BackHome()
    {
        yield return new WaitForSeconds(waitingTime);
        isAttacking = false;
        attackEnds = true;
        this.transform.parent = oriParent;
        transform.position = Vector2.MoveTowards(transform.position, oriPos.position, normalSpeed * Time.deltaTime);
    }

    IEnumerator StopAttack()
    {

        readyToAttack = false;


        this.transform.parent = oriParent;
        transform.position = Vector2.MoveTowards(transform.position, oriPos.position, normalSpeed * Time.deltaTime);

        yield return new WaitForSeconds(3f);

        attackEnds = false;
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
        anim.SetBool("Attack", true);
        this.transform.parent = playerPos;
        
    }

    public void StopStun()
    {
        this.transform.parent = oriParent;
        isStunning = false;
        isAttacking = false;
        attackEnds = true;
        
    }






}
