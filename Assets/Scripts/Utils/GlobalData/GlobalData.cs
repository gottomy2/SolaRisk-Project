using System.Collections.Generic;
using UnityEngine;

public class GlobalData
{
    private const string DEFAULT_STRING = "NO_VALUE";
    private const bool DEFAULT_BOOL = false;
    private const int DEFAULT_INT = 0;
    
    public const string PLAYER_NAME = "playerName";

    public const string INTRO1 = "intro1";
    public const string INTRO2 = "intro2";
    public const string HUB_TUTORIAL1 = "hubTutorial1";
    public const string HUB_TUTORIAL2 = "hubTutorial2";
    public const string MAP_TUTORIAL1 = "mapTutorial1";
    public const string MAP_TUTORIAL2 = "mapTutorial2";
    public const string MAP_TUTORIAL_FINISHED = "mapTutorialFinished";
    public const string MAP_ASSISTANT_ACTIVE = "mapAssistantActive";
    public const string MAP_RESET = "mapReset";
    public const string MINIGAME_FAILED = "minigameFailed";
    public const string MAP_ACTIVE = "mapActive";

    public const string SIMON_BROKEN = "simonBroken";
    public const string SIMON_FIX = "simonFix";
    public const string WIRES_BROKEN = "wiresBroken";
    public const string WIRES_FIX = "wiresFix";
    public const string SWITCHES_BROKEN = "switchesBroken";
    public const string SWITCHES_FIX = "switchesFix";

    public const string PLANET_CAN_LAND = "planetCanLand";
    public const string PLANET_VISITED = "planetVisited";

    private static bool isInited;

    public struct variable
    {
        public string key;
        public bool value;

        public variable(string key, bool value)
        {
            this.key = key;
            this.value = value;
        }
    }
    
    //Slider Value
    public static float MainMenuSliderValue = 0.55f;
    
    //Player Name
    public static string playerName = "PLAYER_1";

    //Tutorial & Game
    public static variable[] dialoguePath;
    public static variable[] hubStats;
    public static bool tutorialRepairs;

    //MapData
    public static bool firstStart = true;
    public static string playerPosition = "Pstart";
    public static string currentPlanetType = "Earthlike";
    public static int days = 0;
    public static int maxResources = 7;
    public static int resources = maxResources;
    public static bool planetChanged = false;

    public static List<PlanetData> planets;
    public static List<string> path = new List<string>();
    public static string lastFlightType = "";

    //Risk Data
    public static List<int> difficultyChoicesList;
    public static List<bool> visitedChoicesList;
    public static List<IData> dataList;

    //Flappy Ship Data
    public static bool flappyShipAssistant;

    public static Dictionary<int, string[]> DIALOGUE_DICTIONARY = new Dictionary<int, string[]>();

    public static void Init()
    {
        if (isInited)
            return;

        dialoguePath = new[]
        {
            new variable(INTRO1, false),
            new variable(INTRO2, false),
            new variable(HUB_TUTORIAL1, false),
            new variable(HUB_TUTORIAL2, false),
            new variable(MAP_TUTORIAL1, false),
            new variable(MAP_TUTORIAL2, false),
            new variable(MAP_TUTORIAL_FINISHED, false),
            new variable(MAP_ASSISTANT_ACTIVE, true),
            new variable(MAP_RESET, false),
        };

        hubStats = new[]
        {
            new variable(MINIGAME_FAILED, false),
            new variable(SIMON_BROKEN, false),
            new variable(SIMON_FIX, false),
            new variable(WIRES_BROKEN, false),
            new variable(WIRES_FIX, false),
            new variable(SWITCHES_BROKEN, false),
            new variable(SWITCHES_FIX, false),
            new variable(PLANET_CAN_LAND, false),
            new variable(PLANET_VISITED, false),
            new variable(MAP_ACTIVE, false)
        };

        planets = new List<PlanetData>();
        for (int i = 0; i < 11; i++)
        {
            planets.Add(ScriptableObject.CreateInstance<PlanetData>());
        }
    
        difficultyChoicesList = new List<int>();
        visitedChoicesList = new List<bool>();
        dataList = new List<IData>();

        tutorialRepairs = false;
        flappyShipAssistant = true;

        isInited = true;
    }
    
    public static bool GetVar(string key, variable[] variables)
    {
        for (int i = 0; i < variables.Length; i++)
        {
            if (variables[i].key == key)
            {
                return variables[i].value;
            }
        }
        Debug.Log("Something went wrong while getting var of key: " + key);
        return DEFAULT_BOOL;
    }

    public static void SetVar(string key, bool value, variable[] variables)
    {
        for (int i = 0; i < variables.Length; i++)
        {
            if (variables[i].key == key)
            {
                Debug.Log("Key: " + key + ", value:" + value);
                variables[i].value = value;
                break;
            }
        }
    }
    
    public static void SaveDifficultyChoice(int choice)
    {
        difficultyChoicesList.Add(choice);
        Debug.Log("Saved difficulty choice: " + choice);
    }
    public static void SaveVisitChoice(bool isVisited)
    {
        visitedChoicesList.Add(isVisited);
        Debug.Log("Saved planet visit: " + isVisited);
    }
    public static void SaveData(IData data){
        dataList.Add(data);
        Debug.Log("Data saved: " + data.GetWholeTime() + ", " + data.GetClicks() + ", " + data.GetHasFinished());
    }

}