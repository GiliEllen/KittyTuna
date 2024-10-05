using UnityEngine;

public abstract class Trap : MonoBehaviour, IDamagable
{
    public LayerMask whatIDamage; 

    public abstract void ApplyDamage(IDamagable damagable);

    public void TakeDamage(int amount)
    {
        Debug.Log($"{gameObject.name} took {amount} damage.");
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
