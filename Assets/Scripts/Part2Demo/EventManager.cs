using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public Button flyButton;
    public MapData mapData;
    public Planet planet;
    public SceneSwitch sceneSwitch;

    private void Awake()
    {
        sceneSwitch = new SceneSwitch();
        flyButton.onClick.AddListener(onButtonClick);
    }

    private void onButtonClick()
    {
        planet = GameObject.Find(mapData.playerPosition).GetComponent<Planet>();
        if (planet.getDifficulty() == 1)
        {
            eventRandomizer(0.5f);
        }
        else if (planet.getDifficulty() == 2)
        {
            eventRandomizer(0.75f);
        }
        else
        {
            eventRandomizer(1f);
        }
    }

    private void eventRandomizer(float percentage)
    {
        if (Random.value < percentage)
        {
            if(Random.value < 0.5f)
            {
                //When flappyShip Minigame
                mapData.lastFlightType = "FlappyShip";
                Debug.Log("flappyShip Minigame occured");
                sceneSwitch.SceneByPath("Assets/Scenes/FlappyShip/LoadingScene.unity");
            }
            else
            {
                //When Asteroids Minigame occured
                mapData.lastFlightType = "Asteroids";
                Debug.Log("Asteroids Minigame occured");
                sceneSwitch.SceneByPath("Assets/Scenes/AsteroidsMiniGame/SampleScene.unity");
            }
        }
        else
        {
            //Going to the dialogue when no event occured
            mapData.lastFlightType = "Safe";
            Debug.Log("No event occured");
        }
        
    }
}
