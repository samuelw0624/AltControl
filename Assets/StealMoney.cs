using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealMoney : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.instance.ReducePoint(3);
            ScorePlayer2.instance.ReducePoint(3);
            Destroy(this.gameObject);
            Debug.Log("CollisionwithKite");
        }
    }
}
