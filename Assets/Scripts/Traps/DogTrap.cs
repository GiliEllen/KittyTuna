using UnityEngine;
using System.Collections;

public class DogTrap : Trap
{
    public Sprite[] idleAnimationFrames; 
    public Sprite[] triggeredAnimationFrames; 
    public Sprite[] SleepAnimationFrames; 
    private SpriteRenderer spriteRenderer;
    private float damageAmount = 1; 
    private bool isTriggered = false;
    private bool isActive = true;

    public GameObject damageEffectPrefab;
    private Coroutine damageEffectCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isTriggered)
        { 
            if (!isActive) 
            {
                PlaySleepAnimation();
            } else 
            {
                PlayIdleAnimation();
            }
        }
    }

    private void PlayIdleAnimation()
    {
        int index = Mathf.FloorToInt(Time.time * 4) % idleAnimationFrames.Length; 
        spriteRenderer.sprite = idleAnimationFrames[index];
    }

    private void PlaySleepAnimation()
    {
        spriteRenderer.sprite = SleepAnimationFrames[0];
    }

    protected override void OnTriggerEnter2D(Collider2D collision) 
    {
        if (isTriggered) return; 
        if (!isActive) return;

        IDamagable damagable = collision.GetComponent<IDamagable>();
        if (damagable != null && whatIDamage == (whatIDamage | (1 << collision.gameObject.layer))) 
        {
            ApplyDamage(damagable);
        }
    }

    public override void ApplyDamage(IDamagable damagable)
    {
        if (!isActive) return;

        PlayableCharacter cat = damagable as PlayableCharacter;

        if (cat != null) 
        {
            if (cat.catType == CatType.GrayCat)
            {
                this.TakeDamage(1); 
                isActive = false;
                PlaySleepAnimation();
                cat.SpecialAbility();
            }
            else
            {
                cat.TakeDamage(1); 
                TriggerAnimation();
                isTriggered = true;
            if (damageEffectCoroutine != null) StopCoroutine(damageEffectCoroutine);
            damageEffectCoroutine = StartCoroutine(ShowDamageEffect());
            }
        }
        else
        {
            Debug.Log("The object is not a PlayableCharacter, cannot apply damage.");
        }
    }

    private void TriggerAnimation()
    {
        if (!isActive) return;
        spriteRenderer.sprite = triggeredAnimationFrames[0]; 
    }

    private IEnumerator ShowDamageEffect()
    {
        GameObject effect = Instantiate(damageEffectPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2);
        Destroy(effect);
        ResetTrap();
    }

    private void ResetTrap()
    {
        isTriggered = false;
    }

    private void Die()
    {
        base.Die();
        isActive = false;
    }
}
