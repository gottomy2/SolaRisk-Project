using UnityEngine;

public class GlobalDataHandler
{

    private static bool isInited;
    
    public static void Init()
    {
        if (!isInited)
        {
            //SavePref("NazwaPrefa");
        }
    }
    
    public static void SavePref(string name, bool value)
    {
        PlayerPrefs.SetInt(name, value? 1 : 0);
        PlayerPrefs.Save();
    }

    public static bool GetPref(string name)
    {
        return PlayerPrefs.GetInt(name) == 1;
    }
    
}