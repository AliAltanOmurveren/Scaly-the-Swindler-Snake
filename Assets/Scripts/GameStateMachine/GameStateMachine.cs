using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState {
    void Enter();
    void Update();
    void Exit();
}

public class GameStateMachine: MonoBehaviour
{
    IGameState currentGameState;

    public CustomerEntranceState customerEntranceState;
    public MagicWeightMinigameState magicWeightMinigameState;
    public WeighingMinigameState weighingMinigameState;

    private void Start() {
        customerEntranceState = gameObject.AddComponent<CustomerEntranceState>();
        magicWeightMinigameState = gameObject.AddComponent<MagicWeightMinigameState>();

        Initialize(customerEntranceState);    
    }

    void Initialize(IGameState gameState) {
        currentGameState = gameState;
        gameState.Enter();
    }

    public void TransitionTo(IGameState nextGameState){
        currentGameState.Exit();

        currentGameState = nextGameState;

        nextGameState.Enter();
    }

    private void Update() {
        if(currentGameState != null) {
            currentGameState.Update();
        }
    }
}
