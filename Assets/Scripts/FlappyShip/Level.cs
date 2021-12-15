using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private const float CAMERA_ORTHO_SIZE = 50f;
    private const float PIPE_WIDTH = 9f;
    private const float PIPE_HEAD_HEIGHT = 0.75f;
    private const float PIPE_MOVE_SPEED = 30f;
    private const float PIPE_DESTROY_X_POS = -100f;
    private const float PIPE_SPAWN_X_POS = 100f;
    private const float SHIP_X_POS = 0f;

    private static Level instance;

    public static Level GetInstance() {
        return instance;
    }

    private List<Pipe> pipeList;
    private int pipesPassedCount;
    private int pipeCounter;
    private float pipeSpawnTimer;
    private float pipeSpawnDelay;
    private float gapSize;
    
    private bool canType;
    private bool isMoveTriggered;

    private State state;
    private GameMode gameMode;
    private Difficulty inGameDifficulty;

    public enum Difficulty {
        Easy,
        Medium,
        Hard
    }

     public enum State {
        WaitingToStart,
        Playing,
        ShipDown
    }

    public enum GameMode {
        InGame,
        Arcade
    }

    private void SetCanType(bool value){
        if(gameMode == GameMode.InGame){
            canType = value;
        }
    }

    public bool CanType(){
        return canType;
    }

    private void Awake() {
        instance = this;
        gameMode = GameMode.InGame;
        canType = true;
        pipeList = new List<Pipe>();
        inGameDifficulty = Difficulty.Easy;
        SetDifficulty(inGameDifficulty); //pass this value through the player's choice
        SetMoveTrigger(false);
        state = State.WaitingToStart;
        pipesPassedCount = 0;
        pipeCounter = 0;
    }

    private void Start() {
        Ship.GetInstance().OnDeath += Ship_OnDeath;
        Ship.GetInstance().OnStartedPlaying += Level_OnStartedPlaying;
    }

    private void Level_OnStartedPlaying(object sender, System.EventArgs e){
        state = State.Playing;
        FlappyDataHandler.GetInstance().RegisterMeasureStart();
    }

    private void Ship_OnDeath(object sender, System.EventArgs e){
        if(!Ship.GetInstance().GetIsDead()){
            state = State.ShipDown;
            Ship.GetInstance().SetIsDead(true);
            FlappyDataHandler.GetInstance().SetIsFailed(true);
            FlappyDataHandler.GetInstance().RegisterMeasureEnd();
            SoundManager.PlaySound(GameAssets.GetInstance().deathSound);
        }
    }

    private void Update() {
        if (state == State.Playing) {
            HandlePipeMovement();
            HandlePipeSpawning();
        }
    }

    private void HandlePipeSpawning() {
        pipeSpawnTimer -= Time.deltaTime;
        if(pipeSpawnTimer < 0) {
            pipeSpawnTimer = pipeSpawnDelay;

            float heightEdgeLimit = 10f;
            float minHeight = gapSize / 2f + heightEdgeLimit;
            float totalHeight = CAMERA_ORTHO_SIZE * 2f;
            float maxHeight = totalHeight - gapSize / 2f - heightEdgeLimit;

            float height = UnityEngine.Random.Range(minHeight, maxHeight);

            if(pipeCounter < 10 || gameMode == GameMode.Arcade) {
                 CreateGapPipes(height, gapSize, PIPE_SPAWN_X_POS);
            }
        }
    }

    private void HandlePipeMovement() {
        for(int i = 0; i < pipeList.Count; i++) {
            Pipe p = pipeList[i];
            bool isRightOfShip = p.GetXPos() > SHIP_X_POS;
            p.Move();
            if(isRightOfShip && p.GetXPos() <= SHIP_X_POS){
                pipesPassedCount++;
                SoundManager.PlaySound(GameAssets.GetInstance().scoreSound);
                
                if(pipesPassedCount / 2 == 10 && gameMode == GameMode.InGame){
                    FlappyDataHandler.GetInstance().RegisterMeasureEnd();
                    SetMoveTrigger(true);
                    SetCanType(false);
                }
            }
            if (p.GetXPos() < PIPE_DESTROY_X_POS) {
                p.DestroyThis();
                pipeList.Remove(p);
                i--;
            }
        }
    }

    private void CreatePipe(float height, float xPosition, bool bottom) {
        Transform pipeHead = Instantiate(GameAssets.GetInstance().pfPipeHead);
        pipeHead.position = new Vector3(xPosition, -CAMERA_ORTHO_SIZE + height - (PIPE_HEAD_HEIGHT / 2f));
        
        pipeHead.position = new Vector3(
            xPosition,
            bottom ? -CAMERA_ORTHO_SIZE + height - (PIPE_HEAD_HEIGHT / 2f) : CAMERA_ORTHO_SIZE - height + (PIPE_HEAD_HEIGHT / 2f)
        );

        Transform pipeBody = Instantiate(GameAssets.GetInstance().pfPipeBody);
        float pipeBodyYPos;
        if(bottom){
            pipeBodyYPos = -CAMERA_ORTHO_SIZE;
        }else{
            pipeBodyYPos = CAMERA_ORTHO_SIZE;
            pipeBody.localScale = new Vector3(1, -1, 1);
        }
        pipeBody.position = new Vector3(xPosition, pipeBodyYPos);

        SpriteRenderer pipeBodySpriteRenderer = pipeBody.GetComponent<SpriteRenderer>();
        pipeBodySpriteRenderer.size = new Vector2(PIPE_WIDTH, height);

        BoxCollider2D pipeBodyBoxCollider = pipeBody.GetComponent<BoxCollider2D>();
        pipeBodyBoxCollider.size = new Vector2(PIPE_WIDTH, height);
        pipeBodyBoxCollider.offset = new Vector2(0f, height / 2f);

        pipeList.Add(new Pipe(pipeHead, pipeBody));
    }

    private void SetDifficulty(Difficulty difficulty) {
          switch(difficulty){
                case Difficulty.Easy: 
                    gapSize = 50f; 
                    pipeSpawnDelay = 1.7f;
                    break;
                case Difficulty.Medium: 
                    gapSize = 40f; 
                    pipeSpawnDelay = 1.5f;
                    break;
                case Difficulty.Hard: 
                    gapSize = 35f; 
                    pipeSpawnDelay = 1.35f;
                    break;
          }
    }

    private Difficulty GetDifficulty() {
          if(pipeCounter >= 30) return Difficulty.Hard;
          if(pipeCounter >= 20) return Difficulty.Medium;
          return Difficulty.Easy;
    }

    private void CreateGapPipes(float gapY, float gapSize, float xPosition) {
        CreatePipe(gapY - gapSize / 2f, xPosition, true);
        CreatePipe(CAMERA_ORTHO_SIZE * 2f - gapY - gapSize / 2f, xPosition, false);
        pipeCounter++;

        if(gameMode == GameMode.Arcade){
             SetDifficulty(GetDifficulty());
        }
    }

    private void SetMoveTrigger(bool value){
        isMoveTriggered = value;
    }

    public bool IsMoveTriggered(){
        return isMoveTriggered;
    }

    public int GetPipeCount() {
        return pipeCounter;
    }

    public int GetPipesPassedCount() {
        return pipesPassedCount / 2; //for score counting purposes
    }

    public State GetState() {
        return state;
    }

    public GameMode GetGameMode() {
        return gameMode;
    }

    /**
    * Represents a single Pipe position
    */
    private class Pipe {
        
        private Transform pipeHeadTransform;
        private Transform pipeBodyTransform;

        public Pipe(Transform pipeHeadTransform, Transform pipeBodyTransform){
            this.pipeHeadTransform = pipeHeadTransform;
            this.pipeBodyTransform = pipeBodyTransform;
        }

        public void Move() {
             pipeHeadTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime;
             pipeBodyTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime;
        }

        public float GetXPos() {
            return pipeHeadTransform.position.x;
        }

        public void DestroyThis() {
            Destroy(pipeHeadTransform.gameObject);
            Destroy(pipeBodyTransform.gameObject);
        }
    }

}