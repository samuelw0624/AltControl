using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBGM : MonoBehaviour
{
    AudioSource clip;
    // Start is called before the first frame update
    
    void Start()
    {
        clip = GetComponent<AudioSource>();
        clip.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
