using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunFunction : MonoBehaviour
{
    public static StunFunction instance;

    [SerializeField]
    public bool isStuned;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isStuned = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isStuned = false;

        }
    }

}
