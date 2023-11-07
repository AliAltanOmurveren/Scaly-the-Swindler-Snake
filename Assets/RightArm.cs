using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using UnityEngine;

public class RightArm : MonoBehaviour
{
    List<GameObject> weights;
    public List<GameObject> totalWeightContributers;
    public List<GameObject> bottomTouchingWeights;
    public int totalWeight;
    public ScaleRotation scaleRotation;

    // Start is called before the first frame update
    void Start()
    {
        weights = new List<GameObject>();
        totalWeightContributers = new List<GameObject>();
        bottomTouchingWeights = new List<GameObject>(); 
    }

    // Update is called once per frame
    void Update()
    {
        totalWeight = 0;

        foreach(GameObject weight in weights){
            bool isStationary = weight.GetComponent<WeightManager>().isStationary;
            bool isTouchingAnother = false;

            foreach(GameObject w in weights){
                if(w.GetComponent<WeightManager>().touchingWeights.Contains(weight)){
                    isTouchingAnother = true;
                }
            }
            
            if((isStationary && bottomTouchingWeights.Contains(weight)) || isTouchingAnother){
                //totalWeight += weight.GetComponent<WeightManager>().weight;
                if(!totalWeightContributers.Contains(weight)){
                    totalWeightContributers.Add(weight);
                }   
            }else{
                if(totalWeightContributers.Contains(weight)){
                    totalWeightContributers.Remove(weight);
                }
            }
        }

        foreach(GameObject weight in totalWeightContributers){
            totalWeight += weight.GetComponent<WeightManager>().weight;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Weight")){
            if(!weights.Contains(other.transform.gameObject)){
                weights.Add(other.transform.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(weights.Contains(other.transform.gameObject)){
            weights.Remove(other.transform.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Weight") && !bottomTouchingWeights.Contains(other.gameObject)){
            bottomTouchingWeights.Add(other.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Weight") && bottomTouchingWeights.Contains(other.gameObject)){
            bottomTouchingWeights.Remove(other.gameObject);
        }
    }
}
