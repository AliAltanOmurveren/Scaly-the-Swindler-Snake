using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWeight : MonoBehaviour
{
    GameStateMachine gameStateMachine;
    public bool isFalling;
    public bool isTouchingTray;
    public LeftArm leftArm;
    public int weight;

    private void Start() {
        gameStateMachine = GameObject.Find("Game State Machine").GetComponent<GameStateMachine>();
        leftArm = GameObject.Find("scale_left_tray").GetComponent<LeftArm>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Arm")){
            isFalling = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Arm") && (object)gameStateMachine.currentGameState == gameStateMachine.magicWeightMinigameState){
            leftArm.totalWeight += weight;
            isTouchingTray = true;
            gameStateMachine.TransitionTo(gameStateMachine.weighingMinigameState);
        }
    }

    private void OnDestroy() {
        if(isTouchingTray){
            leftArm.totalWeight -= weight;
        }
    }
}
