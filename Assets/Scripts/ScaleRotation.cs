using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ScaleRotation : MonoBehaviour
{
    public GameObject scaleTop;
    public GameObject scaleRightArm;
    public GameObject rightArmConnectionPoint;
    public GameObject scaleLeftArm;
    public bool scaleTopIsRotating = false;
    public float currentZRotation = 0; 
    public RightArm rightArm;
    public LeftArm leftArm;
    float turnSpeed = 40f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.D) && !scaleTopIsRotating){

            scaleTop.transform.Rotate(0, 0, turnSpeed );

        }else if(Input.GetKeyDown(KeyCode.A) && !scaleTopIsRotating){
            
            StartCoroutine(SmoothRotationRoutine(20, .2f));

        }

        float scaleTopAngle = scaleTop.transform.localRotation.eulerAngles.z > 180? scaleTop.transform.localRotation.eulerAngles.z - 360 : scaleTop.transform.localRotation.eulerAngles.z;
        float weightAngle = (leftArm.totalWeight * 2) - rightArm.totalWeight * 2; 
        float angleDiff = scaleTopAngle - weightAngle; 
        
        if(angleDiff > 0){
            scaleTop.transform.Rotate(0, 0, -turnSpeed * Time.deltaTime);
        }else if(angleDiff < 0){
            scaleTop.transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
        }
        
        scaleLeftArm.transform.rotation = Quaternion.Euler(0,0,-scaleTop.transform.rotation.z);
        scaleRightArm.transform.rotation = Quaternion.Euler(0,0,-scaleTop.transform.rotation.z);
        rightArmConnectionPoint.transform.rotation = Quaternion.Euler(0,0,-scaleTop.transform.rotation.z);
    }

    private void OnTriggerEnter2D(Collider2D other) {
    }

    public void rotateScaleTo(int value, float duration){
        if(value != currentZRotation){  
            StartCoroutine(SmoothRotationRoutine(value, duration));
        }
    }

    IEnumerator SmoothRotationRoutine(float amount, float duration){
        scaleTopIsRotating = true;

        Quaternion starting_rotation = scaleTop.transform.rotation;
        Quaternion final_rotation = Quaternion.Euler(0, 0, amount);
        //Quaternion final_rotation = Quaternion.Euler(0, 0, scaleTop.transform.rotation.eulerAngles.z + amount);

        float t = 0f;

        while (t < duration){

            scaleTop.transform.rotation = Quaternion.Slerp(starting_rotation, final_rotation, t / duration);
            currentZRotation = scaleTop.transform.rotation.eulerAngles.z;

            scaleLeftArm.transform.rotation = Quaternion.Euler(0,0,-scaleTop.transform.rotation.z);
            scaleRightArm.transform.rotation = Quaternion.Euler(0,0,-scaleTop.transform.rotation.z);

            t += Time.deltaTime;

            yield return null;
        }

        scaleTop.transform.rotation = final_rotation;

        scaleTopIsRotating = false;

        yield break; 
    }
}
