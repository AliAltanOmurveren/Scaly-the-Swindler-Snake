using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerEntranceState : MonoBehaviour, IGameState
{
    GameStateMachine gameStateMachine;

    public GameObject customer;
    CustomerSpriteManager customerSpriteManager;
    public Transform customerSpawn;
    public Transform customerTargetPosition;

    GameObject product;

    public GameObject magicWeightPrefab;
    public Transform magicWeightSpawnPos;

    public GameObject productPrefab;
    public Transform productSpawnPos;
    public Transform productTargetPosition;

    float arriveTime = 3.0f;

    private void Start() {
        gameStateMachine = gameObject.GetComponent<GameStateMachine>();
    }

    public void Enter()
    {
        customerSpriteManager = customer.GetComponent<CustomerSpriteManager>();
        int index = UnityEngine.Random.Range(0, customerSpriteManager.angrySprites.Length);

        customerSpriteManager.currentIndex = index;

        customerSpriteManager.ChangeSpriteToLookingRight(customerSpriteManager.currentIndex);
        customer.GetComponent<SpriteRenderer>().flipX = false;

        Instantiate(magicWeightPrefab, magicWeightSpawnPos.position, Quaternion.identity).name = "Magic Weight";

        StartCoroutine(MoveCustomerX(arriveTime));
        StartCoroutine(BopCustomer(arriveTime, 6));
        StartCoroutine(MoveProduct(1));
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

            Vector3 lerpedPosition = Vector3.Lerp(customerSpawn.position, customerTargetPosition.position, t);

            customer.transform.position = new Vector3(lerpedPosition.x, customer.transform.position.y, customer.transform.position.z);

            yield return null;
        }
    }

    IEnumerator MoveProduct(float seconds) {
        yield return new WaitForSeconds(arriveTime);

        product = Instantiate(productPrefab, customerTargetPosition.position, Quaternion.Euler(0,0,0));
        product.name = "Product";

        float t = 0;

        while(t < 1){
            t += Time.deltaTime / seconds;

            product.transform.position = Vector3.Lerp(customerTargetPosition.position, productTargetPosition.position, t);

            yield return null;
        }

        yield return new WaitForSeconds(1);

        gameStateMachine.TransitionTo(gameStateMachine.magicWeightMinigameState);
    }

    IEnumerator BopCustomer(float seconds, int times){
        for (int i = 0; i < times; i++){
            yield return BopCustomerUp(seconds / times);
        }
    }

    IEnumerator BopCustomerUp(float seconds){
        float t = 0;

        yield return BopCustomerDown(seconds);

        while(t < seconds){
            t += Time.deltaTime / seconds;

            Vector3 lerpedPosition = Vector3.Lerp(customer.transform.position, customerTargetPosition.transform.position + new Vector3(0, 0.5f, 0), t);

            customer.transform.position = new Vector3(customer.transform.position.x, lerpedPosition.y, customer.transform.position.z);

            yield return null;
        }

    }

    IEnumerator BopCustomerDown(float seconds){
        float t = 0;

        while(t < seconds){
            t += Time.deltaTime / seconds;

            Vector3 lerpedPosition = Vector3.Lerp(customer.transform.position, customerTargetPosition.transform.position + new Vector3(0, -0.5f, 0), t);

            customer.transform.position = new Vector3(customer.transform.position.x, lerpedPosition.y, customer.transform.position.z);

            yield return null;
        }

    }
}
