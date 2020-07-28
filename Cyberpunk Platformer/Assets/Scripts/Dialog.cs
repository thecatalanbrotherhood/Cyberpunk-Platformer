using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    private bool isTyping;
    private bool inRange;

    private void Start()
    {
        isTyping = false;
    }

    private void Update()
    {
        NextSentence();

        if (inRange == false)
        {
            textDisplay.text = "";
        }
    }

    IEnumerator Type()
    {
        isTyping = true;
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    public void NextSentence()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTyping == false && inRange == true)
        {
            if (index < sentences.Length - 1)
            {
                textDisplay.text = "";
                StartCoroutine(Type());
                index++;
            }
            else
            {
                textDisplay.text = "";
                StartCoroutine(Type());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Character")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Character")
        {
            inRange = false;
        }
    }
}
