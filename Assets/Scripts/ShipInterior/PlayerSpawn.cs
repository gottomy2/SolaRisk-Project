using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {   
        if (!GlobalData.GetVar("wiresBroken", GlobalData.hubStats) && GlobalData.GetVar("wiresFix",GlobalData.hubStats))
        {
            GlobalData.SetVar("wiresFix", false, GlobalData.hubStats);
            player.transform.position = new Vector3(0f, 1.52f, -4f);
        }
        else if(!GlobalData.GetVar("switchesBroken",GlobalData.hubStats) && GlobalData.GetVar("switchesFix", GlobalData.hubStats))
        {
            GlobalData.SetVar("switchesFix", false, GlobalData.hubStats);
            player.transform.position = new Vector3(0f, 1.52f, -4f);
        }

        if (!GlobalData.GetVar("simonBroken", GlobalData.hubStats) && GlobalData.GetVar("simonFix",GlobalData.hubStats))
        {
            GlobalData.SetVar("simonFix", false, GlobalData.hubStats);
            player.transform.position = new Vector3(0f, 1.52f, -4f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
