using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ScaleRotation : MonoBehaviour
{
    public GameObject scale_top;
    public GameObject scale_right_arm;
    public GameObject right_arm_connection_point;
    public GameObject scale_left_arm;
    bool scale_top_is_rotating = false;
    float turn_amount = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) && !scale_top_is_rotating){

            StartCoroutine(SmoothRotationRoutine(-20, .2f, true));

        }else if(Input.GetKeyDown(KeyCode.A) && !scale_top_is_rotating){
            
            StartCoroutine(SmoothRotationRoutine(20, .2f, true));

        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if (!scale_top_is_rotating){
            StartCoroutine(SmoothRotationRoutine(-20, .2f, true));
        }
    }

    IEnumerator SmoothRotationRoutine(float amount, float duration, bool bop){
        scale_top_is_rotating = true;

        Quaternion starting_rotation = scale_top.transform.rotation;
        Quaternion final_rotation = Quaternion.Euler(0, 0, scale_top.transform.rotation.eulerAngles.z + amount);

        float t = 0f;

        while (t < duration){

            scale_top.transform.rotation = Quaternion.Slerp(starting_rotation, final_rotation, t / duration);

            scale_left_arm.transform.rotation = Quaternion.Euler(0,0,-scale_top.transform.rotation.z);
            scale_right_arm.transform.rotation = Quaternion.Euler(0,0,-scale_top.transform.rotation.z);
            right_arm_connection_point.transform.rotation = Quaternion.Euler(0,0,-scale_top.transform.rotation.z);
            
            t += Time.deltaTime;

            yield return null;
        }

        scale_top.transform.rotation = final_rotation;

        scale_top_is_rotating = false;
        
        if (bop){
            StartCoroutine(SmoothRotationRoutine(-amount / 2, .5f, false));
        }
        

        yield break; 
    }
}
