using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    private float shipTakeOffVelocity;

    private bool isDead;
 
    private const float JUMP_AMOUNT = 100f;

    private static Ship instance;

    public static Ship GetInstance() {
        return instance;
    }

    public event EventHandler OnDeath;
    public event EventHandler OnStartedPlaying;

    private Rigidbody2D shipRigidbody2D;

    private State state;

    private enum State {
        WaitingToStart,
        Flying,
        Down,
        Finished
    }

    private void Awake() {
        isDead = false;
        instance = this;
        shipRigidbody2D = GetComponent<Rigidbody2D>();
        shipRigidbody2D.bodyType = RigidbodyType2D.Static;
        state = State.WaitingToStart;
        shipTakeOffVelocity = 0.0f;
    }

    private void Update() {
        switch (state) {
            default:
            case State.WaitingToStart:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) {
                    state = State.Flying;
                    shipRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    Jump();
                    if (OnStartedPlaying != null) {
                        OnStartedPlaying(this, EventArgs.Empty);
                    }
                }
                break;
            case State.Flying:
                if (Level.GetInstance().IsMoveTriggered() && shipRigidbody2D.transform.position.y != 0f){
                    MoveToCenter();
                }
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) {
                    if(!isDead || Level.GetInstance().CanType()){
                        Jump();
                    }
                }
                if (shipRigidbody2D.transform.position.y < -40){
                    if (OnDeath != null) {
                        OnDeath(this, EventArgs.Empty);
                    }
                }
                break;
            case State.Finished:  
                StartCoroutine(LaunchAwayWait());
                break;
        }
    }

    private void TakeOff(){
        shipRigidbody2D.transform.position = new Vector3(shipRigidbody2D.transform.position.x + shipTakeOffVelocity, 0);
        shipTakeOffVelocity += 0.05f;
    }

    public void MoveToCenter(){
        shipRigidbody2D.bodyType = RigidbodyType2D.Static;
        if(shipRigidbody2D.transform.position.y < 0){
            shipRigidbody2D.transform.position = new Vector3(0, shipRigidbody2D.transform.position.y + 0.1f);
        }

        if(shipRigidbody2D.transform.position.y > 0) {
            shipRigidbody2D.transform.position = new Vector3(0, shipRigidbody2D.transform.position.y - 0.1f);
        }

        if(shipRigidbody2D.transform.position.y > -1f && shipRigidbody2D.transform.position.y < 1f){
            shipRigidbody2D.transform.position = new Vector3(0, 0);
            state = State.Finished;
            SoundManager.PlaySound(GameAssets.GetInstance().takeOffSound);
        }
    }

    private IEnumerator LaunchAwayWait(){
         yield return new WaitForSeconds(1f); 
         TakeOff();
         SceneShader.GetInstance().SetIsShading(true);
         yield return new WaitForSeconds(1f);
         //change scene here
    }

    private void Jump() {
        FlappyDataHandler.GetInstance().RegisterJump();
        shipRigidbody2D.velocity = Vector2.up * JUMP_AMOUNT;
        SoundManager.PlaySound(GameAssets.GetInstance().jumpSound);
        CheckHeight();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (OnDeath != null) {
            OnDeath(this, EventArgs.Empty);
        }
    }

    private void CheckHeight(){
        if(shipRigidbody2D.transform.position.y > 40){
            if (OnDeath != null) {
                OnDeath(this, EventArgs.Empty);
            }
        }
    }

    public bool GetIsDead(){
        return isDead;
    }

    public void SetIsDead(bool isDead){
        this.isDead = isDead;
    }

}