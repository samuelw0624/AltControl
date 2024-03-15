using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitch : MonoBehaviour
{
    [SerializeField]
    public Scene currentScene;
    [SerializeField]
    private AudioSource startSound;
    [SerializeField]
    public GameObject instructionText1;
    [SerializeField]
    public GameObject instructionText2;
    [SerializeField]
    private bool isShowed;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        if(!isShowed && instructionText1 != null && instructionText2 != null)
        {
            StartCoroutine(ShowInstructionText());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isShowed)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                SceneManager.LoadScene("LoadingLevel_01");
            }

            if (currentScene.name == "LoadingLevel_01")
            {
                if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Alpha7))
                {
                    StartCoroutine(EnterLevel1());
                }
            }

            if (currentScene.name == "LoadingLevel_02")
            {
                if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Alpha7))
                {
                    StartCoroutine(EnterLevel2());
                }
            }

            if (currentScene.name == "LoadingLevel_03")
            {
                if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Alpha7))
                {
                    StartCoroutine(EnterLevel3());
                }
            }

            if (currentScene.name == "LoadingLevel_04")
            {
                if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Alpha7))
                {
                    StartCoroutine(EnterLevel4());
                }
            }
        }
        

    }
    IEnumerator ShowInstructionText()
    {
        yield return new WaitForSeconds(1.5f);
        instructionText1.SetActive(true);
        instructionText2.SetActive(true);
        isShowed = true;
    }

    IEnumerator EnterLevel1()
    {
        startSound.Play();
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Level_01");
    }

    IEnumerator EnterLevel2()
    {
        startSound.Play();
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Level_02");
    }

    IEnumerator EnterLevel3()
    {
        startSound.Play();
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Level_03");
    }

    IEnumerator EnterLevel4()
    {
        startSound.Play();
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Level_04");
    }
}
