using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public RectTransform dialogueBox;
    public RectTransform dialogueBoxBackground;
    public TMP_Text dialogueText;

    public bool uiIsChanging = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.localScale = Vector3.zero;
        dialogueBoxBackground.localScale = Vector3.zero;
        ResetText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DialogueBoxPopIn(float duration){
        uiIsChanging = true;

        StartCoroutine(PopInRoutine(duration));
    }

    public void ChangeText(string text, float letterSpeed){
        uiIsChanging = true;

        StartCoroutine(TypeWriterEffectRoutine(text, letterSpeed));
    }

    public void DialogueBoxPopOut(float duration){
        uiIsChanging = true;

        StartCoroutine(PopOutRoutine(duration));
    }

    public void ResetText(){
        dialogueText.text = "";
    }

    IEnumerator PopInRoutine(float duration){
        ResetText();

        float t = 0;

        while(t < 1){
            t += Time.deltaTime / duration;

            dialogueBox.localScale = new Vector3(t, t, t);
            dialogueBoxBackground.localScale = new Vector3(t, t, t);
            dialogueText.GetComponent<RectTransform>().localScale = new Vector3(t, t, t);

            yield return null;
        }
        
        uiIsChanging = false;
    }

    IEnumerator PopOutRoutine(float duration){
        float t = 1;

        while(t > 0){
            t -= Time.deltaTime / duration;
            
            dialogueBox.localScale = new Vector3(t, t, t);
            dialogueBoxBackground.localScale = new Vector3(t, t, t);
            dialogueText.GetComponent<RectTransform>().localScale = new Vector3(t, t, t);

            yield return null;
        }

        ResetText();

        uiIsChanging = false;
    }

    IEnumerator TypeWriterEffectRoutine(string text, float letterSpeed){
        foreach(char letter in text){
            dialogueText.text += letter;

            yield return new WaitForSeconds(letterSpeed); 
        }

        uiIsChanging = false;
    }
}
