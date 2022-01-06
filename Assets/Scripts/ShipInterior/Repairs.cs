using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repairs : MonoBehaviour
{
    public GlobalVars global;

    // Update is called once per frame
    void Update()
    {
        if (global.getVar("minigameFailed", global.hubStats) && !global.getVar("simonBroken", global.hubStats) && !global.getVar("wiresBroken", global.hubStats) && !global.getVar("switchesBroken", global.hubStats))
        {
            global.setVar("minigameFailed", false, global.hubStats);
            global.setVar("mapActive", true, global.hubStats);
        }
    }
}
