using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DrillController : MonoBehaviour
{
    //reference variables
    PlayerOneController p1Script;
    public GameObject flatDrill;
    public GameObject hexDrill;
    public GameObject crossDrill;
    public GameObject superDrill;

    [Header("Shop")]
    [SerializeField]
    public bool keyPressed;
    [SerializeField]
    public GameObject shopUI;
    [SerializeField]
    private AudioSource insufficientFundSound;
    [SerializeField]
    private AudioSource purchaseSuccessfulSound;




    public enum DrillType
    {
        CrossDrill,
        FlatDrill,
        HexDrill,
        SuperDrill,
        None
    }

    public DrillType currentDrill;
    
    // Start is called before the first frame update
    void Start()
    {
        p1Script = PlayerOneController.instance;
        
        //standard starting drill
        currentDrill = DrillType.None;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(p1Script);
        Enter();
        SwitchDrill();
        //HandleDrills();
    }

    #region Drill Methods
    void SwitchDrill()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (PlayerOneController.instance.gameEnd)
            {
                StartCoroutine(RestartGame());
                PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
            }
            print("1");
            if (EnterShop.instance.isPurchased2)
            {
                if (PlayerOneController.instance.currentScrew == PlayerOneController.ScrewType.SuperDrill)
                {
                    currentDrill = DrillType.SuperDrill;
                    //activate UI icons
                    flatDrill.SetActive(false);
                    hexDrill.SetActive(false);
                    crossDrill.SetActive(false);
                    superDrill.SetActive(true);

                    p1Script.FixSign();

                }
            }
            else
            {
                print("2");
                if (PlayerOneController.instance.currentScrew == PlayerOneController.ScrewType.FlatScrew)
                {
                    print("3");
                    currentDrill = DrillType.FlatDrill;
                    //activate UI icons
                    flatDrill.SetActive(true);
                    hexDrill.SetActive(false);
                    crossDrill.SetActive(false);
                    superDrill.SetActive(false);

                    p1Script.FixSign();
                }

            }

            TutorialDialogue.instance.NextSentence();

            EnterShop.instance.pressedTimes += 1;
            //Debug.Log("flat screw is activated ");

            keyPressed = true;
            Purchase();
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (PlayerOneController.instance.gameEnd)
            {
                StartCoroutine(RestartGame());
                PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
            }
            if (PlayerOneController.instance.currentScrew == PlayerOneController.ScrewType.SuperDrill)
            {
                currentDrill = DrillType.SuperDrill;

                //activate UI icons
                flatDrill.SetActive(false);
                hexDrill.SetActive(false);
                crossDrill.SetActive(false);
                superDrill.SetActive(true);

                p1Script.FixSign();
            }
            else
            {
                currentDrill = DrillType.HexDrill;

                //activate UI icons
                flatDrill.SetActive(false);
                hexDrill.SetActive(true);
                crossDrill.SetActive(false);
                superDrill.SetActive(false);

                //print("currentScrew" + PlayerOneController.instance.currentScrew);
                if (PlayerOneController.instance.currentScrew == PlayerOneController.ScrewType.HexScrew)
                {
                    p1Script.FixSign();
                }
            }

            TutorialDialogue.instance.NextSentence();
            EnterShop.instance.pressedTimes += 1;

            //Debug.Log("hex screw is activated");


            keyPressed = true;
            Purchase();
        }


        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (PlayerOneController.instance.gameEnd)
            {
                StartCoroutine(RestartGame());
                PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
            }

            if (PlayerOneController.instance.currentScrew == PlayerOneController.ScrewType.SuperDrill)
            {
                currentDrill = DrillType.SuperDrill;
                //activate UI icons
                flatDrill.SetActive(false);
                hexDrill.SetActive(false);
                crossDrill.SetActive(false);
                superDrill.SetActive(true);

                p1Script.FixSign();

            }
            else
            {
                currentDrill = DrillType.CrossDrill;
                //activate UI icons
                flatDrill.SetActive(false);
                hexDrill.SetActive(false);
                crossDrill.SetActive(true);
                superDrill.SetActive(false);

                //print("currentScrew" + PlayerOneController.instance.currentScrew);
                if (PlayerOneController.instance.currentScrew == PlayerOneController.ScrewType.CrossScrew)
                {
                    p1Script.FixSign();
                }
            }

            TutorialDialogue.instance.NextSentence();
            EnterShop.instance.pressedTimes += 1;

            //Debug.Log("cross screw is activated");

            keyPressed = true;
            Purchase();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            currentDrill = DrillType.None;
            //activate UI icons
            flatDrill.SetActive(false);
            hexDrill.SetActive(false);
            crossDrill.SetActive(false);
            //Debug.Log("Deactivate screw");
        }
    }


    //void HandleDrills()
    //{
    //    CheckDrillStatus();
    //    if (p1Script.isInDrillSlot)
    //    {
    //        //each drill case checks whether the current screw matched the current drill
    //        switch (currentDrill)
    //        {
    //            case DrillType.CrossDrill:
    //                if (PlayerOneController.instance.currentScrew == PlayerOneController.ScrewType.CrossScrew)
    //                {
    //                    p1Script.FixSign();
    //                }
    //                break;
    //            case DrillType.FlatDrill:
    //                if (PlayerOneController.instance.currentScrew == PlayerOneController.ScrewType.FlatScrew)
    //                {
    //                    p1Script.FixSign();
    //                }
    //                break;
    //            case DrillType.HexDrill:
    //                if (PlayerOneController.instance.currentScrew == PlayerOneController.ScrewType.HexScrew)
    //                {
    //                    p1Script.FixSign();
    //                }
    //                break;
    //        }
    //    }
    //    else
    //    {
    //        //Debug.Log("drill is not in slot to repair");
    //        //UX functions
    //    }
    //}

    //void CheckDrillStatus()
    //{
    //    Debug.Log("drill in slot is " + p1Script.isInDrillSlot);

    //    if (Keyboard.current[Key.P].wasPressedThisFrame && (p1Script.signOnLeft || p1Script.signOnRight))
    //    {
    //        p1Script.isInDrillSlot = true;
    //    }
    //    else if (Keyboard.current[Key.L].wasPressedThisFrame && (p1Script.signOnLeft || p1Script.signOnRight))
    //    {
    //        p1Script.isInDrillSlot = false;
    //    }
    //}
    #endregion

    #region Shop
    private void Enter()
    {
        if (EnterShop.instance.withinShopRange && keyPressed)
        {
            shopUI.SetActive(true);
            PlayerOneController.instance.isFreezed = true;
            EnterShop.instance.firstEnter = true;

        }
        else
        {
            LeaveShop();
        }
    }


    public void LeaveShop()
    {
        shopUI.SetActive(false);
        PlayerOneController.instance.isFreezed = false;
        keyPressed = false;
        for (int i = 0; i < EnterShop.instance.shopItem.Length; i++)
        {
            EnterShop.instance.shopItem[i].SetActive(false);
        }
        EnterShop.instance.selectedItem = 0;
        EnterShop.instance.firstEnter = false;
        EnterShop.instance.oriShop = false;
    }


    private void Purchase()
    {
        if (EnterShop.instance.firstEnter)
        {
            if (EnterShop.instance.selectedItem == 0 && EnterShop.instance.isPurchased1 == false && EnterShop.instance.pressedTimes > 1)
            {
                if (ScoreManager.instance.score >= 50)
                {
                    EnterShop.instance.soldOutItems[0].SetActive(true);
                    EnterShop.instance.isPurchased1 = true;
                    ScoreManager.instance.ReducePoint(50);
                    purchaseSuccessfulSound.Play();
                }
                else
                {

                }

            }

            if (EnterShop.instance.selectedItem == 1 && EnterShop.instance.isPurchased2 == false && EnterShop.instance.pressedTimes > 1)
            {
                if (ScoreManager.instance.score >= 100)
                {
                    EnterShop.instance.soldOutItems[1].SetActive(true);
                    EnterShop.instance.isPurchased2 = true;
                    ScoreManager.instance.ReducePoint(100);
                    purchaseSuccessfulSound.Play();
                }
                else
                {
                    insufficientFundSound.Play();
                }
            }

            if (EnterShop.instance.selectedItem == 2 && EnterShop.instance.isPurchased3 == false && EnterShop.instance.pressedTimes > 1)
            {
                if (ScoreManager.instance.score >= 80)
                {
                    EnterShop.instance.soldOutItems[2].SetActive(true);
                    EnterShop.instance.isPurchased3 = true;
                    ScoreManager.instance.ReducePoint(80);
                    purchaseSuccessfulSound.Play();

                }
                else
                {
                    insufficientFundSound.Play();
                }
            }

            if (EnterShop.instance.selectedItem == 3)
            {
                LeaveShop();
                EnterShop.instance.pressedTimes = 0;
            }
        }
    }          
    #endregion

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("TitlePage");
    }
}
