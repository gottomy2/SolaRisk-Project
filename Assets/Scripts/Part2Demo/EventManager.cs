using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public Button flyButton;
    public MapData mapData;
    public GlobalVars global;
    public GameObject assistant1;

    private Planet planet;

    private void Awake()
    {

        if (global.getVar("mapTutorialFinished", global.dialoguePath) && global.getVar("mapReset", global.dialoguePath))
        {
            global.setVar("mapReset", false, global.dialoguePath);
            tutorialFinished();
        }
        else
        {
            if (mapData.firstStart)
            {
                global.setVar("mapTutorial1", false, global.dialoguePath);
                global.setVar("mapTutorial2", false, global.dialoguePath);
                if (!global.getVar("mapTutorialFinished", global.dialoguePath))
                {
                    global.setVar("mapAssistantActive", true, global.dialoguePath);
                }
                else
                {
                    global.setVar("mapAssistantActive", false, global.dialoguePath);
                }
            }
        }


        if (global.getVar("mapTutorial1", global.dialoguePath) || global.getVar("mapTutorialFinished", global.dialoguePath))
        {
            Destroy(assistant1);
        }
        else if (!global.dictionary.ContainsKey(3))
        {
            global.dictionary.Add(3,
            new string[]{
                global.PlayerName + ", witaj w panelu kontroli lotu!",
                "Pozw�l, �e wyt�umacze Ci jak si� nim pos�ugiwa�.",
                "Aby dosta� si� do celu naszej podr�y b�dziemy musieli odwiedzi� a� 3 nieznane planety!",
                "Na ca�e szcz�cie z system kontroli lotu jest wyposa�ony w czujnik zagro�e�...",
                "Po najechaniu kursorem na planet� mo�emy sprawdzi� trudno�� danej trasy!",
                "Wyr�niamy 3 rodzaje tras...",
                "1.Trasy oznaczone kolorem zielonym, wybieraj�c takie trasy mamy 50% szans� na unikni�cie przeszk�d...",
                "2.Trasy oznaczone kolorem ��ltym, wybieraj�c takie trasy mamy 25% szans� na unikni�cie przeszk�d...",
                "I wreszcie numer 3, Trasy oznaczone kolorem czerwonym. wybieraj�c takie trasy nie ob�dzie si� bez przeszk�d.",
                "To jakimi trasami b�dziemy si� porusza� zale�y tylko i wy��cznie od Cieie kapitanie!",               
                "Sprawd�my jak by� sobie poradzi�, �mia�o wybierz pierwsz� tras�!"
                }
            );
        }

        flyButton.onClick.AddListener(onButtonClick);
    }

    private void Update()
    {
        if (assistant1 == null && !global.getVar("mapTutorial1", global.dialoguePath)) {
            if (global.getVar("mapTutorialFinished", global.dialoguePath))
            {
                global.setVar("mapTutorial1", false, global.dialoguePath);
            }
            else
            {
                global.setVar("mapTutorial1", true, global.dialoguePath);
            }
            global.setVar("mapAssistantActive", false, global.dialoguePath);
        }
    }

    private void onButtonClick()
    {
        planet = GameObject.Find(mapData.playerPosition).GetComponent<Planet>();
        if (!global.getVar("mapTutorialFinished", global.dialoguePath))
        {
            if (!global.getVar("mapTutorial2", global.dialoguePath))
            {
                Debug.Log("[TUTORIAL]: Asteroids Minigame occured");
                global.setVar("mapTutorial2", true, global.dialoguePath);
                asteroids();
            }
            else
            {
                Debug.Log("[TUTORIAL]: flappyShip Minigame occured");
                global.setVar("mapTutorialFinished", true, global.dialoguePath);
                flappyShip();
                global.setVar("mapReset", true, global.dialoguePath);
                global.setVar("hubTutorial2", true, global.dialoguePath);
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
        SceneManager.LoadScene("Assets/Scenes/AsteroidsMiniGame/SampleScene.unity");
    }

    private void flappyShip()
    {
        //When flappyShip Minigame
        mapData.lastFlightType = "FlappyShip";
        SceneManager.LoadScene("Assets/Scenes/FlappyShip/LoadingScene.unity");
    }

    private void tutorialFinished()
    {
        mapData.firstStart = true;
        mapData.lastFlightType = "";
        mapData.playerPosition = "Pstart";
        mapData.path = null;
        SceneManager.LoadScene("Assets/Scenes/ShipInterior/InteriorScene.unity");
    }
}
