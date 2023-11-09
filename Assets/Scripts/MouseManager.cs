using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class MouseManager : MonoBehaviour
{
    public GameObject clickedWeight;

    public SpriteShapeController ssp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spline spline = ssp.spline;

        if(clickedWeight == null){
            spline.SetPosition(0, Camera.main.ScreenToWorldPoint(Input.mousePosition) - ssp.transform.position);
        }else{
            spline.SetPosition(0, clickedWeight.transform.position - ssp.transform.position);
        }


        if(Input.GetMouseButtonDown(0)){
            /*
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100)){
                    Debug.Log("Weight clicked");

                if(hit.transform.CompareTag("Weight")){
                    clickedWeight = hit.transform.gameObject;
                }
            }
            */

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

            foreach(RaycastHit2D hit in hits){
                if(hit.transform.CompareTag("Weight")){
                    clickedWeight = hit.transform.gameObject;
                }
            }
        }

        if(Input.GetMouseButtonUp(0)){
            clickedWeight = null;
        }
    }
}
