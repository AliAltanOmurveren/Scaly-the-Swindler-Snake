using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleasingArms : MonoBehaviour
{
    public GameObject leftArmRelease;
    public GameObject rightArmRelease;
    
    public void OpenArms() {
        leftArmRelease.transform.rotation = Quaternion.Euler(0, 0, -90);
        rightArmRelease.transform.rotation = Quaternion.Euler(0, 0, 90);

        GameObject.Find("Magic Weight").GetComponent<MagicWeight>().isFalling = true;

        CloseArms();
    }

    public void CloseArms(){
        StartCoroutine(CloseArmsRoutine(1));
    }

    IEnumerator CloseArmsRoutine(float duration){

        yield return new WaitForSeconds(1);

        float t = 0;

        while(t < 1){
            t += Time.deltaTime / duration;

            leftArmRelease.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(-90, 0, t));
            rightArmRelease.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(90, 0, t));

            yield return null;
        }
    }
}
