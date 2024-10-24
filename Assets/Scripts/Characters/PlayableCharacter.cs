using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public abstract class PlayableCharacter : MonoBehaviour, IDamagable
{
    public float speed;
    protected Vector2 movement;

    public int maxHP = 3;
    public int CurrentHp { get; private set; }

    protected Rigidbody2D rb;
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


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log($"Initial HP: {CurrentHp}");
    }

    protected virtual void Start()
    {   
        GameManager gameManager = GameManager.Instance;
        if (CurrentHp <= 0) 
        {
            CurrentHp = maxHP;
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
        GameManager gameManager = GameManager.Instance;
        if (!isWalking || FindObjectOfType<GameManager>().IsGameOver()) return;
        movement = value.Get<Vector2>();

        if (movement != Vector2.zero)
        {
            if (walkAnimationCoroutine == null)
            {
                
                walkAnimationCoroutine = StartCoroutine(PlayWalkAnimation());
            }
        }
        else
        {
            if (walkAnimationCoroutine != null)
            {
                StopCoroutine(walkAnimationCoroutine);
                walkAnimationCoroutine = null; 
            }

            Sprite[] currentWalkSprites = GetCurrentWalkSprites();
            if (currentWalkSprites != null && currentWalkSprites.Length > 0)
            {
                spriteRenderer.sprite = currentWalkSprites[0]; 
            }
        }
    }

    private void FixedUpdate()
    {
        if (movement != Vector2.zero && isWalking)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    private IEnumerator PlayWalkAnimation()
    {
        Sprite[] currentWalkSprites = GetCurrentWalkSprites();
        int index = 0;
        
        while (movement != Vector2.zero)
        {
            if (currentWalkSprites != null && currentWalkSprites.Length > 0)
            {
                spriteRenderer.sprite = currentWalkSprites[index];
                index = (index + 1) % currentWalkSprites.Length; 
                yield return new WaitForSeconds(animationFrameDuration);
            }
            else
            {
                yield break; 
            }

            currentWalkSprites = GetCurrentWalkSprites(); 
        }

        walkAnimationCoroutine = null; 
    }

    private Sprite[] GetCurrentWalkSprites()
    {
        if (!isWalking) return null;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (vertical > 0) 
        {
            return walkUpSprites;
        }
        else if (vertical < 0) 
        {
            return walkDownSprites;
        }
        else if (horizontal < 0) 
        {
            return walkLeftSprites;
        }
        else if (horizontal > 0) 
        {
            return walkRightSprites;
        }

        return null;
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
            GameManager.Instance.GameOver(gameObject.name);
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

            rb.MovePosition(rb.position + pushDirection * pushSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isPushedBack = false;
        movement = Vector2.zero; 
    }

    public virtual void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
    }

    public virtual void ApplyDamage(IDamagable damagable)
    {
        damagable.TakeDamage(1); 
    }
}
