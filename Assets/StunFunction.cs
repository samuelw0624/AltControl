using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunFunction : MonoBehaviour
{
    public static StunFunction instance;

    [SerializeField]
    public bool isStuned;
    [SerializeField]
    public bool stunApplied;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerCollider")
        {
            isStuned = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerCollider")
        {
            isStuned = false;

        }
    }


}
