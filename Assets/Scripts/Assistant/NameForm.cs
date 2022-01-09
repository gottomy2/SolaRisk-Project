using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NameForm : MonoBehaviour
{

    private TextMeshProUGUI input;
    private Button button;
    private GameObject inputBox;
    private GameObject form, assistant1, assistant2;
    private GameObject warningText;

    void Awake()
    {
        GlobalData.Init();
        form = GameObject.Find("Form");
        input = GameObject.Find("TextInput").GetComponent<TextMeshProUGUI>();
        warningText = GameObject.Find("warningText");
        assistant1 = GameObject.Find("AssistantImage1");
        assistant2 = GameObject.Find("AssistantImage2");

        form.SetActive(false);
        assistant2.SetActive(false);
        warningText.SetActive(false);
    }

    public void Update()
    {
        if(assistant1 == null && !GlobalData.GetVar("intro1", GlobalData.dialoguePath))
        {
            form.SetActive(true);
        }

        if(assistant2 == null)
        {
            GlobalData.SetVar("intro2", true, GlobalData.dialoguePath);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            if (GlobalData.GetVar("intro1", GlobalData.dialoguePath))
            {
                assistant2.SetActive(true);
            }
        }
    }

    public void buttonPressed()
    {
        if(input.text.Length <= 1)
        {
            warningText.SetActive(true);
        }
        else
        {
            warningText.SetActive(false);
            GlobalData.playerName = input.text;
            GlobalData.SetVar("intro1", true, GlobalData.dialoguePath);

            GlobalData.DIALOGUE_DICTIONARY.Add(
               1, new string[] {
            "Wspaniale " + GlobalData.playerName,
            "Zacznijmy od tego �e ka�dy �wie�o upieczony kapitan powinien wybra� sw�j statek!",
            "Statki oczywi�cie r�ni� si� od siebie...",
            "Ka�dy z nich oferuje pewnego rodzaju udogodnienia podczas podr�y...",
            "Och jakie �ycie by�oby pi�kne gdyby istnia� jeden kt�ry mia�by je wszystkie!",
               }
           );

            form.SetActive(false);
        }
    }
}
