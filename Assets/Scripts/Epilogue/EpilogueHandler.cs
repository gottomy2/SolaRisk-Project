using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EpilogueHandler : MonoBehaviour
{

    private const string DEFAULT_DATA = "No Data Provided";
    private const string PERCENTAGE_CHAR = "%";
    private const string RISKY_SOB = "YOU RISKY SON OF A BITCH";
    private const string NEUTRAL_BRD = "YOU NEUTRAL BASTARD";
    private const string AVERSIVE_BRT = "YOU AVERSIVE BRAT";

    private const int GREEN_CHOICE = 1;
    private const int YELLOW_CHOICE = 2;
    private const int RED_CHOICE = 3;

    private List<IData> data;
    private List<int> choices;
    private List<bool> visits;

    private Text minigameSuccess;
    private Text minigameSuccessRatio;

    private Text greenChoices;
    private Text yellowChoices;
    private Text redChoices;

    private Text planetsVisited;

    private Text summaryText;

    private Text flightDaysText;

    private void Awake()
    {
        data = GlobalData.dataList;
        choices = GlobalData.difficultyChoicesList;
        visits = GlobalData.visitedChoicesList;

        minigameSuccessRatio = GameObject.Find("SuccessRatio").GetComponent<Text>();
        minigameSuccess = GameObject.Find("Success").GetComponent<Text>();

        greenChoices = GameObject.Find("GreenPaths").GetComponent<Text>();
        yellowChoices = GameObject.Find("YellowPaths").GetComponent<Text>();
        redChoices = GameObject.Find("RedPaths").GetComponent<Text>();

        planetsVisited = GameObject.Find("PlanetsVisited").GetComponent<Text>();

        summaryText = GameObject.Find("SummaryText").GetComponent<Text>();

        flightDaysText = GameObject.Find("FlightDays").GetComponent<Text>();
    }

    private void InitLighting()
    {
        SceneShader.GetInstance().SetIsLighting(true);
    }

    void Start()
    {
        SetSuccessFields();
        SetRiskFields();
        SetPlanetVisitFields();
        SetSummary(100);
        SetFlightDays();
        InitLighting();
    }

    private void SetSuccessFields()
    {
        if (data.Any())
        {
            minigameSuccess.text = DataProcessor.GetSuccededTryString(data);
            minigameSuccessRatio.text = DataProcessor.GetSuccessTryRatio(data) + PERCENTAGE_CHAR;
        }
        else
        {
            minigameSuccess.text = DEFAULT_DATA;
            minigameSuccessRatio.text = String.Empty;
        }
    }

    private void SetRiskFields()
    {
        if (choices.Any())
        {
            greenChoices.text = DataProcessor.CalculateChoicePercentage(choices, GREEN_CHOICE) + PERCENTAGE_CHAR;
            yellowChoices.text = DataProcessor.CalculateChoicePercentage(choices, YELLOW_CHOICE) + PERCENTAGE_CHAR;
            redChoices.text = DataProcessor.CalculateChoicePercentage(choices, RED_CHOICE) + PERCENTAGE_CHAR;
        }
        else
        {
            greenChoices.text = string.Empty;
            yellowChoices.text = DEFAULT_DATA;
            redChoices.text = string.Empty;
        }
    }

    private void SetPlanetVisitFields()
    {
        planetsVisited.text = visits.Any()
            ? (DataProcessor.CalculatePlanetVisits(visits)
               + " / " + visits.Count)
            : DEFAULT_DATA;
    }

    private void SetSummary(int riskPercentage)
    {
        if (riskPercentage <= 40)
        {
            summaryText.text = AVERSIVE_BRT;
        }
        else if (riskPercentage > 40 && riskPercentage <= 60)
        {
            summaryText.text = NEUTRAL_BRD;
        }
        else
        {
            summaryText.text = RISKY_SOB;
        }
    }

    private void SetFlightDays()
    {
        flightDaysText.text = GlobalData.days.ToString();
    }

}
