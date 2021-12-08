using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textDialog;
    [SerializeField] string[] sentences;
    private int index;
    [SerializeField] float typingSpeed;
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject nextLevelButton;

    void Start()
    {
        StartCoroutine(Type());
    }

    void Update()
    {
        if (textDialog.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDialog.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public int getIndex()
    {
        return index;
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            textDialog.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDialog.text = "";
            continueButton.SetActive(false);
            nextLevelButton.SetActive(true);
        }
    }

    
}
