using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public float weight;

    // Start is called before the first frame update
    void Start()
    {
        GameObject magicWeight = GameObject.Find("Magic Weight");

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), magicWeight.GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Arm")){
            other.gameObject.GetComponent<LeftArm>().totalWeight += weight;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Arm")){
            other.gameObject.GetComponent<LeftArm>().totalWeight -= weight;
        }
    }
}
