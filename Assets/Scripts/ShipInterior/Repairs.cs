using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repairs : MonoBehaviour
{
    public GlobalVars global;

    // Update is called once per frame
    void Update()
    {
        if (GlobalData.GetVar("minigameFailed", GlobalData.hubStats) && !GlobalData.GetVar("simonBroken", GlobalData.hubStats) && !GlobalData.GetVar("wiresBroken", GlobalData.hubStats) && !GlobalData.GetVar("switchesBroken", GlobalData.hubStats))
        {
            GlobalData.SetVar("minigameFailed", false, GlobalData.hubStats);
            GlobalData.SetVar("mapActive", true, GlobalData.hubStats);
        }
    }
}
