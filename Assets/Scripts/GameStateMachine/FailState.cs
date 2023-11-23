using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailState : MonoBehaviour, IGameState
{
    GameObject customer;

    public RightArm rightArm;

    GameStateMachine gameStateMachine;

    CustomerSpriteManager customerSpriteManager;

    public Transform customerSpawn;
    public Transform customerTargetPosition;
    public void Enter()
    {
        GameObject[] weightsToDestroy = GameObject.FindGameObjectsWithTag("Weight");

        if(weightsToDestroy != null){
            foreach (GameObject weight in weightsToDestroy){
                Destroy(weight);
            }

            rightArm.totalWeightContributers = new List<GameObject>();
            rightArm.bottomTouchingWeights = new List<GameObject>();
        }

        gameStateMachine = GameObject.Find("Game State Machine").GetComponent<GameStateMachine>();

        customer = GameObject.Find("Customer");
        customerSpriteManager = customer.GetComponent<CustomerSpriteManager>();

        customerSpriteManager.ChangeSpriteToAngry(customerSpriteManager.currentIndex);

        StartCoroutine(FailRoutine());
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

        customer.transform.position = customerSpawn.position;
    }

    IEnumerator CustomerLookLeft(float duration){
        customer.GetComponent<SpriteRenderer>().flipX = true;
        customer.GetComponent<Customer>().isLookingRight = false;

        customerSpriteManager.ChangeSpriteToAngry(customerSpriteManager.currentIndex);

        yield return new WaitForSeconds(duration);
    }

    IEnumerator FailRoutine(){

        Destroy(GameObject.Find("Product"));

        yield return new WaitForSeconds(1);

        yield return CustomerLookLeft(0);

        yield return MoveCustomerX(2);

        gameStateMachine.TransitionTo(gameStateMachine.customerEntranceState);
    }
}
