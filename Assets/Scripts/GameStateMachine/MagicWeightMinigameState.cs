using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWeightMinigameState : MonoBehaviour, IGameState
{
    GameStateMachine gameStateMachine;
    public GameObject timeBar;

    private void Start() {
        gameStateMachine = gameObject.GetComponent<GameStateMachine>();
    }

    public void Enter()
    {
        Debug.Log("MagicState enter");

        timeBar.GetComponent<LeftTimeBar>().StartTimer(5);
    }

    public void Exit()
    {
        Debug.Log("Magicstate exit");
    }

    void IGameState.Update()
    {
        
    }
}
