using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EpilogueHandler : MonoBehaviour {

    private const string DEFAULT_DATA = "No Data Provided";
    private const string EMPTY = "";
    private const string PERCENTAGE_CHAR = "%";

    [SerializeField]
    private GlobalVars globalVars;

    private List<IData> data;

    private Text minigameSuccess;
    private Text minigameSuccessRatio;
    
    private void Awake(){
       data = globalVars.dataList;

      // data.Add(new FlappyData(10, 10, true));

       minigameSuccessRatio = GameObject.Find("SuccessRatio").GetComponent<Text>();
       minigameSuccess = GameObject.Find("Success").GetComponent<Text>();
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
    }

}
