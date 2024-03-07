using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private Material skyboxScene1;
    [SerializeField]
    private Material skyboxScene2;
    [SerializeField]
    private Material skyboxScene3;
    [SerializeField]
    private Material skyboxScene4;

    [SerializeField]
    private GameObject shopUI1;
    [SerializeField]
    private GameObject shopUI2;
    [SerializeField]
    private bool inShop;
    [SerializeField]
    public Scene currentScene;

    [Header("Tutorial")]
    [SerializeField]
    private GameObject tutorial1;
    [SerializeField]
    private GameObject tutorial2;
    [SerializeField]
    private Animator t_Anim1;
    [SerializeField]
    private Animator t_Anim2;
    [SerializeField]
    public bool tutorialEnd;
    [SerializeField]
    private GameObject closeTab1;
    [SerializeField]
    private GameObject closeTab2;
    [SerializeField]
    private GameObject skipButton1;
    [SerializeField]
    private GameObject skipButton2;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        SkyboxControl();


    }

    // Update is called once per frame
    void Update()
    {

        if (currentScene.name == "Level_01")
        {
            EnterShop();
        }

        Skip();
        Close();
    }

    void SkyboxControl()
    {
        currentScene = SceneManager.GetActiveScene();
        // Change skybox based on the scene name
        if (currentScene.name == "TitlePage")
        {
            RenderSettings.skybox = skyboxScene1;
        }
        else if (currentScene.name == "Level_01")
        {
            RenderSettings.skybox = skyboxScene2;
        }
        else if (currentScene.name == "GameOver")
        {
            RenderSettings.skybox = skyboxScene3;
        } 
        else if(currentScene.name == "Level_Tutorial_01")
        {
            if (!tutorialEnd && closeTab1 != null && closeTab2 != null)
            {
                //t_Anim1.SetTrigger("TutorialStart");
                //t_Anim2.SetTrigger("TutorialStart");
                StartCoroutine(SkipTutorial());
                StartCoroutine(CloseTutorial());
            }

            if (shopUI1 != null && shopUI2 != null)
            {
                shopUI1.SetActive(false);
                shopUI2.SetActive(false);
            }
        }


    }



    public void EnterShop()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            if (!inShop)
            {
                shopUI1.SetActive(true);
                shopUI2.SetActive(true);
                inShop = true;
            }
            else
            {
                shopUI1.SetActive(false);
                shopUI2.SetActive(false);
                inShop = false;
            }
        } 
    }

    public void CloseShop()
    {
        shopUI1.SetActive(false);
        shopUI2.SetActive(false);
        inShop = false;
    }

    #region Tutorial Level
    IEnumerator CloseTutorial()
    {
        yield return new WaitForSeconds(98f);
        closeTab1.SetActive(true);
        closeTab2.SetActive(true);
        tutorialEnd = true;
    }

    void Close()
    {
        if (tutorialEnd)
        {
            tutorial1.SetActive(false);
            tutorial2.SetActive(false);
        }

    }

    IEnumerator SkipTutorial()
    {
        yield return new WaitForSeconds(3f);
        skipButton1.SetActive(true);
        skipButton2.SetActive(true);
        yield return new WaitForSeconds(7f);
        skipButton1.SetActive(false);
        skipButton2.SetActive(false);
    }

    void Skip()
    {
        if (!tutorialEnd && Input.GetKey(KeyCode.M) || Input.GetKey(KeyCode.B))
        {
            StopCoroutine(CloseTutorial());
            StopCoroutine(SkipTutorial());
            tutorial1.SetActive(false);
            tutorial2.SetActive(false);
            tutorialEnd = true;
            print("Skip");
        }
    }

    #endregion
}
