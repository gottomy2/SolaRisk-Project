using System.Collections.Generic;
using UnityEngine;

public class GlobalData
{
    private const string DEFAULT_STRING = "NO_VALUE";
    private const bool DEFAULT_BOOL = false;
    private const int DEFAULT_INT = 0;

    private const string IS_INITED = "isInited";

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

    //MapData
    public static bool firstStart = true;
    public static string playerPosition = "Pstart";
    public static int days = 0;
    public static int maxResources = 7;
    public static int resources = maxResources;

    public static List<PlanetData> planets;
    public static List<string> path = new List<string>();
    public static string lastFlightType = "";

    public static Dictionary<int, string[]> DIALOGUE_DICTIONARY = new Dictionary<int, string[]>
    {
        {
            0, new[]
            {
                "Oho, a kogo to przywia³o?",
                "Nie wydaje mi siê ¿ebyœmy widzieli siê kiedykolwiek wczeœniej...",
                "Pewnie jesteœ tym rekrutem o którym tyle s³ysza³em!",
                "Pozwól no ¿e Ci siê przedstawiê, nazywam siê Mao",
                "A jak Ciebie zw¹?"
            }
        },
        {
            2, new[]
            {
                "Witaj na statku kapitanie!",
                "Zanim zajmiemy przejdziemy do podró¿y pozwól ¿e Ciê oprowadzê!",
                "Zacznijmy od g³ównego panelu!, to jest od mapy!"
            }
        },
    };

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
            new variable(MAP_ASSISTANT_ACTIVE, false),
            new variable(MAP_ASSISTANT_ACTIVE, false),
            new variable(MAP_ASSISTANT_ACTIVE, false)
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
        Debug.Log("Key: " + key + ", value:" + value);
        for (int i = 0; i < variables.Length; i++)
        {
            if (variables[i].key == key)
            {
                Debug.Log("Setting value " + value + " to variables[i].key= " + variables[i].key);
                variables[i].value = value;
                break;
            }
        }
    }
}