using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour, IGameState
{

    GameStateMachine gameStateMachine;
    public RightArm rightArm;
    public Transform customerSpawn;
    public Transform customerTargetPosition;
    GameObject customer;
    GameObject product;
    GameObject magicWeight;

    public void Enter()
    {
        customer = GameObject.Find("Customer");
        gameStateMachine = GameObject.Find("Game State Machine").GetComponent<GameStateMachine>();
        product = GameObject.Find("Product");
        magicWeight = GameObject.Find("Magic Weight");

        int coinsToAdd = 0;

        if(magicWeight != null){
            coinsToAdd += magicWeight.GetComponent<MagicWeight>().weight * 2;
        }

        coinsToAdd += rightArm.totalWeightContributers.Count * 3;

        coinsToAdd += GameManager.trayBonus;

        GameManager.coins += coinsToAdd;

        StartCoroutine(MoveCustomerBack(3));

        GameObject[] weightsToDestroy = GameObject.FindGameObjectsWithTag("Weight");

        if(weightsToDestroy != null){
            foreach (GameObject weight in weightsToDestroy){
                Destroy(weight);
            }

            rightArm.totalWeightContributers = new List<GameObject>();
            rightArm.bottomTouchingWeights = new List<GameObject>();
        }

        if(product != null){
            Destroy(product);
        }

        if(magicWeight != null){
            Destroy(magicWeight);
        }
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }

    IEnumerator MoveCustomerBack(float seconds){
        float t = 0;

        customer.GetComponent<SpriteRenderer>().flipX = true;

        while(t < 1){
            t += Time.deltaTime / seconds;

            Vector3 lerpedPosition = Vector3.Lerp(customerTargetPosition.position, customerSpawn.position, t);

            customer.transform.position = new Vector3(lerpedPosition.x, customer.transform.position.y, customer.transform.position.z);

            yield return null;
        }

        gameStateMachine.TransitionTo(gameStateMachine.customerEntranceState);
    }
}
