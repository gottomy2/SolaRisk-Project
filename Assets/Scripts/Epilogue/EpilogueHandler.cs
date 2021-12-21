using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EpilogueHandler : MonoBehaviour {

    private const string DEFAULT_DATA = "No Data Provided";
    private const string EMPTY = "";
    private const string PERCENTAGE_CHAR = "%";

    private const int GREEN_CHOICE = 1;
    private const int YELLOW_CHOICE = 2;
    private const int RED_CHOICE = 3;

    [SerializeField]
    private GlobalVars globalVars;

    private List<IData> data;
    private List<int> choices;

    private Text minigameSuccess;
    private Text minigameSuccessRatio;

    private Text greenChoices;
    private Text yellowChoices;
    private Text redChoices;
    
    private void Awake(){
       data = globalVars.dataList;
       choices = globalVars.difficultyChoicesList;

       // choices.Add(1);
       // choices.Add(2);
       // choices.Add(3);
       // choices.Add(3);
       // choices.Add(1);
       // choices.Add(1);
      // data.Add(new FlappyData(10, 10, true));

       minigameSuccessRatio = GameObject.Find("SuccessRatio").GetComponent<Text>();
       minigameSuccess = GameObject.Find("Success").GetComponent<Text>();

       greenChoices = GameObject.Find("GreenPaths").GetComponent<Text>();
       yellowChoices = GameObject.Find("YellowPaths").GetComponent<Text>();
       redChoices = GameObject.Find("RedPaths").GetComponent<Text>();
    }

    private void InitLighting(){
        SceneShader.GetInstance().SetIsLighting(true);
    }

    void Start(){
        SetSuccessFields();
        InitLighting();
    }

    void Update(){
        
    }

    private void SetSuccessFields(){
        if(data.Any()){
             minigameSuccess.text = DataProcessor.GetSuccededTryString(data);
             minigameSuccessRatio.text = DataProcessor.GetSuccessTryRatio(data) + PERCENTAGE_CHAR;
        } else {
            minigameSuccess.text = DEFAULT_DATA;
            minigameSuccessRatio.text = EMPTY;
        }

        if (choices.Any())
        {
            greenChoices.text = DataProcessor.CalculateChoicePercentage(choices, GREEN_CHOICE) + "%";
            yellowChoices.text = DataProcessor.CalculateChoicePercentage(choices, YELLOW_CHOICE) + "%";
            redChoices.text = DataProcessor.CalculateChoicePercentage(choices, RED_CHOICE) + "%";
        }
    }

}
