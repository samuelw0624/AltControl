using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class BoundaryController : MonoBehaviour
{
    [SerializeField]
    private PlayerOneController p1c;
    // Start is called before the first frame update
    void Start()
    {
        p1c = PlayerOneController.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            // If the player exits the boundary, reset its position to the origin
            other.transform.position = Vector3.zero; // Reset player position to (0, 0, 0)
        }
        else
        {
            // If other objects exit the boundary, destroy them
            //Destroy(other.gameObject);
            p1c.Warn(3f);
        }
    }
}
