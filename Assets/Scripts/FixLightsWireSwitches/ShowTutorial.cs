using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowTutorial : MonoBehaviour
{
    public GameObject tutorial;
    // Start is called before the first frame update
    void Awake()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByPath("Assets/Scenes/FixLightsWireSwitches/FixTheWires.unity"))
        {
            if (GlobalData.firstTimeWires)
            {
                GlobalData.firstTimeWires = false;
            }
            else
            {
                tutorial.SetActive(false);
            }
        }
        else
        {
            if (GlobalData.firstTimeSwitches)
            {
                GlobalData.firstTimeSwitches = false;
            }
            else
            {
                tutorial.SetActive(false);
            }
        }
    }
}
