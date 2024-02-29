using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    PlayerOneController p1Script;

    public Button exitShopButton;

    public float stableSwapUpgrade;
    public bool isStableSwapped;

    // Start is called before the first frame update
    void Start()
    {
        p1Script = PlayerOneController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            TriggerExistShopButton();
        }
        //Debug.Log("timer is" + p1Script.warningTimer);
    }

    void TriggerExistShopButton()
    {
        if(exitShopButton != null)
        {
            exitShopButton.onClick.Invoke();
        }
    }

    public void StableSwapUpgrade()
    {
        if (!isStableSwapped)
        {
            Debug.Log("upgraded");
            //p1Script.Warn(stableSwapUpgrade);
            isStableSwapped = true;
        }
        else
        {
            return;
        }
    }
}
