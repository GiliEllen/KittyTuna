using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayableCharacter : MonoBehaviour, IDamagable
{
    public float speed;
    protected Vector2 movement;

    public int maxHp;
    protected int currentHp;

    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        currentHp = maxHp;
        rb = GetComponent<Rigidbody2D>(); 
    }

    public virtual void OnMovement(InputValue value)
    {
        Debug.Log($"Movement Input: {movement}"); 
        movement = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
         Debug.Log("Movement Input: " + movement);
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
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
