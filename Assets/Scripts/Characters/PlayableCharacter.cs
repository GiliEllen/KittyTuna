// PlayableCharacter.cs
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayableCharacter : MonoBehaviour, IDamagable
{
    public int speed;
    protected Vector2 movement;

    public int maxHp;
    protected int currentHp;

    protected virtual void Start()
    {
        currentHp = maxHp;
    }

    public virtual void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
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
