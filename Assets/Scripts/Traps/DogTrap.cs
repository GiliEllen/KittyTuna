using UnityEngine;

public class DogTrap : Trap
{
    public Sprite[] idleAnimationFrames; 
    public Sprite[] triggeredAnimationFrames; 
    private SpriteRenderer spriteRenderer;
    private float damageAmount = 1; 
    private bool isTriggered = false;

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
            PlayIdleAnimation();
        }
    }

    private void PlayIdleAnimation()
    {
        int index = Mathf.FloorToInt(Time.time * 4) % idleAnimationFrames.Length; 
        spriteRenderer.sprite = idleAnimationFrames[index];
    }

    protected override void OnTriggerEnter2D(Collider2D collision) 
    {
        if (isTriggered) return; 

        IDamagable damagable = collision.GetComponent<IDamagable>();
        if (damagable != null && whatIDamage == (whatIDamage | (1 << collision.gameObject.layer))) 
        {
            ApplyDamage(damagable);
            isTriggered = true;
            TriggerAnimation();
            if (damageEffectCoroutine != null) StopCoroutine(damageEffectCoroutine);
            damageEffectCoroutine = StartCoroutine(ShowDamageEffect());
        }
    }

    public override void ApplyDamage(IDamagable damagable)
    {
        damagable.TakeDamage((int)damageAmount);
    }

    private void TriggerAnimation()
    {
        spriteRenderer.sprite = triggeredAnimationFrames[0]; 
    }

    private System.Collections.IEnumerator ShowDamageEffect()
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
}
