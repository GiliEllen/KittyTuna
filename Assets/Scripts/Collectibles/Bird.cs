using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Bird : MonoBehaviour
{
    public Sprite[] spritesArray;
    public GameManager gameManager;
    public SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool isCollected = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

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

    public async void HandleCollected() {
        if (isCollected) return;

        isCollected = true;
        audioSource.Play();
        spriteRenderer.enabled = false;
        gameManager.AddPoint(1);
        await Task.Delay(2000);
        gameObject.SetActive(false);
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
