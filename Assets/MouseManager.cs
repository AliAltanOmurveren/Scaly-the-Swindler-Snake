using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public GameObject clickedWeight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
