using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repairs : MonoBehaviour
{
    public GlobalVars global;

    // Update is called once per frame
    void Update()
    {
        if (GlobalDataHandler.GetPref(GlobalDataHandler.MINIGAME_FAILED) && !GlobalDataHandler.GetPref(GlobalDataHandler.SIMON_BROKEN) && !GlobalDataHandler.GetPref(GlobalDataHandler.WIRES_BROKEN) && !GlobalDataHandler.GetPref(GlobalDataHandler.SWITCHES_BROKEN))
        {
            GlobalDataHandler.SavePref(GlobalDataHandler.MINIGAME_FAILED, false);
            GlobalDataHandler.SavePref(GlobalDataHandler.MAP_ACTIVE, true);
            
        }
    }
}
