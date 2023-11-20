using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpriteManager : MonoBehaviour
{
    public Sprite[] lookingRightSprites;
    public Sprite[] suspiciousSprites;
    public Sprite[] angrySprites;

    public int currentIndex;

    SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSpriteToLookingRight(int index){
        spriteRenderer.sprite = lookingRightSprites[index];
    }

    public void ChangeSpriteToSuspicious(int index){
        spriteRenderer.sprite = suspiciousSprites[index];
    }

    public void ChangeSpriteToAngry(int index){
        spriteRenderer.sprite = angrySprites[index];
    }
}
