using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {
   
    public static Main Instance;
    public GlobalVars global;
    
    public int switchCount;
    public GameObject winText;
    private int _onCount = 0;
    private SceneSwitch sceneSwitch;

    private void Awake() {
        Instance = this;
        sceneSwitch = new SceneSwitch();
    }
    
    public void SwitchChange(int points) {
        _onCount += points;
        if (_onCount == switchCount) {
            winText.SetActive(true); 

            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByPath("Assets/Scenes/FixLightsWireSwitches/FixTheWires.unity"))
            {
                global.setVar("wiresBroken", false, global.hubStats);
                global.setVar("wiresFix", true, global.hubStats);
            }
            else
            {
                global.setVar("switchesBroken", false, global.hubStats);
                global.setVar("switchesFix", true, global.hubStats);
            }
            sceneSwitch.SceneByPath("Assets/Scenes/ShipInterior/InteriorScene.unity");
        }
    }

}