using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialDialogue : MonoBehaviour
{
    public static TutorialDialogue instance;

    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private string[] sentences;
    [SerializeField]
    private int index;
    [SerializeField]
    private float typingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Type());
        }
        else
        {
            dialogueText.text = "";
        }
    }
}
