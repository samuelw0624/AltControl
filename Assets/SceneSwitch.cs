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
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            SceneManager.LoadScene("LoadingLevel_01");
        }

        if(currentScene.name == "LoadingLevel_01")
        {
            if(Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Alpha7))
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
