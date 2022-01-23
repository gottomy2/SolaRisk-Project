using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repairs : MonoBehaviour
{
    void Update()
    {
        if (GlobalData.GetVar("minigameFailed", GlobalData.hubStats) && !GlobalData.GetVar("simonBroken", GlobalData.hubStats) && !GlobalData.GetVar("wiresBroken", GlobalData.hubStats) && !GlobalData.GetVar("switchesBroken", GlobalData.hubStats))
        {
            Debug.Log("All repairs are made so the map is now Active and minigameFailed = false");
            GlobalData.SetVar("mapActive", true, GlobalData.hubStats);
            GlobalData.SetVar("minigameFailed", false, GlobalData.hubStats);
        }
    }
}
