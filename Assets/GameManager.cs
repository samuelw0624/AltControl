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



    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        SkyboxControl();
        shopUI1.SetActive(false);
        shopUI2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentScene.name == "Level_01")
        {
            EnterShop();
        }

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

        print(RenderSettings.skybox);
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


}
