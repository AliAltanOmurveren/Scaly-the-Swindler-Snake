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
    bool winTransitionOnce;

    public void Enter()
    {
        failTransitionOnce = true;
        winTransitionOnce = true;

        gameStateMachine = GameObject.Find("Game State Machine").GetComponent<GameStateMachine>();

        rightTimeBar.StartTimer(gameDuration);
    }

    public void Exit()
    {
        rightTimeBar.ResetTimer();
        
    }

    void IGameState.Update()
    {
        if(!scaleRotation.scaleTopIsRotating && 
            scaleRotation.transform.localRotation.eulerAngles.z == 0 && 
            !rightTimeBar.timeExpired &&
            winTransitionOnce)
        {
            winTransitionOnce = false;
            
            gameStateMachine.TransitionTo(gameStateMachine.winState);
        }

        if(rightTimeBar.timeExpired && scaleRotation.weightAngle != 0 && failTransitionOnce){
            failTransitionOnce = false;
            gameStateMachine.TransitionTo(gameStateMachine.failState);
        }
    }


}
