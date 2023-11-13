using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEntranceState : MonoBehaviour, IGameState
{
    GameStateMachine gameStateMachine;

    private void Start() {
        gameStateMachine = gameObject.GetComponent<GameStateMachine>();
    }

    public void Enter()
    {
        Debug.Log("CustomerEntrance enter");
        StartCoroutine(Wait5Min());
    }

    public void Exit()
    {
        Debug.Log("CustomerEntrance exit");
    }

    void IGameState.Update()
    {
        Debug.Log("State is CustomerEntrance");
    }

    IEnumerator Wait5Min() {
        yield return new WaitForSeconds(5);

        gameStateMachine.TransitionTo(gameStateMachine.magicWeightMinigameState);
    }
}
