using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterShop : MonoBehaviour
{
    public static EnterShop instance;

    [SerializeField]
    private GameObject ShopInstruction;
    [SerializeField]
    private GameObject ShopInstruction1;
    [SerializeField]
    public bool withinShopRange;
    [SerializeField]
    public GameObject[] shopItem;
    [SerializeField]
    public int selectedItem;
    [SerializeField]
    public int previousItem;
    [SerializeField]
    public bool firstEnter;
    [SerializeField]
    public bool oriShop;
    [SerializeField]
    public GameObject[] soldOutItems;
    [SerializeField]
    public bool isPurchased1, isPurchased2, isPurchased3;
    [SerializeField]
    public int pressedTimes;
    [SerializeField]
    public AudioSource selectionSound;

    // Start is called before the first frame update
    void Start()
    {
        ShopInstruction.SetActive(false);
        ShopInstruction1.SetActive(false);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectItem()
    {
        if (oriShop)
        {
            previousItem = selectedItem;
            selectedItem += 1;

            if (selectedItem > shopItem.Length -1)
            {

                selectedItem = 0;
                shopItem[3].SetActive(false);

            }
            else
            {
                if (previousItem != selectedItem)
                {
                    shopItem[previousItem].SetActive(false);
                    shopItem[selectedItem].SetActive(true);
                }
            }

        }
        
        if(firstEnter && !oriShop)
        {
            selectedItem = 0;
            shopItem[selectedItem].SetActive(true);
            oriShop = true;
            pressedTimes = 1;
        }





    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShopInstruction.SetActive(true);
            ShopInstruction1.SetActive(true);
            withinShopRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShopInstruction.SetActive(false);
            ShopInstruction1.SetActive(false);
            withinShopRange = false;
        }
    }
}
