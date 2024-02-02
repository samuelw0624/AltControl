using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteEffect : MonoBehaviour
{
    public static KiteEffect instance { get; private set; }

    [SerializeField]
    private AudioSource kiteBreakingSound;
    [SerializeField]
    public bool kiteAttack;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kite"))
        {
            //kiteBreakingSound.Play();
            //ScoreManager.instance.ReducePoint(3);
            //Destroy(collision.gameObject);
            Debug.Log("CollisionwithKite");

            kiteAttack = true;
        }
    }
}
