using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Assistant : MonoBehaviour
{
    public GlobalVars global;
    public int numerator;

    private Text messageText;
    private Button button;
    private TextWriter.TextWriterSingle textWriterSingle;
    private int pressedCount;
    private Animator animator;
    private SceneSwitch sceneSwitch;

    private void Awake()
    {
        pressedCount = 0;
        //Dialogue
        messageText = transform.Find("messageBox").Find("message").GetComponent<Text>();
        animator = GetComponent<Animator>();
        button = transform.Find("messageBox").GetComponent<Button>();
        button.onClick.AddListener(pressDialogueBox);
    }

    private void pressDialogueBox()
    {

        if (textWriterSingle != null && textWriterSingle.isActive())
        {
            textWriterSingle.WritaAllAndDestroy();
        }
        else
        {
            string[] messageArray = global.dictionary[numerator];
            if (pressedCount >= messageArray.Length)
            {
                hideAndDestroyAssistant();
            }
            else
            {
                string message = messageArray[pressedCount];
                textWriterSingle = TextWriter.AddWriter_Static(messageText, message, .05f, false);
                pressedCount++;
                Debug.Log("TextWriter: " + textWriterSingle.getUiText());
                Debug.Log("pressedCount: " + pressedCount);
            }
        }
    }

    private void hideAndDestroyAssistant()
    {
        //Moves assistant out of range of camera:
        animator.SetBool("isDialogueFinished", true);

        //Destroys the assistant objecT:
        Destroy(gameObject,1f);
    }
}
