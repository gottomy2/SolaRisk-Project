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
                if (!GlobalData.tutorialRepairs)
                {
                    GlobalData.SetVar("simonBroken", true, GlobalData.hubStats);
                    GlobalData.SetVar("wiresBroken", true, GlobalData.hubStats);
                    GlobalData.SetVar("switchesBroken", true, GlobalData.hubStats);
                    GlobalData.SetVar("repairsTutorialActive", true, GlobalData.dialoguePath);
                    GlobalData.tutorialRepairs = true;
                }
                else
                {
                    switch (Random.Range(1,4))
                    {
                        case 1:
                            GlobalData.SetVar("simonBroken", true, GlobalData.hubStats);
                            break;
                        case 2:
                            GlobalData.SetVar("wiresBroken", true, GlobalData.hubStats);
                            break;
                        case 3:
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
            "Pozw??l, ??e wyt??umacz?? Ci jak si?? nim pos??ugiwa??.",
            "Aby dosta?? si?? do celu naszej podr????y, b??dziemy musieli odwiedzi?? a?? 3 nieznane planety!",
            "Na ca??e szcz????cie, system kontroli lotu jest wyposa??ony w czujnik zagro??enia...",
            "Po najechaniu kursorem na planet?? mo??emy sprawdzi?? trudno???? i ryzyko niefortunnych zdarze?? na danej trasie!",
            "Wyr????niamy 3 rodzaje tras...",
            "1.Trasy oznaczone kolorem zielonym, wybieraj??c takie trasy mamy 50% szansy na unikni??cie przeszk??d...",
            "2.Trasy oznaczone kolorem ??????tym, wybieraj??c takie trasy mamy 25% szansy na unikni??cie przeszk??d...",
            "I wreszcie numer 3, Trasy oznaczone kolorem czerwonym. wybieraj??c takie trasy nie ob??dzie si?? bez przeszk??d.",
            "Nie zapominajmy r??wnie?? o tym, ??e nasze zasoby s?? ograniczone!",
            "Zapas ??ywno??ci starczy nam na nie d??u??ej ni?? 7 dni",
            "Podr????e kolejno zielon??, ??????t?? i czerwon?? tras?? zajmuj?? po 3, 2, 1 dni...",
            "W razie gdyby brakowa??o nam zasob??w za pomoc?? specjalnego skafandra b??dziemy mogli zej???? na planet?? i postara?? si?? ich dozbiera??.",
            "To jakimi trasami b??dziemy si?? porusza?? zale??y tylko i wy????cznie od Ciebie kapitanie, jednak powinnismy dosta?? si?? do celu naszej podr????y jak najszybciej to mo??liwe.",
            "Sprawd??my jak by?? sobie poradzi??... ??mia??o! Wybierz pierwsz?? tras??!"
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
        SceneManager.LoadScene("Assets/Scenes/ShipInterior/InteriorScene.unity");
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
