using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public abstract class PlayableCharacter : MonoBehaviour, IDamagable
{
    public float speed;
    // protected Vector2 movement;

    public int maxHP = 3;
    public int CurrentHp { get; private set; }

    // protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;

    public Sprite[] walkDownSprites;
    public Sprite[] walkUpSprites;
    public Sprite[] walkLeftSprites;
    public Sprite[] walkRightSprites;
    public float animationFrameDuration = 0.1f;
    public CatType catType;
    public string catName;

    private Coroutine walkAnimationCoroutine; 
    public bool isWalking = true; 
    private Coroutine hurtEffectCoroutine;
    private bool isPushedBack = false;
    public GameManager gameManager;


    protected virtual void Awake()
    {
        // rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    protected virtual void Start()
    {   
        if (CurrentHp <= 0) 
        {
            CurrentHp = maxHP;
        }
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
        FindObjectOfType<HPDisplayManager>().UpdateHP(this);
    }

    public void SetHP(int hp)
    {
        CurrentHp = hp;
        FindObjectOfType<HPDisplayManager>().UpdateHP(this); 
    }


    public virtual void OnMovement(InputValue value)
    {
        Debug.Log("Movement");
    }

    public virtual void SpecialAbility() 
    {
        isWalking = false;
    }

    public void TakeDamage(int howMuch)
    {
        CurrentHp -= howMuch;
        if (CurrentHp < 0) CurrentHp = 0;

        FindObjectOfType<HPDisplayManager>().UpdateHP(this);

        if (CurrentHp <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(BlinkEffect());
            Vector2 pushDirection = GetPushbackDirection();
            if (!isPushedBack)
            {
                StartCoroutine(PushBack(pushDirection));
            }
        }
    }

    private IEnumerator BlinkEffect()
    {
        float blinkDuration = 2f;
        float elapsedTime = 0f;
        bool spriteVisible = true;

        while (elapsedTime < blinkDuration)
        {
            spriteRenderer.enabled = spriteVisible;
            spriteVisible = !spriteVisible;
            yield return new WaitForSeconds(0.1f);
            elapsedTime += 0.1f;
        }

        spriteRenderer.enabled = true; 
    }

    private Vector2 GetPushbackDirection()
    {
        if (spriteRenderer.sprite != null)
        {
            if (System.Array.Exists(walkUpSprites, sprite => sprite == spriteRenderer.sprite))
            {
                return Vector2.down;
            }
            else if (System.Array.Exists(walkDownSprites, sprite => sprite == spriteRenderer.sprite))
            {
                return Vector2.up;
            }
            else if (System.Array.Exists(walkLeftSprites, sprite => sprite == spriteRenderer.sprite))
            {
                return Vector2.right;
            }
            else if (System.Array.Exists(walkRightSprites, sprite => sprite == spriteRenderer.sprite))
            {
                return Vector2.left;
            }
        }

        return Vector2.zero; 
    }

    private IEnumerator PushBack(Vector2 pushDirection)
    {
        isPushedBack = true;
        float pushDuration = 0.7f; 
        float elapsedTime = 0f;
        float pushSpeed = 15f; 

        while (elapsedTime < pushDuration)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, pushDirection, pushSpeed * Time.deltaTime);

            if (hit.collider != null)
            {
                break;
            }

            // rb.MovePosition(rb.position + pushDirection * pushSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isPushedBack = false;
        // movement = Vector2.zero; 
    }

    public virtual void Die()
    {
        gameManager.GameOver();
        Debug.Log($"{gameObject.name} has died.");
    }

    public virtual void ApplyDamage(IDamagable damagable)
    {
        damagable.TakeDamage(1); 
    }
}
