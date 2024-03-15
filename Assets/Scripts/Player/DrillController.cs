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
    public GameObject shopUI2;
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
            if (PlayerOneController.instance.gameEnd && PlayerOneController.instance.delayScoreboardUI)
            {
                if (GameManager.instance.currentScene.name == "Level_Tutorial_01")
                {
                    PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
                    ScoreManager.instance.restartGame = true;
                    ScoreManager.instance.isRead = false;
                    StartCoroutine(EnterTutorialLevel01());
                    //SceneManager.LoadScene("LoadingLevel_01");
                    
                }

                if(GameManager.instance.currentScene.name == "Level_01")
                {
                    PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
                    ScoreManager.instance.restartGame = true;
                    ScoreManager.instance.isRead = false;
                    StartCoroutine(EnterLevel01());
                }
                
                if(GameManager.instance.currentScene.name == "Level_02")
                {
                    print("Loading scene 03");
                    PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
                    ScoreManager.instance.restartGame = true;
                    ScoreManager.instance.isRead = false;
                    StartCoroutine(EnterLevel02());
                }

                if (GameManager.instance.currentScene.name == "Level_03")
                {
                    PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
                    ScoreManager.instance.restartGame = true;
                    ScoreManager.instance.isRead = false;
                    StartCoroutine(EnterLevel03());
                }

                if (GameManager.instance.currentScene.name == "Level_04")
                {
                    PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
                    ScoreManager.instance.restartGame = true;
                    ScoreManager.instance.isRead = false;
                    StartCoroutine(EnterLevel04());
                }

            }


            if (EnterShop.instance.isPurchased2)
            {
                currentDrill = DrillType.SuperDrill;
                //activate UI icons
                flatDrill.SetActive(false);
                hexDrill.SetActive(false);
                crossDrill.SetActive(false);
                superDrill.SetActive(true);
                if (PlayerOneController.instance.currentScrew == PlayerOneController.ScrewType.SuperDrill)
                {
                    if (!PlayerOneController.instance.isFreezed && !PlayerOneController.instance.gameEnd)
                    {
                        p1Script.FixSign();
                    }
                }
            }
            else
            {
                currentDrill = DrillType.FlatDrill;
                //activate UI icons
                flatDrill.SetActive(true);
                hexDrill.SetActive(false);
                crossDrill.SetActive(false);
                superDrill.SetActive(false);

                if (PlayerOneController.instance.currentScrew == PlayerOneController.ScrewType.FlatScrew)
                {
                    if(!PlayerOneController.instance.isFreezed && !PlayerOneController.instance.gameEnd)
                    {

                        p1Script.FixSign();

                    }

                }

            }

            EnterShop.instance.pressedTimes += 1;
            //Debug.Log("flat screw is activated ");

            keyPressed = true;
            Purchase();
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (PlayerOneController.instance.gameEnd && PlayerOneController.instance.delayScoreboardUI)
            {
                if (GameManager.instance.currentScene.name == "Level_Tutorial_01")
                {
                    //loading scene

                    PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
                    StartCoroutine(EnterLoading01());
                }

                if (GameManager.instance.currentScene.name == "Level_01")
                {
                    PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
                    StartCoroutine(EnterLoading02());
                }

                if (GameManager.instance.currentScene.name == "Level_02")
                {
                    PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
                    StartCoroutine(EnterLoading03());
                }

                if (GameManager.instance.currentScene.name == "Level_03")
                {
                    PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
                    StartCoroutine(EnterLoading04());
                }

                if (GameManager.instance.currentScene.name == "Level_04")
                {
                    PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
                    StartCoroutine(RestartGame());
                }
            }


            if (EnterShop.instance.isPurchased2)
            {
                currentDrill = DrillType.SuperDrill;

                //activate UI icons
                flatDrill.SetActive(false);
                hexDrill.SetActive(false);
                crossDrill.SetActive(false);
                superDrill.SetActive(true);

                if (PlayerOneController.instance.currentScrew == PlayerOneController.ScrewType.SuperDrill)
                {
                    if (!PlayerOneController.instance.isFreezed && !PlayerOneController.instance.gameEnd)
                    {

                        p1Script.FixSign();

                    }
                }
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
                    if (!PlayerOneController.instance.isFreezed && !PlayerOneController.instance.gameEnd)
                    {

                        p1Script.FixSign();

                    }
                }
            }

            EnterShop.instance.pressedTimes += 1;

            //Debug.Log("hex screw is activated");


            keyPressed = true;
            Purchase();
        }


        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (PlayerOneController.instance.gameEnd && PlayerOneController.instance.delayScoreboardUI)
            {
                if (GameManager.instance.currentScene.name == "Level_Tutorial_01")
                {
                    //loading scene
                    PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
                    StartCoroutine(RestartGame());
                }

                if (GameManager.instance.currentScene.name == "Level_01")
                {
                    PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
                    StartCoroutine(RestartGame());
                }

                if (GameManager.instance.currentScene.name == "Level_02")
                {
                    PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
                    StartCoroutine(RestartGame());
                }

                if (GameManager.instance.currentScene.name == "Level_03")
                {
                    PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
                    StartCoroutine(RestartGame());
                }

                if (GameManager.instance.currentScene.name == "Level_04")
                {
                    PlayerOneController.instance.repairAudio.PlayOneShot(PlayerOneController.instance.repairClip);
                    StartCoroutine(RestartGame());
                }

            }


            if (EnterShop.instance.isPurchased2)
            {
                currentDrill = DrillType.SuperDrill;
                //activate UI icons
                flatDrill.SetActive(false);
                hexDrill.SetActive(false);
                crossDrill.SetActive(false);
                superDrill.SetActive(true);
                if (PlayerOneController.instance.currentScrew == PlayerOneController.ScrewType.SuperDrill)
                {
                    if (!PlayerOneController.instance.isFreezed && !PlayerOneController.instance.gameEnd)
                    {
 
                        p1Script.FixSign();

                    }
                }
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
                    if (!PlayerOneController.instance.isFreezed && !PlayerOneController.instance.gameEnd)
                    {

                        p1Script.FixSign();

                    }
                    //print("Repair");
                }
            }

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
        if(GameManager.instance.currentScene.name == "Level_02" || GameManager.instance.currentScene.name == "Level_03" || GameManager.instance.currentScene.name == "Level_04")
        {
            if (EnterShop.instance.withinShopRange && keyPressed)
            {
                //shopUI.SetActive(true);
                //shopUI2.SetActive(true);
                StartCoroutine(ShowShopUI());


            }
            else
            {
                LeaveShop();
            }
        }

    }

    IEnumerator ShowShopUI()
    {
        yield return new WaitForSeconds(0.7f);
        shopUI.SetActive(true);
        shopUI2.SetActive(true);
        PlayerOneController.instance.isFreezed = true;
        EnterShop.instance.firstEnter = true;
     
    }

    IEnumerator CloseShopUI()
    {
        yield return new WaitForSeconds(0.5f);
        shopUI.SetActive(false);
        shopUI2.SetActive(false);
        PlayerOneController.instance.isFreezed = false;
    }


    public void LeaveShop()
    {
        if(shopUI != null && shopUI2 != null)
        {
            //shopUI.SetActive(false);
            //shopUI2.SetActive(false);
            StartCoroutine(CloseShopUI());
           

        }
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
                if (ScoreManager.instance.score >= 750)
                {
                    EnterShop.instance.soldOutItems[0].SetActive(true);
                    EnterShop.instance.soldOutItems2[0].SetActive(true);
                    EnterShop.instance.isPurchased1 = true;
                    ScoreManager.instance.ReducePoint(750);
                    purchaseSuccessfulSound.Play();
                }
                else
                {

                }

            }

            if (EnterShop.instance.selectedItem == 1 && EnterShop.instance.isPurchased2 == false && EnterShop.instance.pressedTimes > 1)
            {
                if (ScoreManager.instance.score >= 2000)
                {
                    EnterShop.instance.soldOutItems[1].SetActive(true);
                    EnterShop.instance.soldOutItems2[1].SetActive(true);
                    EnterShop.instance.isPurchased2 = true;
                    ScoreManager.instance.ReducePoint(2000);
                    purchaseSuccessfulSound.Play();
                }
                else
                {
                    insufficientFundSound.Play();
                }
            }

            if (EnterShop.instance.selectedItem == 2 && EnterShop.instance.isPurchased3 == false && EnterShop.instance.pressedTimes > 1)
            {
                if (ScoreManager.instance.score >= 1000)
                {
                    EnterShop.instance.soldOutItems[2].SetActive(true);
                    EnterShop.instance.soldOutItems2[2].SetActive(true);
                    EnterShop.instance.isPurchased3 = true;
                    ScoreManager.instance.ReducePoint(1000);
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
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("TitlePage");
    }

    IEnumerator EnterTutorialLevel01()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Level_Tutorial_01");
    }

    IEnumerator EnterLoading01()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("LoadingLevel_01");
    }

    IEnumerator EnterLevel01()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Level_01");
    }

    IEnumerator EnterLoading02()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("LoadingLevel_02");
    }

    IEnumerator EnterLevel02()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Level_02");
    }

    IEnumerator EnterLoading03()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("LoadingLevel_03");
    }

    IEnumerator EnterLevel03()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Level_03");
    }

    IEnumerator EnterLoading04()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("LoadingLevel_04");
    }

    IEnumerator EnterLevel04()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Level_04");
    }



}
