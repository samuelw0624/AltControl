using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    public static TutorialScript instance;
    [Header("Tutorial_01")]
    [SerializeField]
    private GameObject dialogue1;
    [SerializeField]
    private GameObject dialogue2;
    [SerializeField]
    private GameObject closeTab1;
    [SerializeField]
    private GameObject closeTab2;
    [SerializeField]
    public bool startTutorial1;
    [SerializeField]
    private GameObject Tutorial1;
    [SerializeField]
    private Scene currentScene;
    [SerializeField]
    private GameObject tutorialObject;
    private bool close1;

    [Header("Tutorial_02")]
    [SerializeField]
    private GameObject dialogue2_1;
    [SerializeField]
    private GameObject dialogue2_2;
    [SerializeField]
    public bool startTutorial2;
    [SerializeField]
    private GameObject tutorialObject2;

    [SerializeField]
    private GameObject dialogue3_1;
    [SerializeField]
    private GameObject dialogue3_2;
    public bool close2;
    [SerializeField]
    private Transform cam1;
    [SerializeField]
    private Transform dialogue3_1Transform;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        dialogue1.SetActive(false);
        dialogue2.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        CloseTab();
        CloseTab2();
        CloseTab3();

        if (Tutorial1 != null && Timer.instance.gameStart)
        {
            Tutorial1.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Timer.instance.gameStart)
        {
            if(collision.gameObject.CompareTag("Tutorial1") && !startTutorial1)
            {
                tutorialObject = collision.gameObject;
                dialogue1.SetActive(true);
                dialogue2.SetActive(true);
                Timer.instance.stopTimer = true;
                StartCoroutine(TutorialDialogue.instance.Type1());
                StartCoroutine(TutorialDialogue.instance.Type2());
                startTutorial1 = true;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Timer.instance.gameStart)
        {
            if (collision.gameObject.CompareTag("Tutorial2") && !startTutorial2)
            {
                tutorialObject2 = collision.gameObject;
                Timer.instance.stopTimer = true;
                Timer.instance.inTutorial = true;

                dialogue2_1.SetActive(true);
                dialogue2_2.SetActive(true);

                StartCoroutine(TutorialDialogue.instance.Type3());
                StartCoroutine(TutorialDialogue.instance.Type4());
                startTutorial2 = true;
            }

            if (collision.gameObject.CompareTag("Tutorial3") && !startTutorial2)
            {
                tutorialObject2 = collision.gameObject;
                Timer.instance.stopTimer = true;
                Timer.instance.inTutorial = true;

                dialogue3_1.SetActive(true);
                dialogue3_2.SetActive(true);

                StartCoroutine(TutorialDialogue.instance.Type5());
                StartCoroutine(TutorialDialogue.instance.Type6());
                startTutorial2 = true;
            }
        }
    }

    void CloseTab()
    {
        currentScene = SceneManager.GetActiveScene();
        print(currentScene.name);
        if(currentScene.name == "Level_Tutorial_01" && !close1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8))
            {
                dialogue1.SetActive(false);
                dialogue2.SetActive(false);
                Timer.instance.stopTimer = false;
                Timer.instance.inTutorial = false;
                Destroy(tutorialObject);
                Timer.instance.StartTimerAction();
                close1 = true;
            }
        }

    }

    void CloseTab2()
    {
        if (currentScene.name == "Level_Tutorial_01")
        {
            if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8))
            {
                dialogue2_1.SetActive(false);
                dialogue2_2.SetActive(false);
                Timer.instance.stopTimer = false;
                Timer.instance.inTutorial = false;
                Destroy(tutorialObject2);
                Timer.instance.StartTimerAction();
                close2 = true;
            }
        }
    }

    void CloseTab3()
    {
        if (currentScene.name == "Level_Tutorial_01")
        {
            if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8))
            {
                dialogue3_1.SetActive(false);
                dialogue3_2.SetActive(false);
                Timer.instance.stopTimer = false;
                Timer.instance.inTutorial = false;
                Destroy(tutorialObject2);
                Timer.instance.StartTimerAction();
                close2 = true;
            }
        }
    }


}
