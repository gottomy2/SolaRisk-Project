using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public Button flyButton;
    public MapData mapData;
    public SceneSwitch sceneSwitch;
    public GlobalVars global;
    public GameObject assistant1;

    private Planet planet;

    private void Awake()
    {

        if (global.getDialoguePath("mapTutorialFinished") && global.getDialoguePath("mapReset"))
        {
            global.setDialoguePath("mapReset", false);
            tutorialFinished();
        }
        else
        {
            if (mapData.firstStart)
            {
                global.setDialoguePath("mapTutorial1", false);
                global.setDialoguePath("mapTutorial2", false);
                if (!global.getDialoguePath("mapTutorialFinished"))
                {
                    global.setDialoguePath("mapAssistantActive", true);
                }
                else
                {
                    global.setDialoguePath("mapAssistantActive", false);
                }
            }
        }


        if (global.getDialoguePath("mapTutorial1") || global.getDialoguePath("mapTutorialFinished"))
        {
            Destroy(assistant1);
        }
        else if (!global.dictionary.ContainsKey(3))
        {
            global.dictionary.Add(3,
            new string[]{
                global.PlayerName + ", witaj w panelu kontroli lotu!",
                "Pozwól, ¿e wyt³umacze Ci jak siê nim pos³ugiwaæ.",
                "Aby dostaæ siê do celu naszej podró¿y bêdziemy musieli odwiedziæ a¿ 3 nieznane planety!",
                "Na ca³e szczêœcie z system kontroli lotu jest wyposa¿ony w czujnik zagro¿eñ...",
                "Po najechaniu kursorem na planetê mo¿emy sprawdziæ trudnoœæ danej trasy!",
                "Wyró¿niamy 3 rodzaje tras...",
                "1.Trasy oznaczone kolorem zielonym, wybieraj¹c takie trasy mamy 50% szansê na unikniêcie przeszkód...",
                "2.Trasy oznaczone kolorem ¿óltym, wybieraj¹c takie trasy mamy 25% szansê na unikniêcie przeszkód...",
                "I wreszcie numer 3, Trasy oznaczone kolorem czerwonym. wybieraj¹c takie trasy nie obêdzie siê bez przeszkód.",
                "To jakimi trasami bêdziemy siê poruszaæ zale¿y tylko i wy³¹cznie od Cieie kapitanie!",               
                "SprawdŸmy jak byœ sobie poradzi³, œmia³o wybierz pierwsz¹ trasê!"
                }
            );
        }

        flyButton.onClick.AddListener(onButtonClick);
    }

    private void Update()
    {
        if (assistant1 == null && !global.getDialoguePath("mapTutorial1")) {
            if (global.getDialoguePath("mapTutorialFinished"))
            {
                global.setDialoguePath("mapTutorial1", false);
            }
            else
            {
                global.setDialoguePath("mapTutorial1", true);
            }
            global.setDialoguePath("mapAssistantActive", false);
        }
    }

    private void onButtonClick()
    {
        planet = GameObject.Find(mapData.playerPosition).GetComponent<Planet>();
        if (!global.getDialoguePath("mapTutorialFinished"))
        {
            if (!global.getDialoguePath("mapTutorial2"))
            {
                Debug.Log("[TUTORIAL]: Asteroids Minigame occured");
                global.setDialoguePath("mapTutorial2", true);
                asteroids();
            }
            else
            {
                Debug.Log("[TUTORIAL]: flappyShip Minigame occured");
                global.setDialoguePath("mapTutorialFinished", true);
                flappyShip();
                global.setDialoguePath("mapReset", true);
            }
        }
        else
        {
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
    }

    private void eventRandomizer(float percentage)
    {
        if (Random.value < percentage)
        {
            if (Random.value < 0.5f)
            {
                Debug.Log("[GAME]: flappyShip Minigame occured");
                flappyShip();
            }
            else
            {
                Debug.Log("[GAME]: Asteroids Minigame occured");
                asteroids();
            }
        }
        else
        {
            //Going to the dialogue when no event occured
            mapData.lastFlightType = "Safe";
            Debug.Log("No event occured");
        }
    }

    private void asteroids()
    {
        //When Asteroids Minigame occured
        mapData.lastFlightType = "Asteroids";
        sceneSwitch.SceneByPath("Assets/Scenes/AsteroidsMiniGame/SampleScene.unity");
    }

    private void flappyShip()
    {
        //When flappyShip Minigame
        mapData.lastFlightType = "FlappyShip";
        sceneSwitch.SceneByPath("Assets/Scenes/FlappyShip/LoadingScene.unity");
    }

    private void tutorialFinished()
    {
        mapData.firstStart = true;
        mapData.lastFlightType = "";
        mapData.playerPosition = "Pstart";
        mapData.path = null;
        sceneSwitch.SceneByPath("Assets/Scenes/ShipInterior/InteriorScene.unity");
    }
}
