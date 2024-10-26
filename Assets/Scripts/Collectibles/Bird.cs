using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public Sprite[] spritesArray;
    public GameManager gameManager;
    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found!");
        }
    }

    public void SetSprite() {
        if (spriteRenderer) {
            spriteRenderer.sprite = spritesArray[0];
        }
    }

    public void HandleCollected() {
        gameObject.SetActive(false);
        gameManager.AddPoint();
    }
}
