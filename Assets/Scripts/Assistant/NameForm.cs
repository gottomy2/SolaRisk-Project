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


    // Start is called before the first frame update
    void Awake()
    { 
        button = GameObject.Find("SubmitName").GetComponent<Button>();
        input = GameObject.Find("TextInput").GetComponent<TextMeshProUGUI>();
        inputBox = GameObject.Find("NameInput");
        button.gameObject.SetActive(false);
        inputBox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void buttonPressed()
    {
        global.PlayerName = input.text;

        button.gameObject.SetActive(false);
        inputBox.gameObject.SetActive(false);
        sceneSwitch.NextScene();
    }
}
