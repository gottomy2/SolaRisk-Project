using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {
   
    public static Main Instance;

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
                GlobalData.SetVar("wiresBroken", false, GlobalData.hubStats);
                GlobalData.SetVar("wiresFix", true, GlobalData.hubStats);
            }
            else
            {
                GlobalData.SetVar("switchesBroken", false, GlobalData.hubStats);
                GlobalData.SetVar("switchesFix", true, GlobalData.hubStats);
            }
            SceneManager.LoadScene("Assets/Scenes/ShipInterior/InteriorScene.unity");
        }
    }

}