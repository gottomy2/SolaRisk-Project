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
        if (!global.getVar("wiresBroken", global.hubStats) && global.getVar("wiresFix",global.hubStats))
        {
            global.setVar("wiresFix", false, global.hubStats);
            player.transform.position = new Vector3(0f, 1.52f, -4f);
        }
        else if(!global.getVar("switchesBroken",global.hubStats) && global.getVar("switchesFix", global.hubStats))
        {
            global.setVar("switchesFix", false, global.hubStats);
            player.transform.position = new Vector3(0f, 1.52f, -4f);
        }

        if (!global.getVar("simonBroken", global.hubStats) && global.getVar("simonFix",global.hubStats))
        {
            global.setVar("simonFix", false, global.hubStats);
            player.transform.position = new Vector3(0f, 1.52f, -4f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
