using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWeights : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 mouseWorldPos;
    public GameObject weight;
    TargetJoint2D weightJoint;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)){
            mouseWorldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

            if(!weight.GetComponent<TargetJoint2D>()){
                weightJoint = weight.AddComponent<TargetJoint2D>();
            }else {
                weightJoint = weight.GetComponent<TargetJoint2D>();
            }

            weightJoint.anchor = new Vector2(0, 0);

            if (weightJoint){
                weightJoint.target = mouseWorldPos;
            }
        }

        if(Input.GetMouseButtonUp(0)){
            if (weightJoint){
                Destroy(weightJoint);
                weightJoint = null;
            }
        }
    }
}
