using System.Collections;
using UnityEngine;

public class ToddlerTrap : Trap
{
    public Sprite[] walkLeftSprites; 
    public Sprite[] walkRightSprites; 
    public Sprite[] triggeredSprites; 
    public float speed = 1f;          
    private Vector2 movementDirection = Vector2.left;
    private SpriteRenderer spriteRenderer;
    private Coroutine walkAnimationCoroutine;
    private bool isPaused = false;


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
        movementDirection = movementDirection == Vector2.left ? Vector2.right : Vector2.left;

        PlayableCharacter cat = collision.gameObject.GetComponent<PlayableCharacter>();
        if (cat != null)
        {
            isPaused = true;
            ApplyDamage(cat);
            StartCoroutine(PlayTriggeredAnimation());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayableCharacter cat = collision.GetComponent<PlayableCharacter>();
        if (cat != null)
        {
            isPaused = true;
            ApplyDamage(cat);
            StartCoroutine(PlayTriggeredAnimation());
        }
    }

    public override void ApplyDamage(IDamagable damagable)
    {
        PlayableCharacter cat = damagable as PlayableCharacter;
        if (cat != null)
        {
            cat.TakeDamage(1); 
            StartCoroutine(PlayTriggeredAnimation());
        }
    }

    private IEnumerator PlayTriggeredAnimation()
    {
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
}
