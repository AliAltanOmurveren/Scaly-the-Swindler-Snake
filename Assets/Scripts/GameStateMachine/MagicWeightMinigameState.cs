using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWeightMinigameState : MonoBehaviour, IGameState
{
    GameStateMachine gameStateMachine;
    public GameObject timeBar;
    public LeftTimeBar leftTimeBar;
    GameObject customer;
    CustomerSpriteManager customerSpriteManager;
    MagicWeight magicWeight;
    float minigameDuration = 5;

    private void Start() {
        gameStateMachine = gameObject.GetComponent<GameStateMachine>();
    }

    public void Enter()
    {
        Debug.Log("MagicState enter");

        customer = GameObject.Find("Customer");
        customerSpriteManager = customer.GetComponent<CustomerSpriteManager>();
        
        StartCoroutine(CustomerLookRight(0));

        magicWeight = GameObject.Find("Magic Weight").GetComponent<MagicWeight>();

        timeBar.GetComponent<LeftTimeBar>().StartTimer(minigameDuration);

        StartCoroutine(CustomerAlternateLook(minigameDuration));
    }

    public void Exit()
    {
        Debug.Log("Magicstate exit");
        customerSpriteManager.ChangeSpriteToLookingRight(customerSpriteManager.currentIndex);
        customer.GetComponent<SpriteRenderer>().flipX = false;
        timeBar.GetComponent<LeftTimeBar>().ResetTimer();
        StopAllCoroutines();
    }

    void IGameState.Update()
    {
        if(magicWeight.isFalling && customer.GetComponent<Customer>().isLookingRight){
            Fail();
        }

        if(leftTimeBar.timeExpired && !magicWeight.isTouchingTray){
            gameStateMachine.TransitionTo(gameStateMachine.weighingMinigameState);
        }
    }

    void Fail(){
            Debug.Log("Fail");
            Destroy(magicWeight.gameObject);
            timeBar.GetComponent<LeftTimeBar>().StopTimer();
            gameStateMachine.TransitionTo(gameStateMachine.failState);
    }

    IEnumerator CustomerAlternateLook(float totalDuration){
        float t = 0;

        while (t < totalDuration){

            float timeOnLeft = Random.Range(0.85f, 1.1f);
            t += timeOnLeft;
            if(t > totalDuration){
                break;
            }else{
                yield return CustomerLookLeft(timeOnLeft);
            }

            float timeOnRight = Random.Range(0.5f, 1f);
            t += timeOnRight;
            if(t > totalDuration){
                break;
            }else{
                yield return CustomerLookRight(timeOnRight);
            }
        }

        yield return CustomerLookRight(0);
    }

    IEnumerator CustomerLookLeft(float duration){
        customer.GetComponent<SpriteRenderer>().flipX = true;
        customer.GetComponent<Customer>().isLookingRight = false;

        customerSpriteManager.ChangeSpriteToLookingRight(customerSpriteManager.currentIndex);

        yield return new WaitForSeconds(duration);
    }

    IEnumerator CustomerLookRight(float duration){
        customer.GetComponent<SpriteRenderer>().flipX = false;
        customer.GetComponent<Customer>().isLookingRight = true;

        customerSpriteManager.ChangeSpriteToSuspicious(customerSpriteManager.currentIndex);

        yield return new WaitForSeconds(duration);
    }
}
