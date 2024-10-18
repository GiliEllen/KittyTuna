using UnityEngine;

public abstract class Trap : MonoBehaviour, IDamagable
{
    public LayerMask whatIDamage; 

    public abstract void ApplyDamage(IDamagable damagable);
    public int maxHP = 1;
    public int CurrentHp { get; private set; }

    protected virtual void Start()
    {   
        if (CurrentHp <= 0) 
        {
            CurrentHp = maxHP;
        }
    }

    public void TakeDamage(int howMuch)
    {
        Debug.Log($"Current HP before damage: {CurrentHp}");
        CurrentHp -= howMuch;
        Debug.Log($"Current HP after damage: {CurrentHp}");
        if (CurrentHp < 0) CurrentHp = 0;

        if (CurrentHp <= 0)
        {
            Die();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {   
        if (((1 << collision.gameObject.layer) & whatIDamage) != 0)
        {
            IDamagable damagable = collision.GetComponent<IDamagable>();
            if (damagable != null)
            {
                ApplyDamage(damagable);
            }
        }
    }

    public virtual void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
    }
}
