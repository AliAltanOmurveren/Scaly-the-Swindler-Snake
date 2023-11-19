using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailState : MonoBehaviour, IGameState
{
    GameObject customer;

    public Transform customerSpawn;
    public Transform customerTargetPosition;
    public void Enter()
    {
        customer = GameObject.Find("Customer");

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
        yield return new WaitForSeconds(duration);
    }

    IEnumerator FailRoutine(){

        Destroy(GameObject.Find("Product"));

        yield return new WaitForSeconds(1);

        yield return CustomerLookLeft(0);

        yield return MoveCustomerX(2);
    }
}
