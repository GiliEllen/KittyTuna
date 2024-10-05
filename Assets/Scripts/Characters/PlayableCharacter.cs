using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public abstract class PlayableCharacter : MonoBehaviour, IDamagable
{
    public float speed;
    protected Vector2 movement;

    public int maxHP = 3;  // Maximum HP
    public int CurrentHp { get; private set; }

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
        Debug.Log($"Initial HP: {CurrentHp}");
    }

    protected virtual void Start()
    {   
        if (CurrentHp <= 0) 
        {
            CurrentHp = maxHP;
        }
        Debug.Log($"start HP: {CurrentHp}");
        FindObjectOfType<HPDisplayManager>().UpdateHP(this);
    }

    public void SetHP(int hp)
    {
        CurrentHp = hp;
        Debug.Log($"Setting HP to: {CurrentHp}");
        FindObjectOfType<HPDisplayManager>().UpdateHP(this); 
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

    public void TakeDamage(int howMuch)
    {
        Debug.Log($"Current HP before damage: {CurrentHp}");
        CurrentHp -= howMuch;
        Debug.Log($"Current HP after damage: {CurrentHp}");
        if (CurrentHp < 0) CurrentHp = 0;

        FindObjectOfType<HPDisplayManager>().UpdateHP(this);

        if (CurrentHp <= 0)
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
