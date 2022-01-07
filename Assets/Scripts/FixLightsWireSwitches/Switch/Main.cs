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

    private void Awake() {
        Instance = this;
    }
    
    public void SwitchChange(int points) {
        _onCount += points;
        if (_onCount == switchCount) {
            winText.SetActive(true); 

            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByPath("Assets/Scenes/FixLightsWireSwitches/FixTheWires.unity"))
            {
                GlobalDataHandler.SavePref(GlobalDataHandler.WIRES_BROKEN, false);
                GlobalDataHandler.SavePref(GlobalDataHandler.WIRES_FIX, true);
            }
            else
            {
                GlobalDataHandler.SavePref(GlobalDataHandler.SWITCHES_BROKEN, false);
                GlobalDataHandler.SavePref(GlobalDataHandler.SWITCHES_FIX, true);
            }
            SceneManager.LoadScene("Assets/Scenes/ShipInterior/InteriorScene.unity");
        }
    }

}