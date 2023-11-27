using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialState : MonoBehaviour, IGameState
{
    public UIController uiController;

    GameStateMachine gameStateMachine;

    MagicWeight magicWeight;
    public GameObject customer;
    public GameObject arrowPrefab;
    public Transform arrowSwitchPos;
    public Transform arrowMagicWeightPos;

    float letterSpeed = 0.05f;
    public CustomerEntranceState customerEntranceState;

    public void Enter()
    {
        gameStateMachine = gameObject.GetComponent<GameStateMachine>();

        StartCoroutine(TutorialChainRoutine());
    }

    public void Exit()
    {
        
    }

    void IGameState.Update()
    {
        
    }

    IEnumerator WaitForUI(){
        while(uiController.uiIsChanging){
            yield return null;
        }
    }

    IEnumerator WaitForClick(){
        while (!Input.GetMouseButtonDown(0)){
            yield return null;
        }
    }

    IEnumerator TutorialChainRoutine(){
       /*
        yield return new WaitForSeconds(2);

        uiController.DialogueBoxPopIn(1);

        yield return WaitForUI();

        uiController.ChangeText("Welcome to Scaly,\nthe Swindler Snake!\n(Click to continue...)", letterSpeed);

        yield return WaitForUI();
        yield return WaitForClick();

        uiController.ResetText();

        uiController.ChangeText("Your goal is opening your own shop\nbut that's not cheap.", letterSpeed);

        yield return WaitForUI();
        yield return WaitForClick();

        uiController.ResetText();

        uiController.ChangeText("Your cut of the job is barely enough for living. You need to swindle customers to achieve your goal.", letterSpeed);

        yield return WaitForUI();
        yield return WaitForClick();

        uiController.DialogueBoxPopOut(1);

        yield return WaitForUI();
*/
        customerEntranceState.EnterForTutorial();

        yield return new WaitForSeconds(4);

        customer.GetComponent<SpriteRenderer>().flipX = true;

        uiController.DialogueBoxPopIn(1);

        yield return WaitForUI();

        uiController.ChangeText("Click the switch to drop magic weight. Customers can see it until it hits the tray. Drop it while customers are looking away!", letterSpeed);

        yield return WaitForUI();

        GameObject arrowSwitch = Instantiate(arrowPrefab, new Vector3(arrowSwitchPos.position.x, arrowSwitchPos.position.y, -50), Quaternion.Euler(0, 0, 90));

        yield return new WaitForSeconds(2);

        arrowSwitch.SetActive(false);

        GameObject arrowMagicWeight = Instantiate(arrowPrefab, new Vector3(arrowMagicWeightPos.position.x, arrowMagicWeightPos.position.y, -50), Quaternion.Euler(0, 0, 90));

        yield return new WaitForSeconds(2);

        Destroy(arrowMagicWeight);

        uiController.DialogueBoxPopOut(1);

        yield return WaitForUI();

        arrowSwitch.SetActive(true);

        magicWeight = GameObject.Find("Magic Weight").GetComponent<MagicWeight>();
        
        while(!magicWeight.isTouchingTray){
            yield return null;
        }

        customer.GetComponent<SpriteRenderer>().flipX = false;

        uiController.DialogueBoxPopIn(1);

        yield return WaitForUI();

        uiController.ChangeText("Finally guess the total weight. Time is limited so be careful. Good Luck", 0.05f);

    }
}
