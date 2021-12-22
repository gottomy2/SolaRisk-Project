using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToHub : MonoBehaviour
{
    private SceneSwitch sceneSwitch = new SceneSwitch();
    //send user back to hub on esc click from minigames: simon,wires,switches
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sceneSwitch.SceneByPath("Assets/Scenes/ShipInterior/InteriorScene.unity");
        }
    }
}
