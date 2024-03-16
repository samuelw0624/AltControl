using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SceneTransitionManager : MonoBehaviour
{
    Scene currentScene;
    AudioSource startSound;
    
    [Header("Select Tab")]
    [SerializeField]
    private GameObject restartTab;
    [SerializeField]
    private GameObject nextLevelTab;
    [SerializeField]
    private GameObject mainMenuTab;
    [SerializeField]
    private GameObject restartTab2;
    [SerializeField]
    private GameObject nextLevelTab2;
    [SerializeField]
    private GameObject mainMenuTab2;
    [SerializeField]
    private bool readButton;

    [SerializeField]
    private GameObject scoreBoard1;
    [SerializeField]
    private GameObject scoreBoard2;
    [SerializeField]
    private bool isShow;

    [SerializeField]
    private Animator anim_coffee;
    [SerializeField]
    private GameObject illustration_Drill1;
    [SerializeField]
    private GameObject illustration_Drill2;

    private void Start()
    {
        startSound = this.GetComponent<AudioSource>();
        if (!isShow)
        {
            StartCoroutine(ShowScoreBoard());
        }
    }
    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "TitlePage")
        {
            if(Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Alpha9))
            {
                if (!readButton)
                {
                    startSound.Play();
                    anim_coffee.SetTrigger("Start");
                    illustration_Drill1.SetActive(false);
                    illustration_Drill2.SetActive(false);
                    StartCoroutine(StartLoading());
                }

            }
        }
        if (currentScene.name == "GameOver")
        {
            if (ScoreBoard.instance.isTutorialLevel && !readButton && isShow)
            {
                restartTab.SetActive(true);
                nextLevelTab.SetActive(true);
                mainMenuTab.SetActive(true);

                restartTab2.SetActive(true);
                nextLevelTab2.SetActive(true);
                mainMenuTab2.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    startSound.Play();
                    StartCoroutine(StartLoadingLevel01());
                    
                } 
                
                if (Input.GetKeyDown(KeyCode.Alpha9))
                {
                    startSound.Play();
                    StartCoroutine(StartLoading());
                    
                }
                
                if (Input.GetKeyDown(KeyCode.Alpha8))
                {
                    startSound.Play();
                    StartCoroutine(StartLoadingMainMenu());
                    
                }

            }

            if (ScoreBoard.instance.isLevel01 && !readButton && isShow)
            {
                print("Level01");
                restartTab.SetActive(true);
                nextLevelTab.SetActive(true);
                mainMenuTab.SetActive(true);

                restartTab2.SetActive(true);
                nextLevelTab2.SetActive(true);
                mainMenuTab2.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    startSound.Play();
                    StartCoroutine(StartLoadingLevel02());

                    print("Level01_7");
                }
                
                if (Input.GetKeyDown(KeyCode.Alpha9))
                {
                    startSound.Play();
                    StartCoroutine(StartLoadingLevel01());
                    print("Level01_9");

                }
                
                if (Input.GetKeyDown(KeyCode.Alpha8))
                {
                    startSound.Play();
                    StartCoroutine(StartLoadingMainMenu());
                    print("Level01_8");

                }
            }

            if (ScoreBoard.instance.isLevel02 && !readButton && isShow)
            {
                restartTab.SetActive(true);
                nextLevelTab.SetActive(true);
                mainMenuTab.SetActive(true);

                restartTab2.SetActive(true);
                nextLevelTab2.SetActive(true);
                mainMenuTab2.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    startSound.Play();
                    StartCoroutine(StartLoadingLevel03());
                }
                
                if (Input.GetKeyDown(KeyCode.Alpha9))
                {
                    startSound.Play();
                    StartCoroutine(StartLoadingLevel02());
                }
                
                if (Input.GetKeyDown(KeyCode.Alpha8))
                {
                    startSound.Play();
                    StartCoroutine(StartLoadingMainMenu());
                }
            }

            if (ScoreBoard.instance.isLevel03 && !readButton && isShow)
            {
                restartTab.SetActive(true);
                nextLevelTab.SetActive(true);
                mainMenuTab.SetActive(true);

                restartTab2.SetActive(true);
                nextLevelTab2.SetActive(true);
                mainMenuTab2.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    startSound.Play();
                    StartCoroutine(StartLoadingLevel04());
                }
                
                if (Input.GetKeyDown(KeyCode.Alpha9))
                {
                    startSound.Play();
                    StartCoroutine(StartLoadingLevel03());
                }
                
                if (Input.GetKeyDown(KeyCode.Alpha8))
                {
                    startSound.Play();
                    StartCoroutine(StartLoadingMainMenu());
                }
            }

            if (ScoreBoard.instance.isLevel04 && !readButton && isShow)
            {
                restartTab.SetActive(true);
                nextLevelTab.SetActive(false);
                mainMenuTab.SetActive(true);

                restartTab2.SetActive(true);
                nextLevelTab2.SetActive(false);
                mainMenuTab2.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Alpha9))
                {
                    startSound.Play();
                    StartCoroutine(StartLoadingLevel04());
                }
                
                if (Input.GetKeyDown(KeyCode.Alpha8))
                {
                    startSound.Play();
                    StartCoroutine(StartLoadingMainMenu());
                }
            }
        }
    }

    IEnumerator ShowScoreBoard()
    {
        yield return new WaitForSeconds(3f);
        
        if(scoreBoard1!=null && scoreBoard2 != null)
        {
            scoreBoard1.SetActive(true);
            scoreBoard2.SetActive(true);
        }

        isShow = true;
    }

    IEnumerator StartLoading()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level_Tutorial_01");
        readButton = true;
    }
    IEnumerator StartLoadingMainMenu()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("TitlePage");
        readButton = true;
    }
    IEnumerator StartLoadingLevel01()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Level_01");
        readButton = true;
        print("Loading level 1");
    }
    IEnumerator StartLoadingLevel02()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Level_02");
        readButton = true;
    }
    IEnumerator StartLoadingLevel03()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Level_03");
        readButton = true;
    }
    IEnumerator StartLoadingLevel04()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Level_04");
        readButton = true;
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("TitlePage");
        readButton = true;
    }

    public void LoadMainGame()
    {
        SceneManager.LoadScene("Level_Tutorial_01");
        readButton = true;
    }

    public void Loadlevel01()
    {
        SceneManager.LoadScene("Level_01");
        readButton = true;
        print("Loading level 1");
    }
    public void Loadlevel02()
    {
        SceneManager.LoadScene("Level_02");
        readButton = true;
    }
    public void Loadlevel03()
    {
        SceneManager.LoadScene("Level_03");
        readButton = true;
    }
    public void Loadlevel04()
    {
        SceneManager.LoadScene("Level_04");
        readButton = true;
    }

}
