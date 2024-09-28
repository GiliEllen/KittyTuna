// PlayableCharacter.cs
using UnityEngine;

public abstract class PlayableCharacter : MonoBehaviour, IDamagable
{
    public float speed;

    public int maxHp;
    protected int currentHp;

    protected virtual void Start()
    {
        currentHp = maxHp;
    }

    public virtual void Movement()
    {

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
