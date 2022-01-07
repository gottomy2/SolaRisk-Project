using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GlobalVars global;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {   
        if (!GlobalDataHandler.GetPref(GlobalDataHandler.WIRES_BROKEN) && GlobalDataHandler.GetPref(GlobalDataHandler.WIRES_FIX))
        {
            GlobalDataHandler.SavePref(GlobalDataHandler.WIRES_FIX, false);
            player.transform.position = new Vector3(0f, 1.52f, -4f);
        }
        else if(!GlobalDataHandler.GetPref(GlobalDataHandler.SWITCHES_BROKEN) && GlobalDataHandler.GetPref(GlobalDataHandler.SWITCHES_FIX))
        {
            GlobalDataHandler.SavePref(GlobalDataHandler.SWITCHES_FIX, false);
            player.transform.position = new Vector3(0f, 1.52f, -4f);
        }

        if (!GlobalDataHandler.GetPref(GlobalDataHandler.SIMON_BROKEN) && GlobalDataHandler.GetPref(GlobalDataHandler.SIMON_FIX))
        {
            GlobalDataHandler.SavePref(GlobalDataHandler.SIMON_FIX, false);
            player.transform.position = new Vector3(0f, 1.52f, -4f);
        }
    }
    
}
