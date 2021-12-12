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

    private State state;

    public enum Difficulty {
        Easy,
        Medium,
        Hard,
        Possiblent
    }

     public enum State {
        WaitingToStart,
        Playing,
        ShipDown
    }

    private void Awake() {
        instance = this;
        pipeList = new List<Pipe>();
        SetDifficulty(Difficulty.Easy);
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
    }

    private void Ship_OnDeath(object sender, System.EventArgs e){
        state = State.ShipDown;
        SoundManager.PlaySound(SoundManager.Sound.Death);
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
            CreateGapPipes(height, gapSize, PIPE_SPAWN_X_POS);
        }
    }

    private void HandlePipeMovement() {
        for(int i = 0; i < pipeList.Count; i++) {
            Pipe p = pipeList[i];
            bool isRightOfShip = p.GetXPos() > SHIP_X_POS;
            p.Move();
            if(isRightOfShip && p.GetXPos() <= SHIP_X_POS){
                pipesPassedCount++;
                SoundManager.PlaySound(SoundManager.Sound.Score);
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
                    gapSize = 33f; 
                    pipeSpawnDelay = 1.2f;
                    break;
                case Difficulty.Possiblent:
                    gapSize = 27f;
                    pipeSpawnDelay = 1f;
                    break;
          }
    }

    private Difficulty GetDifficulty() {
          if(pipeCounter >= 30) return Difficulty.Possiblent;
          if(pipeCounter >= 20) return Difficulty.Hard;
          if(pipeCounter >= 10) return Difficulty.Medium;
          return Difficulty.Easy;
    }

    private void CreateGapPipes(float gapY, float gapSize, float xPosition) {
        CreatePipe(gapY - gapSize / 2f, xPosition, true);
        CreatePipe(CAMERA_ORTHO_SIZE * 2f - gapY - gapSize / 2f, xPosition, false);
        pipeCounter++;
        SetDifficulty(GetDifficulty());
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
