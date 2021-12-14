using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {

    [SerializeField]
    public AudioClip errorSound;

    private const string GRID_NAME = "ButtonGrid";
    private const int GRID_SIZE = 9;
    public const int startSequenceLength = 1;
    public const float SEQUENCE_DELAY = 0.5f;
    private const float SCENE_CHANGE_DELAY = 1.0f;

    public float buttonSequenceActiveDuration = 0.3f;
    public float buttonSequenceCooldownDuration = 0.1f;

    private int[] currentSequence;
    private int currentSequenceLength;
    private int playerClicks;
    private int nextIndexToCheck;

    [SerializeField]
    private GameButton[] buttons;

    [SerializeField]
    private GameLight[] lights;
  
    [SerializeField]
    private GameLight indicator;

    private int lightsOn;
    private int score;

    private GameButton nextButton;

    private PlayerHandler playerHandler;

    private Difficulty difficulty;
    private GameMode gameMode;

    private static GameBoard instance;

    public static GameBoard GetInstance(){
        return instance;
    }

    public enum State{
        Waiting,
        Playing,
        GameOver
    }

    private State state;

    void Awake(){
        instance = this;
        state = State.Waiting;
        difficulty = Difficulty.Medium; // let the player decide
        gameMode = GameMode.InGame;

        CheckDifficulty();
        playerHandler = GetComponent<PlayerHandler>();

        playerHandler.SetCanClick(false);
        playerHandler.SetCanType(true);

        buttons = GetComponentsInChildren<GameButton>();
        lights = GetComponentsInChildren<GameLight>();
        indicator.SetInactive();
    }

    private void CheckDifficulty(){
        switch(difficulty){
            case Difficulty.Easy:{
                buttonSequenceActiveDuration = 0.4f;
                buttonSequenceCooldownDuration = 0.2f;
                break;
            }
            case Difficulty.Medium:{
                buttonSequenceActiveDuration = 0.3f;
                buttonSequenceCooldownDuration = 0.1f;
                break;
            }
            case Difficulty.Hard:{
                buttonSequenceActiveDuration = 0.2f;
                buttonSequenceCooldownDuration = 0.05f;
                break;
            }
        }
    }

    private void GenerateSequence(){
        currentSequence = new int[currentSequenceLength];
        for (int i = 0; i < currentSequenceLength; i++){
           currentSequence[i] = Random.Range(0, buttons.Length);
        }
    }

    public void StartGame(){
        playerHandler.SetCanClick(true);
        playerHandler.SetCanType(false);
        //GameOverWindow.GetInstance().Hide();
        state = State.Playing;
        score = 0;
        currentSequenceLength = startSequenceLength;
        DeactivateLights();
        StartCoroutine(SequenceRoutine());
    }

    private IEnumerator SequenceRoutine(){
        indicator.SetWronglyActive();    
        yield return new WaitForSeconds(SEQUENCE_DELAY);
        playerHandler.SetCanClick(false);
        GenerateSequence();

        for (int i = 0; i < currentSequenceLength; i++){
            nextButton = buttons[currentSequence[i]];
            yield return StartCoroutine(nextButton.PlayBlinkRoutine(buttonSequenceActiveDuration, buttonSequenceCooldownDuration));
        }

        StartCoroutine(PlayerResponseRoutine());
    }

    private IEnumerator PlayerResponseRoutine(){
        nextIndexToCheck = 0;
        playerClicks = 0;
        playerHandler.SetCanClick(true);
        indicator.SetActive();

        if(gameMode == GameMode.InGame){
            DataHandler.GetInstance().RegisterMeasureStart();
        }
        
        while (playerClicks < currentSequenceLength){  yield return null; }

        ActivateNextLight();
        CheckLigtsInGame();
        playerHandler.SetCanClick(false);
        IncrementSequenceLength();
        IncrementScore();
        StartCoroutine(SequenceRoutine());
    }

    //private const string SCENE_NAME;

    private IEnumerator StartSceneChangeRoutine(){
        yield return new WaitForSeconds(SCENE_CHANGE_DELAY);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(SCENE_NAME); //set NEXT_SCENE const to be next scene
    }

    public void HandleClick(int buttonIndex){
        if(gameMode == GameMode.InGame && DataHandler.GetInstance().IsRegistering()) {
            DataHandler.GetInstance().RegisterClick();
        }

        IncrentPlayerClicks();

        if (currentSequence[nextIndexToCheck] == buttonIndex){
            nextIndexToCheck++;
        } else {
            SoundManager.PlaySound(errorSound);
            SetAllLightsRed();
            StopAllCoroutines();
            BlinkButtonsRed();
            playerHandler.SetCanType(true);
            playerHandler.SetCanClick(false);

            if(gameMode == GameMode.Arcade) {
                //GameOverWindow.GetInstance().Show();
            }
        }
    }

    private void BlinkButtonsRed(){
        foreach(GameButton gb in buttons){
            StartCoroutine(gb.PlayErrorRoutine());
        }
    }

    private void ActivateNextLight(){
        if(lightsOn < 5){
            lights[lightsOn].SetActive();
        }
        lightsOn++;
    }

    private void DeactivateLights(){
        foreach(GameLight g in lights){
            g.SetInactive();
        }
        lightsOn = 0;
    }

    private void SetAllLightsRed(){
        foreach(GameLight g in lights){
            g.SetWronglyActive();
        }
        lightsOn = 0;
    }

    private void IncrementScore(){
        score++;
    }

    private void IncrementSequenceLength(){
        currentSequenceLength++;
    }

    private void IncrentPlayerClicks(){
        playerClicks++;
    }

    private void Proceed(){
        switch(gameMode){
            case GameMode.Arcade:{
                //retry or exit
                break;
            }
            case GameMode.InGame:{
                Debug.Log("Should go forward");
                StopAllCoroutines();
                StartSceneChangeRoutine();
                break;
            }
        }
    }

    private void CheckLigtsInGame(){
        if(gameMode == GameMode.InGame && lightsOn == 5){
            Proceed();
        }
    }

    public State GetState(){
        return state;
    }

    public GameMode GetGameMode(){
        return gameMode;
    }

    public int GetScore(){
        return score;
    }

}