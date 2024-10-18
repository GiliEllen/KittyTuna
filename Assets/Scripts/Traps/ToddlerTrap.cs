using System.Collections;
using UnityEngine;

public class ToddlerTrap : Trap
{
    public Sprite[] walkLeftSprites; 
    public Sprite[] walkRightSprites; 
    public Sprite[] triggeredSprites; 
    public Sprite[] fallingSprites; 
    public Sprite idleSprite; 
    public float speed = 1f;          
    private Vector2 movementDirection = Vector2.left;
    private SpriteRenderer spriteRenderer;
    private Coroutine walkAnimationCoroutine;
    private bool isPaused = false;
    private bool isActive = true;
    public GameObject heartEffectPrefab;
    private Coroutine heartEffectCoroutine;


    protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(MoveAndAnimate());
    }

private IEnumerator MoveAndAnimate()
{
    int spriteIndex = 0;
    float moveDuration = Time.time + 0.1f; 

    if(!isActive) 
    {
        yield break;
    }

    while (movementDirection != Vector2.zero && !isPaused)
    {
        transform.Translate(movementDirection * speed * Time.deltaTime);

        Sprite[] currentSprites = movementDirection == Vector2.left ? walkLeftSprites : walkRightSprites;
        if (currentSprites.Length > 0)
        {
            spriteRenderer.sprite = currentSprites[spriteIndex];
            spriteIndex = (spriteIndex + 1) % currentSprites.Length;
        }

        yield return null;

        if (Time.time >= moveDuration)
        {
            moveDuration = Time.time + 0.2f; 
        }
    }

        walkAnimationCoroutine = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive) return;
        movementDirection = movementDirection == Vector2.left ? Vector2.right : Vector2.left;

        PlayableCharacter cat = collision.gameObject.GetComponent<PlayableCharacter>();
        if (cat != null)
        {
            isPaused = true;
            ApplyDamage(cat);
        }
    }

    public override void ApplyDamage(IDamagable damagable)
    {
        PlayableCharacter cat = damagable as PlayableCharacter;

        if (cat != null)
        {
            if (cat.catType == CatType.CalicoCat) {
                this.TakeDamage(1); 
                isActive = false;
                isPaused = true;
                cat.SpecialAbility();
                Debug.Log("play fall animation now");
                StartCoroutine(PlayFallAnimation());
            } else {
            cat.TakeDamage(1); 
            StartCoroutine(PlayTriggeredAnimation());
            }

        }
    }

    private IEnumerator PlayTriggeredAnimation()
    {
        if(!isActive) 
        {
            yield break;
        }
        
        StopCoroutine(MoveAndAnimate());
        Debug.Log("Triggered animation started.");
        for (int i = 0; i < triggeredSprites.Length; i++)
        {
            spriteRenderer.sprite = triggeredSprites[i];
            yield return new WaitForSeconds(0.2f);
        }
        Debug.Log("Triggered animation ended.");
        isPaused = false; 
        StartCoroutine(MoveAndAnimate());
    }

    private IEnumerator PlayFallAnimation()
    {
        if (heartEffectCoroutine != null) StopCoroutine(heartEffectCoroutine);
        heartEffectCoroutine = StartCoroutine(ShowHeartEffect());
        StopCoroutine(MoveAndAnimate());
        Debug.Log("Falling animation started.");
        for (int i = 0; i < fallingSprites.Length; i++)
        {
            spriteRenderer.sprite = fallingSprites[i];
            yield return new WaitForSeconds(0.2f);
        }
        spriteRenderer.sprite = idleSprite;
        isPaused = true;
        Debug.Log("Falling animation ended.");
        
        MakeGameObjectColliderInactive();
    }

    private IEnumerator ShowHeartEffect()
    {
        Debug.Log("Show effect");
        GameObject effect = Instantiate(heartEffectPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(10);
        Destroy(effect);
    }

    private void MakeGameObjectColliderInactive() {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }
}
