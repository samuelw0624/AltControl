using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialDialogue : MonoBehaviour
{
    public static TutorialDialogue instance;

    [SerializeField]
    private Text dialogueText2;
    [SerializeField]
    private Text dialogueText1;
    [SerializeField]
    private string sentences1;
    [SerializeField]
    private string sentences2;

    [SerializeField]
    private float typingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator Type1()
    {
        foreach(char letter in sentences1.ToCharArray())
        {
            dialogueText1.text += letter;
            //dialogueText2.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }
    public IEnumerator Type2()
    {
        foreach (char letter in sentences2.ToCharArray())
        {
            dialogueText2.text += letter;
            //dialogueText2.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }

}
