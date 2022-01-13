using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EventManager : MonoBehaviour
{
    public Button flyButton;
    public GameObject assistant1;
    public GameObject warningText;  
    
    private Planet planet;
    private Difficulty difficulty;
    private bool mapFirst = true;
    private enum State
    {
        Tutorial,
        Game
    }

    private void HandleMinigameFails()
    {
        if(GlobalData.GetVar("mapTutorialFinished", GlobalData.dialoguePath))
        {
            if (GlobalData.GetVar("minigameFailed", GlobalData.hubStats))
            {
                warningText.SetActive(true);
                GlobalData.SetVar("mapActive", false, GlobalData.hubStats);
                //GlobalData.SetVar("mapAssistantActive", true, GlobalData.dialoguePath);
                if (!GlobalData.tutorialRepairs)
                {
                    GlobalData.SetVar("simonBroken", true, GlobalData.hubStats);
                    GlobalData.SetVar("wiresBroken", true, GlobalData.hubStats);
                    GlobalData.SetVar("switchesBroken", true, GlobalData.hubStats);
                    GlobalData.tutorialRepairs = true;
                }
                else
                {
                    switch ((int)Math.Round(Random.value % 3))
                    {
                        case 0:
                            GlobalData.SetVar("simonBroken", true, GlobalData.hubStats);
                            break;
                        case 1:
                            GlobalData.SetVar("wiresBroken", true, GlobalData.hubStats);
                            break;
                        case 2:
                            GlobalData.SetVar("switchesBroken", true, GlobalData.hubStats);
                            break;
                    }
                }
            }
        }
    }

    private void SetTutorialDialogue()
    {
        if (!GlobalData.DIALOGUE_DICTIONARY.ContainsKey(1))
        {
            GlobalData.DIALOGUE_DICTIONARY.Add(1, new []{
            "Witaj w panelu kontroli lotu!",
            "Pozwól, że wytłumaczę Ci jak się nim posługiwać.",
            "Aby dostać się do celu naszej podróży, będziemy musieli odwiedzić aż 3 nieznane planety!",
            "Na całe szczęście, system kontroli lotu jest wyposażony w czujnik zagrożenia...",
            "Po najechaniu kursorem na planetę możemy sprawdzić trudność i ryzyko niefortunnych zdarzeń na danej trasie!",
            "Wyróżniamy 3 rodzaje tras...",
            "1.Trasy oznaczone kolorem zielonym, wybierając takie trasy mamy 50% szansy na uniknięcie przeszkód...",
            "2.Trasy oznaczone kolorem żółtym, wybierając takie trasy mamy 25% szansy na uniknięcie przeszkód...",
            "I wreszcie numer 3, Trasy oznaczone kolorem czerwonym. wybierając takie trasy nie obędzie się bez przeszkód.",
            "Nie zapominajmy również o tym, że nasze zasoby są ograniczone!",
            "Zapas żywności starczy nam na nie dłużej niż 7 dni",
            "Podróże kolejno zieloną, żółtą i czerwoną trasą zajmują po 3, 2, 1 dni...",
            "W razie gdyby brakowało nam zasobów za pomocą specjalnego skafandra będziemy mogli zejść na planetę i postarać się ich dozbierać.",
            "To jakimi trasami będziemy się poruszać zależy tylko i wyłącznie od Ciebie kapitanie!",
            "Sprawdźmy jak byś sobie poradził... Śmiało! Wybierz pierwszą trasę!"
            });
        }
    }

    
    private void KillAssistant()
    {
        //Destroys assistant if player talked to him before or tutorial is finished
        if (GlobalData.GetVar("mapTutorial1", GlobalData.dialoguePath) 
            || GlobalData.GetVar("mapTutorialFinished", GlobalData.dialoguePath))
        {
            Destroy(assistant1);
            GlobalData.SetVar("mapAssistantActive", false, GlobalData.dialoguePath);
        }
    }

    private void SetMapActive()
    {
        //Sets map active when assistant is killed for the first time:
        if (assistant1 == null && mapFirst)
        {
            GlobalData.SetVar("mapActive", true, GlobalData.hubStats);
            mapFirst = false;
        }
        //Sets map inactive when minigame was failed
        else if (warningText.active && GlobalData.GetVar(GlobalData.MAP_ACTIVE, GlobalData.hubStats)) {
            GlobalData.SetVar("mapActive", false, GlobalData.hubStats);
        }
    }

    private void Update()
    {
        ParseDifficulty();
        SetMapActive();
    }
    
    private void Awake()
    {
        SetTutorialDialogue();
        HandleMinigameFails();

        //Resets the map when tutorial is finished.
        if (GlobalData.GetVar("mapTutorialFinished", GlobalData.dialoguePath) 
            && GlobalData.GetVar("mapReset", GlobalData.dialoguePath))
        {
            GlobalData.SetVar("mapReset", false, GlobalData.dialoguePath);
            SetTutorialFinished();
        }
        else
        {
            if (GlobalData.firstStart)
            {
                GlobalData.SetVar("mapTutorial1", false, GlobalData.dialoguePath);
                GlobalData.SetVar("mapTutorial2", false, GlobalData.dialoguePath);
                if (!GlobalData.GetVar("mapTutorialFinished", GlobalData.dialoguePath))
                {
                    GlobalData.SetVar("mapAssistantActive", true, GlobalData.dialoguePath);
                }
                else
                {
                    GlobalData.SetVar("mapAssistantActive", false, GlobalData.dialoguePath);
                }
            }
        }

        KillAssistant();
        ConfirmAssistantIsDead();
        //flyButton.onClick.AddListener(onButtonClick);
    }

    private void ConfirmAssistantIsDead()
    {
        if (assistant1 == null && GlobalData.GetVar("mapTutorial1", GlobalData.dialoguePath)) {
            if (GlobalData.GetVar("mapTutorialFinished", GlobalData.dialoguePath))
            {
                GlobalData.SetVar("mapTutorial1", false, GlobalData.dialoguePath);
            }
            else
            {
                GlobalData.SetVar("mapTutorial1", true, GlobalData.dialoguePath);
            }
        }
    }

    public void onButtonClick()
    {
        planet = GameObject.Find(GlobalData.playerPosition).GetComponent<Planet>();
        ParseDifficulty();
        if (!GlobalData.GetVar("mapTutorialFinished", GlobalData.dialoguePath))
        {
            if (!GlobalData.GetVar("mapTutorial1", GlobalData.dialoguePath))
            {
                GlobalData.SetVar("mapTutorial1", true, GlobalData.dialoguePath);
                SetDifficulty(Difficulty.Easy);
                OpenAsteroids(State.Tutorial);
            }
            else
            {
                GlobalData.SetVar("hubTutorial2", true, GlobalData.dialoguePath);
                SetDifficulty(Difficulty.Easy);
                OpenFlappyShip(State.Tutorial);
            }
        }
        else
        {
            if (planet.getDifficulty() == 1)
            {
                RandomizeEvent(0.5f);
                //RandomizeEvent(0f);
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
            GlobalData.lastFlightType = "Safe";
            SceneManager.LoadScene("Assets/Scenes/ShipInterior/InteriorScene.unity");
            Debug.Log("No event occured");
        }
    }

    private void OpenAsteroids(State state)
    {
        Debug.Log("["+ state + "]: Asteroids Minigame occured");
        GlobalData.lastFlightType = "Asteroids";
        SceneDifficultyHandler.OpenAsteroids(difficulty);
    }

    private void OpenFlappyShip(State state)
    {
        Debug.Log("["+ state + "]: OpenFlappyShip Minigame occured");
        GlobalData.lastFlightType = "FlappyShip";
        SceneDifficultyHandler.OpenFlappyShip(difficulty);
    }

    private void SetTutorialFinished()
    {
        GlobalData.firstStart = true;
        GlobalData.lastFlightType = "";
        GlobalData.playerPosition = "Pstart";
        GlobalData.path = null;
    }

    private void ParseDifficulty()
    {
        planet = GameObject.Find(GlobalData.playerPosition).GetComponent<Planet>();
        switch (planet.getDifficulty())
        {
            default: difficulty = Difficulty.Easy; break;
            case 2: difficulty = Difficulty.Medium; break;
            case 3: difficulty = Difficulty.Hard; break;
        }
    }

    private void SetDifficulty(Difficulty d)
    {
        difficulty = d;
    }
    
}
