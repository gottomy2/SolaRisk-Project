using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToHub : MonoBehaviour
{
    //send user back to hub on esc click from minigames: simon,wires,switches
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Assets/Scenes/ShipInterior/InteriorScene.unity");
        }
    }
}
