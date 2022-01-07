using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NameForm : MonoBehaviour
{
    public GlobalVars global;

    private TextMeshProUGUI input;
    private Button button;
    private GameObject inputBox;
    private GameObject form, assistant1, assistant2;
    private GameObject warningText;

    void Awake()
    {
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
        if(assistant1 == null && !GlobalDataHandler.GetPref(GlobalDataHandler.INTRO1))
        {
            form.SetActive(true);
        }

        if(assistant2 == null)
        {
            GlobalDataHandler.SavePref(GlobalDataHandler.INTRO2, true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            if (GlobalDataHandler.GetPref(GlobalDataHandler.INTRO1))
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
            global.PlayerName = input.text;
            GlobalDataHandler.SavePref(GlobalDataHandler.INTRO1, true);
            global.dictionary.Add(
               1, new string[] {
            "Wspaniale " + global.PlayerName,
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
