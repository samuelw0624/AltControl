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

    [SerializeField]
    private Text dialogueText3;
    [SerializeField]
    private Text dialogueText4;
    [SerializeField]
    private string sentences3;
    [SerializeField]
    private string sentences4;

    [SerializeField]
    private Text dialogueText5;
    [SerializeField]
    private Text dialogueText6;

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

            yield return new WaitForSeconds(typingSpeed);

        }
    }
    public IEnumerator Type2()
    {
        foreach (char letter in sentences2.ToCharArray())
        {
            dialogueText2.text += letter;

            yield return new WaitForSeconds(typingSpeed);

        }
    }

    public IEnumerator Type3()
    {
        foreach (char letter in sentences3.ToCharArray())
        {
            dialogueText3.text += letter;

            yield return new WaitForSeconds(typingSpeed);

        }
    }

    public IEnumerator Type4()
    {
        foreach (char letter in sentences4.ToCharArray())
        {
            dialogueText4.text += letter;

            yield return new WaitForSeconds(typingSpeed);

        }
    }

    public IEnumerator Type5()
    {
        foreach (char letter in sentences3.ToCharArray())
        {
            dialogueText5.text += letter;

            yield return new WaitForSeconds(typingSpeed);

        }
    }

    public IEnumerator Type6()
    {
        foreach (char letter in sentences4.ToCharArray())
        {
            dialogueText6.text += letter;

            yield return new WaitForSeconds(typingSpeed);

        }
    }

}
