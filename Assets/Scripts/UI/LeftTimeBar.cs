using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTimeBar : MonoBehaviour
{
    float initialScale;
    public void StartTimer(float seconds){
        StartCoroutine(Timer(seconds));
    }

    public void StopTimer(){
        StopAllCoroutines();
    }

    public void ResetTimer(){
        StopAllCoroutines();
        transform.localScale = new Vector3(initialScale, transform.localScale.y, transform.localScale.z);
    }

    IEnumerator Timer(float seconds){
        float t = 0;
        initialScale = transform.localScale.x;

        while (t < 1){
            t += Time.deltaTime / seconds;

            transform.localScale = new Vector3(Mathf.Lerp(initialScale, 0, t), transform.localScale.y, transform.localScale.z);

            yield return null;
        }
    }
}
