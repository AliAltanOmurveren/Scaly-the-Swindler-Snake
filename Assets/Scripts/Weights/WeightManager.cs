using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightManager : MonoBehaviour
{
    public int weight;
    public bool isStationary;
    Rigidbody2D rb;
    public List<GameObject> touchingWeights;
    Vector3 oldPos;
    Vector3 newPos;
    Quaternion oldRot;
    Quaternion newRot;

    // Start is called before the first frame update
    void Start()
    {
        touchingWeights = new List<GameObject>();
        rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        oldPos = newPos;
        oldRot = newRot;

        newPos = transform.position;
        newRot = transform.rotation;

        isStationary = false;

        if(rb.IsSleeping()){
            isStationary = true;
        }

        if(Vector3.Distance(oldPos, newPos) < 0.01 && Quaternion.Angle(oldRot, newRot) < 1){
            isStationary = true;
        }else{
            isStationary = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.CompareTag("Weight")){
            if(!touchingWeights.Contains(other.gameObject) && other.gameObject.GetComponent<WeightManager>().isStationary){
                touchingWeights.Add(other.gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.transform.CompareTag("Weight")){
            if(touchingWeights.Contains(other.gameObject)){
                touchingWeights.Remove(other.gameObject);
            }
        }
    }
}
