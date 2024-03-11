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
    public GameObject[] shopItem2;
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
    public GameObject[] soldOutItems2;
    [SerializeField]
    public bool isPurchased1, isPurchased2, isPurchased3;
    [SerializeField]
    public int pressedTimes;
    [SerializeField]
    public AudioSource selectionSound;

    [SerializeField]
    public bool isRead;

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
        RecallPurchasingItems();
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
                shopItem[0].SetActive(true);
                shopItem[3].SetActive(false);
                shopItem2[0].SetActive(true);
                shopItem2[3].SetActive(false);

            }
            else
            {
                if (previousItem != selectedItem)
                {
                    shopItem[previousItem].SetActive(false);
                    shopItem[selectedItem].SetActive(true);
                    shopItem2[previousItem].SetActive(false);
                    shopItem2[selectedItem].SetActive(true);

                }
            }

        }
        
        if(firstEnter && !oriShop)
        {
            selectedItem = 0;
            shopItem[selectedItem].SetActive(true);
            shopItem2[selectedItem].SetActive(true);
            oriShop = true;
            pressedTimes = 1;
        }

    }

    void RecallPurchasingItems()
    {
        if(GameManager.instance.currentScene.name == "Level_02" || GameManager.instance.currentScene.name == "Level_03" || GameManager.instance.currentScene.name == "Level_04")
        {
            if (!isRead)
            {
                isPurchased1 = ScoreBoard.instance.item1WasPurchased;
                isPurchased2 = ScoreBoard.instance.item2WasPurchased;
                isPurchased3 = ScoreBoard.instance.item3WasPurchased;
                isRead = true;
            }
        } 
        else if(GameManager.instance.currentScene.name == "Level_Tutorial_01")
        {
            if (!isRead)
            {
                ScoreBoard.instance.item1WasPurchased = false;
                ScoreBoard.instance.item2WasPurchased = false;
                ScoreBoard.instance.item3WasPurchased = false;
                isPurchased1 = ScoreBoard.instance.item1WasPurchased;
                isPurchased2 = ScoreBoard.instance.item2WasPurchased;
                isPurchased3 = ScoreBoard.instance.item3WasPurchased;
                isRead = true;
            }

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
