using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class MouseManager : MonoBehaviour
{
    public GameObject clickedWeight;
    public GameObject releasingArms;

    public GameStateMachine gameStateMachine;

    public SpriteShapeController ssp;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        gameStateMachine = GameObject.Find("Game State Machine").GetComponent<GameStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        Spline spline = ssp.spline;

        if(clickedWeight == null){
            spline.SetPosition(0, Camera.main.ScreenToWorldPoint(Input.mousePosition) - ssp.transform.position);

            Vector3 spline0Pos = spline.GetPosition(0);
            Vector3 spline2Pos = spline.GetPosition(2);
            spline.SetPosition(1, (spline0Pos + spline2Pos) / 2);

        }else{
            spline.SetPosition(0, clickedWeight.transform.position - ssp.transform.position);
            
            Vector3 spline0Pos = spline.GetPosition(0);
            Vector3 spline2Pos = spline.GetPosition(2);
            spline.SetPosition(1, (spline0Pos + spline2Pos) / 2);
        }


        if(Input.GetMouseButtonDown(0)){

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

            foreach(RaycastHit2D hit in hits){
                if(hit.transform.CompareTag("Weight")){
                    clickedWeight = hit.transform.gameObject;
                }else if(hit.transform.CompareTag("Switch") && (object)gameStateMachine.currentGameState == gameStateMachine.magicWeightMinigameState){
                    releasingArms.GetComponent<ReleasingArms>().OpenArms();
                }
            }
        }

        if(Input.GetMouseButtonUp(0)){
            clickedWeight = null;
        }         
    }
}
