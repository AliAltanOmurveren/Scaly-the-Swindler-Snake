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

    public GameObject magicWeightPrefab1;
    public GameObject magicWeightPrefab3;
    public GameObject magicWeightPrefab5;
    public GameObject magicWeightPrefab7;
    public Transform magicWeightSpawnPos;

    public GameObject[] productPrefabs;
    public Transform productSpawnPos;
    public Transform productTargetPosition;
    int[] weights = {1, 3, 5, 7};

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

        if (GameObject.Find("Magic Weight") != null){
            Destroy(GameObject.Find("Magic Weight"));
        }
        
        if (GameObject.Find("Product") != null){
            Destroy(GameObject.Find("Product"));
        }

        if(GameManager.magicWeightWeight == 1){
            Instantiate(magicWeightPrefab1, magicWeightSpawnPos.position, Quaternion.identity).name = "Magic Weight";    
        }else if(GameManager.magicWeightWeight == 3){
            Instantiate(magicWeightPrefab3, magicWeightSpawnPos.position, Quaternion.identity).name = "Magic Weight";
        }else if(GameManager.magicWeightWeight == 5){
            Instantiate(magicWeightPrefab5, magicWeightSpawnPos.position, Quaternion.identity).name = "Magic Weight";
        }else if(GameManager.magicWeightWeight == 7){
            Instantiate(magicWeightPrefab7, magicWeightSpawnPos.position, Quaternion.identity).name = "Magic Weight";
        }

        StartCoroutine(MoveCustomerX(arriveTime));
        StartCoroutine(BopCustomer(arriveTime, 6));
        StartCoroutine(MoveProduct(1, true));
    }

    public void EnterForTutorial(){
        customerSpriteManager = customer.GetComponent<CustomerSpriteManager>();
        int index = UnityEngine.Random.Range(0, customerSpriteManager.angrySprites.Length);

        customerSpriteManager.currentIndex = index;

        customerSpriteManager.ChangeSpriteToLookingRight(customerSpriteManager.currentIndex);
        customer.GetComponent<SpriteRenderer>().flipX = false;

        if(GameManager.magicWeightWeight == 1){
            Instantiate(magicWeightPrefab1, magicWeightSpawnPos.position, Quaternion.identity).name = "Magic Weight";    
        }else if(GameManager.magicWeightWeight == 3){
            Instantiate(magicWeightPrefab3, magicWeightSpawnPos.position, Quaternion.identity).name = "Magic Weight";
        }else if(GameManager.magicWeightWeight == 5){
            Instantiate(magicWeightPrefab5, magicWeightSpawnPos.position, Quaternion.identity).name = "Magic Weight";
        }else if(GameManager.magicWeightWeight == 7){
            Instantiate(magicWeightPrefab7, magicWeightSpawnPos.position, Quaternion.identity).name = "Magic Weight";
        }

        StartCoroutine(MoveCustomerX(arriveTime));
        StartCoroutine(BopCustomer(arriveTime, 6));
        StartCoroutine(MoveProduct(1, false));
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

    IEnumerator MoveProduct(float seconds, bool transition) {
        yield return new WaitForSeconds(arriveTime);

        int index = UnityEngine.Random.Range(0, productPrefabs.Length);

        product = Instantiate(productPrefabs[index], customerTargetPosition.position, Quaternion.Euler(0,0,0));
        product.name = "Product";

        int numOfWeights = UnityEngine.Random.Range(1, 4);

        product.GetComponent<Product>().weight = 0;

        for (int i = 0; i < numOfWeights; i++){
            int j = UnityEngine.Random.Range(0, weights.Length);

            product.GetComponent<Product>().weight += weights[j];
        }

        float t = 0;

        while(t < 1){
            t += Time.deltaTime / seconds;

            product.transform.position = Vector3.Lerp(customerTargetPosition.position, productTargetPosition.position, t);

            yield return null;
        }

        yield return new WaitForSeconds(1);

        if(transition)
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
