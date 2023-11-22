using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeighingMinigameState : MonoBehaviour, IGameState
{

    GameStateMachine gameStateMachine;

    public RightTimeBar rightTimeBar;

    float gameDuration = 10;

    public ScaleRotation scaleRotation;

    bool failTransitionOnce;

    public void Enter()
    {
        failTransitionOnce = true;

        gameStateMachine = GameObject.Find("Game State Machine").GetComponent<GameStateMachine>();
        Debug.Log("weighing minigame enter");

        rightTimeBar.StartTimer(gameDuration);
    }

    public void Exit()
    {
        rightTimeBar.ResetTimer();
        GameObject magicWeight = GameObject.Find("Magic Weight");
        
        if(magicWeight != null){
            Destroy(magicWeight);
        }
    }

    public void Update()
    {
        if(!scaleRotation.scaleTopIsRotating && 
            scaleRotation.transform.localRotation.eulerAngles.z == 0 && 
            !rightTimeBar.timeExpired){
            Debug.Log("Win");
        }

        if(rightTimeBar.timeExpired && scaleRotation.weightAngle != 0 && failTransitionOnce){
            failTransitionOnce = false;
            gameStateMachine.TransitionTo(gameStateMachine.failState);
        }
    }


}
