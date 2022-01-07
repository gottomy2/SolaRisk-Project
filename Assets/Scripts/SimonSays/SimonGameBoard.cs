using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class SimonGameBoard : MonoBehaviour
{
    [SerializeField] public AudioClip errorSound;

    private const string GRID_NAME = "ButtonGrid";
    private const int GRID_SIZE = 9;
    public const int startSequenceLength = 1;
    public const float SEQUENCE_DELAY = 0.5f;
    private const float SCENE_CHANGE_DELAY = 1.0f;

    public float buttonSequenceActiveDuration = 0.3f;
    public float buttonSequenceCooldownDuration = 0.1f;
    public GlobalVars global;

    private int[] currentSequence;
    private int currentSequenceLength;
    private int playerClicks;
    private int nextIndexToCheck;

    private GameObject startText;

    [SerializeField] private GameButton[] buttons;

    [SerializeField] private GameLight[] lights;

    [SerializeField] private GameLight indicator;

    private int lightsOn;
    private int score;

    private bool isCoroutinesStopping;

    private GameButton nextButton;

    private PlayerHandler playerHandler;

    private static Difficulty difficulty;
    private GameMode gameMode;

    private static SimonGameBoard instance;

    public static SimonGameBoard GetInstance()
    {
        return instance;
    }

    public enum State
    {
        Waiting,
        Playing
    }

    private State state;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        instance = this;
        state = State.Waiting;
        gameMode = GameMode.InGame;

        isCoroutinesStopping = false;

        startText = GameObject.Find("StartText");
        startText.SetActive(false);

        CheckDifficulty();
        playerHandler = GetComponent<PlayerHandler>();

        playerHandler.SetCanClick(false);
        playerHandler.SetCanType(true);

        buttons = GetComponentsInChildren<GameButton>();
        lights = GetComponentsInChildren<GameLight>();
        indicator.SetInactive();
    }

    private void CheckDifficulty()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
            {
                buttonSequenceActiveDuration = 0.4f;
                buttonSequenceCooldownDuration = 0.2f;
                break;
            }
            case Difficulty.Medium:
            {
                buttonSequenceActiveDuration = 0.3f;
                buttonSequenceCooldownDuration = 0.1f;
                break;
            }
            case Difficulty.Hard:
            {
                buttonSequenceActiveDuration = 0.2f;
                buttonSequenceCooldownDuration = 0.05f;
                break;
            }
        }
        
        Debug.Log("Difficulty of Simon: " + difficulty);
    }

    private void GenerateSequence()
    {
        currentSequence = new int[currentSequenceLength];
        for (int i = 0; i < currentSequenceLength; i++)
        {
            currentSequence[i] = Random.Range(0, buttons.Length);
        }
    }

    private void Start()
    {
        SceneShader.GetInstance().SetIsLighting(true);
    }

    public void StartGame()
    {
        playerHandler.SetCanClick(true);
        playerHandler.SetCanType(false);
        state = State.Playing;
        score = 0;
        currentSequenceLength = startSequenceLength;
        DeactivateLights();
        StartCoroutine(SequenceRoutine());
    }

    private void Update()
    {
        startText.SetActive(state == State.Waiting);
    }

    private IEnumerator SequenceRoutine()
    {
        indicator.SetWronglyActive();
        yield return new WaitForSeconds(SEQUENCE_DELAY);
        playerHandler.SetCanClick(false);
        GenerateSequence();

        for (int i = 0; i < currentSequenceLength; i++)
        {
            if (isCoroutinesStopping) yield break;

            nextButton = buttons[currentSequence[i]];
            yield return StartCoroutine(nextButton.PlayBlinkRoutine(buttonSequenceActiveDuration,
                buttonSequenceCooldownDuration));
        }

        StartCoroutine(PlayerResponseRoutine());
    }

    private IEnumerator PlayerResponseRoutine()
    {
        nextIndexToCheck = 0;
        playerClicks = 0;
        playerHandler.SetCanClick(true);
        indicator.SetActive();

        if (gameMode == GameMode.InGame)
        {
            SimonDataHandler.GetInstance().RegisterMeasureStart();
        }

        while (playerClicks < currentSequenceLength)
        {
            yield return null;
        }

        ActivateNextLight();
        CheckLightsInGame();
        playerHandler.SetCanClick(false);
        IncrementSequenceLength();
        IncrementScore();
        StartCoroutine(SequenceRoutine());
    }

    private IEnumerator StartSceneChangeRoutine()
    {
        yield return new WaitForSeconds(SCENE_CHANGE_DELAY);
        SceneShader.GetInstance().SetIsShading(true);
        yield return new WaitForSeconds(SCENE_CHANGE_DELAY);
        //Change scene here
        SceneManager.LoadScene("Assets/Scenes/ShipInterior/InteriorScene.unity");
    }

    public void HandleClick(int buttonIndex)
    {
        if (gameMode == GameMode.InGame && SimonDataHandler.GetInstance().IsRegistering())
        {
            SimonDataHandler.GetInstance().RegisterClick();
        }

        IncrementPlayerClicks();

        if (currentSequence[nextIndexToCheck] == buttonIndex)
        {
            nextIndexToCheck++;
        }
        else
        {
            SimonDataHandler.GetInstance().SetIsFailed(true);
            SimonDataHandler.GetInstance().Finish();
            SoundManager.PlaySound(errorSound);
            SetAllLightsRed();
            StopAllCoroutines();
            BlinkButtonsRed();
            playerHandler.SetCanType(true);
            playerHandler.SetCanClick(false);
            GlobalDataHandler.SavePref(GlobalDataHandler.SIMON_FIX, true);
            Proceed();
        }
    }

    private void BlinkButtonsRed()
    {
        foreach (GameButton gb in buttons)
        {
            StartCoroutine(gb.PlayErrorRoutine());
        }
    }

    private void ActivateNextLight()
    {
        if (lightsOn < 5)
        {
            lights[lightsOn].SetActive();
        }

        lightsOn++;
    }

    private void DeactivateLights()
    {
        foreach (GameLight g in lights)
        {
            g.SetInactive();
        }

        lightsOn = 0;
    }

    private void SetAllLightsRed()
    {
        foreach (GameLight g in lights)
        {
            g.SetWronglyActive();
        }

        lightsOn = 0;
    }

    private void IncrementScore()
    {
        score++;
    }

    private void IncrementSequenceLength()
    {
        currentSequenceLength++;
    }

    private void IncrementPlayerClicks()
    {
        playerClicks++;
    }

    private void Proceed()
    {
        switch (gameMode)
        {
            case GameMode.Arcade:
            {
                //retry or exit
                break;
            }
            case GameMode.InGame:
            {
                isCoroutinesStopping = true;
                StopAllCoroutines();
                StartCoroutine(StartSceneChangeRoutine());
                break;
            }
        }
    }

    private void CheckLightsInGame()
    {
        if (gameMode == GameMode.InGame && lightsOn == 5)
        {
            SimonDataHandler.GetInstance().Finish();
            GlobalDataHandler.SavePref(GlobalDataHandler.SIMON_BROKEN, false);
            Proceed();
        }
    }

    public State GetState()
    {
        return state;
    }

    public GameMode GetGameMode()
    {
        return gameMode;
    }

    public int GetScore()
    {
        return score;
    }

    public static void SetDifficulty(Difficulty d)
    {
        difficulty = d;
    }
}