using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairSpots : MonoBehaviour
{

    Renderer rend;
    int colorValue;
    PlayerController playerController;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rend = this.GetComponent<Renderer>();
        colorValue = Random.Range(1, 3);

        player = GameObject.FindWithTag("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        if(colorValue == 1)
        {
            rend.material.color = Color.yellow;

        } else if(colorValue == 2)
        {
            rend.material.color = Color.green;
        } else if(colorValue == 3)
        {
            rend.material.color = Color.red;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && player.GetComponent<PlayerController>().colorNumber == colorValue)
        {
            Debug.Log("Trigered");
            Destroy(this.gameObject);
        }

    }
}
