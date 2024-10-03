using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

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
            StartCoroutine(PlayWalkAnimation()); 
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

        if (currentWalkSprites != null)
        {
            for (int i = 0; i < currentWalkSprites.Length; i++)
            {
                spriteRenderer.sprite = currentWalkSprites[i];
                yield return new WaitForSeconds(animationFrameDuration);
            }
        }
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
