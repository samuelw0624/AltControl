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
        
    }

    IEnumerator EnterLevel1()
    {
        startSound.Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level_01");
    }
}
