using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTimeBar : MonoBehaviour
{
    public void StartTimer(float seconds){
        StartCoroutine(Timer(seconds));
    }

    IEnumerator Timer(float seconds){
        float t = 0;
        float initial_scale = transform.localScale.x;

        while (t < 1){
            t += Time.deltaTime / seconds;

            transform.localScale = new Vector3(Mathf.Lerp(initial_scale, 0, t), transform.localScale.y, transform.localScale.z);

            yield return null;
        }
    }
}
