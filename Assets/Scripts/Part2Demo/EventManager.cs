using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EventManager : MonoBehaviour
{
    public Button flyButton;
    public MapData mapData;
    public GlobalVars global;
    public GameObject assistant1;
    public GameObject warningText;  
    
    private Planet planet;
    private Difficulty difficulty;
    private enum State
    {
        Tutorial,
        Game
    }
    
    private string[] TUTORIAL_DIALOGUE = {
        ", witaj w panelu kontroli lotu!",
        "Pozwól, ?e wyt?umacz? Ci jak si? nim pos?ugiwa?.",
        "Aby dosta? si? do celu naszej podró?y, b?dziemy musieli odwiedzi? a? 3 nieznane planety!",
        "Na ca?e szcz??cie, system kontroli lotu jest wyposa?ony w czujnik zagro?e?...",
        "Po najechaniu kursorem na planetê mo¿emy sprawdziæ trudnoœæ danej trasy!",
        "Wyró?niamy 3 rodzaje tras...",
        "1.Trasy oznaczone kolorem zielonym, wybieraj¹c takie trasy mamy 50% szansê na unikniêcie przeszkód...",
        "2.Trasy oznaczone kolorem ¿óltym, wybieraj¹c takie trasy mamy 25% szansê na unikniêcie przeszkód...",
        "I wreszcie numer 3, Trasy oznaczone kolorem czerwonym. wybieraj¹c takie trasy nie obêdzie siê bez przeszkód.",
        "To jakimi trasami bêdziemy siê poruszaæ zale¿y tylko i wy³¹cznie od Cieie kapitanie!",
        "SprawdŸmy jak byœ sobie poradzi³, œmia³o wybierz pierwsz¹ trasê!"
    };

    private void HandleMinigameFails()
    {
        if (GlobalDataHandler.GetPref(GlobalDataHandler.MINIGAME_FAILED)
            && GlobalDataHandler.GetPref(GlobalDataHandler.MAP_TUTORIAL_FINISHED))
        {
            warningText.SetActive(true);
            GlobalDataHandler.SavePref(GlobalDataHandler.MAP_ACTIVE, false);
            switch ((int) Math.Round(Random.value % 3))
            {
                case 0:
                    GlobalDataHandler.SavePref(GlobalDataHandler.SIMON_BROKEN, true);
                    break;
                case 1:
                    GlobalDataHandler.SavePref(GlobalDataHandler.WIRES_BROKEN, true);
                    break;
                case 2:
                    GlobalDataHandler.SavePref(GlobalDataHandler.SWITCHES_BROKEN, true);
                    break;
            }
        }
    }

    private void SetTutorialDialogue()
    {
        if (!global.dictionary.ContainsKey(3))
        {
            TUTORIAL_DIALOGUE[0] = global.PlayerName + TUTORIAL_DIALOGUE[0];
            global.dictionary.Add(3, TUTORIAL_DIALOGUE);
        }
    }

    
    private void KillAssistant()
    {
        //Destroys assistant if player talked to him before or tutorial is finished
        Debug.Log("Killing the Assistant");
        Debug.Log(GlobalDataHandler.GetPref(GlobalDataHandler.MAP_TUTORIAL1 +", " + GlobalDataHandler.GetPref(GlobalDataHandler.MAP_TUTORIAL_FINISHED)));
        GlobalDataHandler.SavePref(GlobalDataHandler.MAP_ACTIVE, true);
        if (GlobalDataHandler.GetPref(GlobalDataHandler.MAP_TUTORIAL1) 
            || GlobalDataHandler.GetPref(GlobalDataHandler.MAP_TUTORIAL_FINISHED))
        {
            Destroy(assistant1);
            GlobalDataHandler.SavePref(GlobalDataHandler.MAP_ASSISTANT_ACTIVE, true);
            Debug.Log("Assistant Killed");
        }
    }

    private void SetMapActive()
    {
        if (!GlobalDataHandler.GetPref(GlobalDataHandler.MAP_ASSISTANT_ACTIVE) 
            && !GlobalDataHandler.GetPref(GlobalDataHandler.MAP_TUTORIAL_FINISHED) 
            && !GlobalDataHandler.GetPref(GlobalDataHandler.MAP_ACTIVE))
        {
            Debug.Log("Setting map active");
            GlobalDataHandler.SavePref(GlobalDataHandler.MAP_ACTIVE, true);
        }
    }

    private void Update()
    {
        ParseDifficulty();
    }
    
    private void Awake()
    {
        SetTutorialDialogue();
        HandleMinigameFails();

        //Resets the map when tutorial is finished.
        if (GlobalDataHandler.GetPref(GlobalDataHandler.MAP_TUTORIAL_FINISHED) 
            && GlobalDataHandler.GetPref(GlobalDataHandler.MAP_RESET))
        {
            GlobalDataHandler.SavePref(GlobalDataHandler.MAP_RESET, false);
            SetTutorialFinished();
        }
        else
        {
            if (mapData.firstStart)
            {
                GlobalDataHandler.SavePref(GlobalDataHandler.MAP_TUTORIAL1, false);
                GlobalDataHandler.SavePref(GlobalDataHandler.MAP_TUTORIAL2, false);
                GlobalDataHandler.SavePref(
                    GlobalDataHandler.MAP_ASSISTANT_ACTIVE,
                    !GlobalDataHandler.GetPref(GlobalDataHandler.MAP_TUTORIAL_FINISHED)
                    );
            }
        }

        KillAssistant();
        ConfirmAssistantIsDead();
        SetMapActive();
        flyButton.onClick.AddListener(OnButtonClick);
    }

    private void ConfirmAssistantIsDead()
    {
        if (assistant1 == null && GlobalDataHandler.GetPref(GlobalDataHandler.MAP_TUTORIAL1))
        {
            GlobalDataHandler.SavePref(
                GlobalDataHandler.MAP_TUTORIAL1,
                !GlobalDataHandler.GetPref(GlobalDataHandler.MAP_TUTORIAL_FINISHED)
                );
        }
    }

    private void OnButtonClick()
    {
        planet = GameObject.Find(mapData.playerPosition).GetComponent<Planet>();
        ParseDifficulty();
        if (!GlobalDataHandler.GetPref(GlobalDataHandler.MAP_TUTORIAL_FINISHED))
        {
            if (!GlobalDataHandler.GetPref(GlobalDataHandler.MAP_TUTORIAL1))
            {
                GlobalDataHandler.SavePref(GlobalDataHandler.MAP_TUTORIAL1, true);
                SetDifficulty(Difficulty.Easy);
                OpenAsteroids(State.Tutorial);
            }
            else
            {
                GlobalDataHandler.SavePref(GlobalDataHandler.HUB_TUTORIAL2, true);
                SetDifficulty(Difficulty.Easy);
                OpenFlappyShip(State.Tutorial);
            }
        }
        else
        {
            if (planet.getDifficulty() == 1)
            {
                RandomizeEvent(0.5f);
            }
            else if (planet.getDifficulty() == 2)
            {
                RandomizeEvent(0.75f);
            }
            else
            {
                RandomizeEvent(1f);
            }
        }
    }

    private void RandomizeEvent(float percentage)
    {
        if (Random.value < percentage)
        {
            if (Random.value < 0.5f)  OpenFlappyShip(State.Game);
            else OpenAsteroids(State.Game);
        }
        else
        {
            //Going to the dialogue when no event occured
            mapData.lastFlightType = "Safe";
            Debug.Log("No event occured");
        }
    }

    private void OpenAsteroids(State state)
    {
        Debug.Log("["+ state + "]: Asteroids Minigame occured");
        mapData.lastFlightType = "Asteroids";
        SceneDifficultyHandler.OpenAsteroids(difficulty);
    }

    private void OpenFlappyShip(State state)
    {
        Debug.Log("["+ state + "]: OpenFlappyShip Minigame occured");
        mapData.lastFlightType = "FlappyShip";
        SceneDifficultyHandler.OpenFlappyShip(difficulty);
    }

    private void SetTutorialFinished()
    {
        mapData.firstStart = true;
        mapData.lastFlightType = "";
        mapData.playerPosition = "Pstart";
        mapData.path = null;
        SceneManager.LoadScene("Assets/Scenes/ShipInterior/InteriorScene.unity");
    }

    private void ParseDifficulty()
    {
        planet = GameObject.Find(mapData.playerPosition).GetComponent<Planet>();
        difficulty = planet.getDifficulty() switch
        {
            2 => Difficulty.Medium,
            3 => Difficulty.Hard,
            _ => Difficulty.Easy
        };
    }

    private void SetDifficulty(Difficulty d)
    {
        difficulty = d;
    }
    
}
