using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameForm : MonoBehaviour
{
    //public NonClickableDialogues dialogues;
    public GlobalVars global;

    private TextMeshProUGUI input;
    private Button button;
    private GameObject inputBox;
    private SceneSwitch sceneSwitch = new SceneSwitch();
    private GameObject form, assistant1, assistant2;
    private bool done = false;


    // Start is called before the first frame update
    void Awake()
    {
        form = GameObject.Find("Form");
        input = GameObject.Find("TextInput").GetComponent<TextMeshProUGUI>();
        assistant1 = GameObject.Find("AssistantImage1");
        assistant2 = GameObject.Find("AssistantImage2");


        form.SetActive(false);
        assistant2.SetActive(false);
    }

    public void Update()
    {
        if(assistant1 == null && !done)
        {
            form.SetActive(true);
        }
        if(assistant2 != null && done)
        {
            assistant2.SetActive(true);
        }
    }

    public void buttonPressed()
    {
        global.PlayerName = input.text;
        done = true;

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
        /*sceneSwitch.NextScene();*/
    }
}
