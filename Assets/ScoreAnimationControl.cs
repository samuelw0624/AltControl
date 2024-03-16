using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAnimationControl : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.SetTrigger("Score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
