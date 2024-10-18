using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleTrap : Trap
{
    public Sprite sprite;
    private SpriteRenderer spriteRenderer; 
    private bool isActive = true;

    protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayableCharacter cat = collision.gameObject.GetComponent<PlayableCharacter>();
        if (cat != null)
        {
            ApplyDamage(cat);
        }
    }

    public override void ApplyDamage(IDamagable damagable)
    {
        PlayableCharacter cat = damagable as PlayableCharacter;
        if (cat != null)
        {
             if (cat.catType == CatType.BlackCat)
            {
                this.TakeDamage(1); 
                cat.SpecialAbility();
            }
            else
            {
                cat.TakeDamage(1); 
            }
        }
    }

    public override void Die()
    {
        Debug.Log($"PuddleTrap Die method called on object with Instance ID: {gameObject.GetInstanceID()}"); 
        StartCoroutine(ShrinkAndDestroy());
        base.Die();
    }

    private IEnumerator ShrinkAndDestroy()
{
    float duration = 2f; 
    float elapsedTime = 0f;
    Vector3 originalScale = transform.localScale;

    while (elapsedTime < duration)
    {
       
        float scaleFactor = Mathf.Lerp(1f, 0f, elapsedTime / duration);
        transform.localScale = originalScale * scaleFactor;

        elapsedTime += Time.deltaTime;
        yield return null; 
    }

    transform.localScale = Vector3.zero;
    Debug.Log($"Destroying PuddleTrap GameObject: {gameObject.name}");
    // Destroy(gameObject);
    gameObject.SetActive(false);
}
}
