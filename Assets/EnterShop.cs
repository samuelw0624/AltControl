using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterShop : MonoBehaviour
{

    [SerializeField]
    private GameObject ShopInstruction;
    [SerializeField]
    private GameObject ShopInstruction1;
    // Start is called before the first frame update
    void Start()
    {
        ShopInstruction.SetActive(false);
        ShopInstruction1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShopInstruction.SetActive(true);
            ShopInstruction1.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShopInstruction.SetActive(false);
            ShopInstruction1.SetActive(false);
        }
    }
}
