using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameForm : MonoBehaviour
{
    public GlobalVars global;

    private TextMeshProUGUI input;
    private Button button;
    private GameObject inputBox;
    private SceneSwitch sceneSwitch = new SceneSwitch();
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
        if(assistant1 == null && !global.dialoguePath["assistant1"])
        {
            form.SetActive(true);
        }
        if(assistant2 != null && global.dialoguePath["assistant1"])
        {
            assistant2.SetActive(true);
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
            global.dialoguePath["assistant1"] = true;

            global.dictionary.Add(
               1, new string[] {
            "Wspaniale " + global.PlayerName,
            "Zacznijmy od tego ¿e ka¿dy œwie¿o upieczony kapitan powinien wybraæ swój statek!",
            "Statki oczywiœcie ró¿ni¹ siê od siebie...",
            "Ka¿dy z nich oferuje pewnego rodzaju udogodnienia podczas podró¿y...",
            "Och jakie ¿ycie by³oby piêkne gdyby istnia³ jeden który mia³by je wszystkie!",
               }
           );

            form.SetActive(false);
            //sceneSwitch.NextScene();
        }
    }
}
