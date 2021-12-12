using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Assistant : MonoBehaviour
{
    
    private Text messageText;
    private Button button;
    private TextWriter.TextWriterSingle textWriterSingle;
    private int pressedCount = 0;
    private int pressedCount2 = 0;
    private Animator animator;
    public GlobalVars global;
    private Button inputButton;
    private GameObject inputField;
    private TextMeshProUGUI inputText;
    private SceneSwitch sceneSwitch;

    private void Awake()
    {
        sceneSwitch = new SceneSwitch();

        //Dialogue
        messageText = transform.Find("messageBox").Find("message").GetComponent<Text>();
        animator = GetComponent<Animator>();
        button = transform.Find("messageBox").GetComponent<Button>();
        button.onClick.AddListener(pressDialogueBox);

        //Input and c
        inputButton = GameObject.Find("SubmitName").GetComponent<Button>();
        inputText = GameObject.Find("TextInput").GetComponent<TextMeshProUGUI>();
        inputField = GameObject.Find("NameInput");
        inputButton.gameObject.SetActive(false);
        inputField.gameObject.SetActive(false);
    }

    private void pressDialogueBox()
    {
        if (textWriterSingle != null && textWriterSingle.isActive())
        {
            textWriterSingle.WritaAllAndDestroy();
        }
        else
        {
            string[] messageArray = global.Introduction;
            if (pressedCount >= messageArray.Length)
            {
                hideAndDestroyAssistant();
            }
            else
            {
                string message = messageArray[pressedCount];
                textWriterSingle = TextWriter.AddWriter_Static(messageText, message, .05f, true);
                pressedCount++;
            }
        }
    }

    private void pressDialogueBox2()
    {
        animator.Play("IdleAnimation");
        if (textWriterSingle != null && textWriterSingle.isActive())
        {
            textWriterSingle.WritaAllAndDestroy();
        }
        else
        {
            string[] messageArray = new string[]
            {
                global.PlayerName + " wspaniale!"
            };

            if (pressedCount2 >= messageArray.Length)
            {
                hideAndDestroyAssistant2();
            }
            else
            {
                string message2 = messageArray[pressedCount2];
                textWriterSingle = TextWriter.AddWriter_Static(messageText, message2, .05f, true);
                pressedCount2++;
            }
        }
    }

    private void hideAndDestroyAssistant()
    {
        //Moves assistant out of range of camera:
        animator.SetBool("isDialogueFinished", true);

        inputField.gameObject.SetActive(true);
        inputButton.gameObject.SetActive(true);

        //Destroys the assistant objecT:
        //Destroy(gameObject);
    }

    private void hideAndDestroyAssistant2()
    {
        //Moves assistant out of range of camera:
        animator.SetBool("isDialogueFinished", true);

        //Destroys the assistant objecT:
        //Destroy(gameObject);
    }

    public void buttonPressed()
    {
        global.PlayerName = inputText.text;

        inputButton.gameObject.SetActive(false);
        inputField.gameObject.SetActive(false);
        pressDialogueBox2();
        //sceneSwitch.NextScene();
    }
}
