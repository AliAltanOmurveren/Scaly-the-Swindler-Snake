using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour, IGameState
{

    GameStateMachine gameStateMachine;
    public Transform customerSpawn;
    public Transform customerTargetPosition;
    GameObject customer;

    public void Enter()
    {
        customer = GameObject.Find("Customer");
        gameStateMachine = GameObject.Find("Game State Machine").GetComponent<GameStateMachine>();
    }

    public void Exit()
    {
        
    }

    void IGameState.Update()
    {
        
    }

    IEnumerator MoveCustomerX(float seconds){
        float t = 0;

        while(t < 1){
            t += Time.deltaTime / seconds;

            Vector3 lerpedPosition = Vector3.Lerp(customerTargetPosition.position, customerSpawn.position, t);

            customer.transform.position = new Vector3(lerpedPosition.x, customer.transform.position.y, customer.transform.position.z);

            yield return null;
        }
    }
}
