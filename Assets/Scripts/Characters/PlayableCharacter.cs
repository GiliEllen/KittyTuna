using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public abstract class PlayableCharacter : MonoBehaviour, IDamagable
{
    public float speed;
    protected Vector2 movement;

    public int maxHp;
    protected int currentHp;

    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;

    public Sprite[] walkDownSprites;
    public Sprite[] walkUpSprites;
    public Sprite[] walkLeftSprites;
    public Sprite[] walkRightSprites;
    public Sprite[] jumpAnimationSprites;
    public float animationFrameDuration = 0.1f;

    private Coroutine walkAnimationCoroutine; 
    private bool isWalking; 

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        currentHp = maxHp;
    }

    public virtual void OnMovement(InputValue value)
    {
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
        if (movement != Vector2.zero)
        {
            Debug.Log("Movement Input: " + movement);
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

    public abstract void SpecialAbility();

    public virtual void TakeDamage(int howMuch)
    {
        currentHp -= howMuch;
        if (currentHp <= 0)
        {
            Die();
        }
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
