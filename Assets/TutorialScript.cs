using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogue1;
    [SerializeField]
    private GameObject dialogue2;
    [SerializeField]
    private GameObject closeTab1;
    [SerializeField]
    private GameObject closeTab2;
    [SerializeField]
    private bool startTutorial1;
    [SerializeField]
    private GameObject Tutorial1;
    [SerializeField]
    private Scene currentScene;
    [SerializeField]
    private GameObject tutorialObject;


    // Start is called before the first frame update
    void Start()
    {
        dialogue1.SetActive(false);
        dialogue2.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        CloseTab();

        if (Tutorial1 != null && Timer.instance.gameStart)
        {
            Tutorial1.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Timer.instance.gameStart && collision.gameObject.CompareTag("Tutorial1") &&!startTutorial1)
        {
            tutorialObject = collision.gameObject;
            print("Hello");
            dialogue1.SetActive(true);
            dialogue2.SetActive(true);
            Timer.instance.stopTimer = true;
            StartCoroutine(TutorialDialogue.instance.Type1());
            StartCoroutine(TutorialDialogue.instance.Type2());
            startTutorial1 = true;
        }
    }

    void CloseTab()
    {
        currentScene = SceneManager.GetActiveScene();
        print(currentScene.name);
        if(currentScene.name == "Level_Tutorial_01")
        {
            if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8))
            {
                dialogue1.SetActive(false);
                dialogue2.SetActive(false);
                Timer.instance.stopTimer = false;
                Timer.instance.inTutorial = false;
                Destroy(tutorialObject);
            }
        }

    }


}
