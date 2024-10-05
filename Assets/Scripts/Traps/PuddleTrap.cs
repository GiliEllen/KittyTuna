using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleTrap : Trap
{
    public Sprite sprite;
    private SpriteRenderer spriteRenderer; 

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
            cat.TakeDamage(1); 
        }
    }
}
