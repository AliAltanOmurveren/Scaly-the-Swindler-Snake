using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWeight : MonoBehaviour
{
    public bool isFalling;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Arm")){
            isFalling = false;
        }
    }

    
}
