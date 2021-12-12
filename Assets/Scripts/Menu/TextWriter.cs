using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    private static TextWriter instance;

    private List<TextWriterSingle> textWriterList;

    private void Awake()
    {
        instance = this;
        textWriterList = new List<TextWriterSingle>();
    }


    public static TextWriterSingle AddWriter_Static(Text uiText, string message, float timePerCharacter, bool invisibleChar)
    {
        instance.RemoveWriter(uiText);
        return instance.AddWriter(uiText, message, timePerCharacter, invisibleChar);
    }

    private TextWriterSingle AddWriter(Text uiText, string message, float timePerCharacter, bool invisibleChar)
    {
        TextWriterSingle textWriterSingle = new TextWriterSingle(uiText, message, timePerCharacter, invisibleChar);
        textWriterList.Add(textWriterSingle);
        return textWriterSingle;
    }

    public static void RemoveWriterStatic(Text uiText)
    {
        instance.RemoveWriter(uiText);
    }

    private void RemoveWriter(Text uiText) { 
        for (int i = 0; i < textWriterList.Count; i++) {
            if (textWriterList[i].getUiText() == uiText){
                textWriterList.RemoveAt(i);
                i--;
            }
        }
    }


    private void Update()
    {
        for(int i = 0; i < textWriterList.Count; i++)
        {
            bool destroyInstance = textWriterList[i].Update();
            if (destroyInstance)
            {
                textWriterList.RemoveAt(i);
                i--;
            }
        }
        
    }


    public class TextWriterSingle
    {
        private Text uiText;
        private string message;
        private int characterIndex;
        private float timePerCharacter;
        private float timer;
        private bool invisibleChar;

        public TextWriterSingle(Text uiText, string message, float timePerCharacter, bool invisibleChar)
        {
            this.uiText = uiText;
            this.message = message;
            this.timePerCharacter = timePerCharacter;
            this.invisibleChar = invisibleChar;
            characterIndex = 0;
        }

        //Returns true on complet
        public bool Update()
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                string text = message.Substring(0, characterIndex);
                if (invisibleChar)
                {
                    text += "<color=#00000000>" + message.Substring(characterIndex) + "</color>";
                }
                uiText.text = text;

                //Checking if text has ended if so sets uiText to Null to prevent crashing.
                if (characterIndex >= message.Length)
                {
                    uiText = null;
                    return true;
                }
            }
            return false;
        }

        public Text getUiText()
        {
            return uiText;
        }
        public bool isActive()
        {
            return characterIndex < message.Length;
        }

        public void WritaAllAndDestroy()
        {
            uiText.text = message;
            characterIndex = message.Length;
            TextWriter.RemoveWriterStatic(uiText);
        }
    }
}
