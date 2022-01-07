using UnityEngine;

public class GlobalDataHandler
{

    private static bool isInited;

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

    public static void Init()
    {
        if (!isInited)
        {
            InitPref(INTRO1, false);
            InitPref(INTRO2, false);
            InitPref(HUB_TUTORIAL1, false);
            InitPref(HUB_TUTORIAL2, false);
            InitPref(MAP_TUTORIAL1, false);
            InitPref(MAP_TUTORIAL2, false);
            InitPref(MAP_TUTORIAL_FINISHED, false);
            InitPref(MAP_ASSISTANT_ACTIVE, true);
            InitPref(MAP_RESET, false);
            
            InitPref(WIRES_BROKEN, false);
            InitPref(WIRES_FIX, false);
            InitPref(SIMON_BROKEN, false);
            InitPref(SIMON_FIX, false);
            InitPref(SWITCHES_BROKEN, false);
            InitPref(SWITCHES_FIX, false);
            InitPref(PLANET_CAN_LAND, false);
            InitPref(PLANET_VISITED, false);
            InitPref(MINIGAME_FAILED, false);
            InitPref(MAP_ACTIVE, true);

            isInited = true;
        }
    }

    private static void InitPref(string name, bool value)
    {
        if (!PlayerPrefs.HasKey(name))
        {
            PlayerPrefs.SetInt(name, value ? 1 : 0);
            PlayerPrefs.Save();
            Debug.Log("INITED PREF WITH NAME=[" + name + "]");
        }
        else
        {
            Debug.Log("PREF [" + name + "] already exists!");
        }
    }
    
    public static void SavePref(string name, bool value)
    {
        Debug.Log("PREF [" + name + "] saved with value=[" + value + "]");
        PlayerPrefs.SetInt(name, value? 1 : 0);
        PlayerPrefs.Save();
    }

    public static bool GetPref(string name)
    {
        return PlayerPrefs.GetInt(name) == 1;
    }
    
}