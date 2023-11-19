using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    GameStateMachine gameStateMachine;

    public bool isLookingRight = true;
    void Start()
    {
        gameStateMachine = GameObject.Find("Game State Machine").GetComponent<GameStateMachine>();
    }

    void Update()
    {
        
    }
}
