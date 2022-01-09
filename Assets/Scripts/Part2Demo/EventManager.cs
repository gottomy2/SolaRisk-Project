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
        if (GlobalData.GetVar("minigameFailed", GlobalData.hubStats) 
            && GlobalData.GetVar("mapTutorialFinished", GlobalData.dialoguePath))
        {
            warningText.SetActive(true);
            GlobalData.SetVar("mapActive", false, GlobalData.hubStats);
            //GlobalData.SetVar("mapAssistantActive", true, GlobalData.dialoguePath);
            switch ((int) Math.Round(Random.value % 3))
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

    private void SetTutorialDialogue()
    {
        if (!GlobalData.DIALOGUE_DICTIONARY.ContainsKey(3))
        {
            TUTORIAL_DIALOGUE[0] = GlobalData.playerName + TUTORIAL_DIALOGUE[0];
            GlobalData.DIALOGUE_DICTIONARY.Add(3, TUTORIAL_DIALOGUE);
        }
    }

    
    private void KillAssistant()
    {
        //Destroys assistant if player talked to him before or tutorial is finished
        GlobalData.SetVar("mapActive", true, GlobalData.hubStats);
        if (GlobalData.GetVar("mapTutorial1", GlobalData.dialoguePath) 
            || GlobalData.GetVar("mapTutorialFinished", GlobalData.dialoguePath))
        {
            Destroy(assistant1);
            GlobalData.SetVar("mapAssistantActive", false, GlobalData.dialoguePath);
        }
    }

    private void SetMapActive()
    {
        if (!GlobalData.GetVar("mapAssistantActive", GlobalData.dialoguePath) 
            && !GlobalData.GetVar("mapTutorialFinished", GlobalData.dialoguePath) 
            && !GlobalData.GetVar("mapActive", GlobalData.dialoguePath))
        {
            Debug.Log("Setting map active");
            GlobalData.SetVar("mapActive", true, GlobalData.hubStats);
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
        if (GlobalData.GetVar("mapTutorialFinished", GlobalData.dialoguePath) 
            && GlobalData.GetVar("mapReset", GlobalData.dialoguePath))
        {
            GlobalData.SetVar("mapReset", false, GlobalData.dialoguePath);
            SetTutorialFinished();
        }
        else
        {
            if (mapData.firstStart)
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
        SetMapActive();
        flyButton.onClick.AddListener(onButtonClick);
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

    private void onButtonClick()
    {
        planet = GameObject.Find(mapData.playerPosition).GetComponent<Planet>();
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
