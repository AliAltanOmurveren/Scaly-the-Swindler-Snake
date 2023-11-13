using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWeightMinigameState : MonoBehaviour, IGameState
{
    GameStateMachine gameStateMachine;

    private void Start() {
        gameStateMachine = gameObject.GetComponent<GameStateMachine>();
    }

    public void Enter()
    {
        Debug.Log("MagicState enter");
    }

    public void Exit()
    {
        Debug.Log("Magicstate exit");
    }

    void IGameState.Update()
    {
        Debug.Log("State is MagicState");
    }
}
