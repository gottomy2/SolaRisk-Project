using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player;
    void Start()
    {   
        SceneShader.GetInstance().SetShadeSpeed(3f);
        SceneShader.GetInstance().SetIsLighting(true);

        if (!GlobalData.GetVar("wiresBroken", GlobalData.hubStats) && GlobalData.GetVar("wiresFix",GlobalData.hubStats))
        {
            GlobalData.SetVar("wiresFix", false, GlobalData.hubStats);
        }
        else if(!GlobalData.GetVar("switchesBroken",GlobalData.hubStats) && GlobalData.GetVar("switchesFix", GlobalData.hubStats))
        {
            GlobalData.SetVar("switchesFix", false, GlobalData.hubStats);
        }

        if (!GlobalData.GetVar("simonBroken", GlobalData.hubStats) && GlobalData.GetVar("simonFix",GlobalData.hubStats))
        {
            GlobalData.SetVar("simonFix", false, GlobalData.hubStats);
        }
    }

    void Update()
    {
        if (SceneShader.GetInstance().GetCurrentOpacity() == 0)
        {
            SceneShader.GetInstance().DestroyCurtain();
        }
    }
}
