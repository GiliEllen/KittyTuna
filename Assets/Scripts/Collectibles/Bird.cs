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

    private void Update()
    {       
        PlayIdleAnimation();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollected();
    }

    private void PlayIdleAnimation()
    {
        int index = Mathf.FloorToInt(Time.time * 4) % spritesArray.Length; 
        spriteRenderer.sprite = spritesArray[index];
    }
}
