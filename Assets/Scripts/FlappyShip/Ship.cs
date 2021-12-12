using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
 
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
        Down
    }

    private void Awake() {
        instance = this;
        shipRigidbody2D = GetComponent<Rigidbody2D>();
        shipRigidbody2D.bodyType = RigidbodyType2D.Static;
        state = State.WaitingToStart;
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
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) {
                    Jump();
                }
                break;
            case State.Down:
                break;
        }
    }

    private void Jump() {
        shipRigidbody2D.velocity = Vector2.up * JUMP_AMOUNT;
        SoundManager.PlaySound(SoundManager.Sound.Jump);
        checkHeight();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        shipRigidbody2D.bodyType = RigidbodyType2D.Static;
        if (OnDeath != null) {
            OnDeath(this, EventArgs.Empty);
        }
    }

    private void checkHeight(){
        if(shipRigidbody2D.transform.position.y > 40){
            //shipRigidbody2D.bodyType = RigidbodyType2D.Static;
            //Death from jumping too high depending on the Difficulty level? 
            if (OnDeath != null) {
                OnDeath(this, EventArgs.Empty);
            }
        }
    }
}
