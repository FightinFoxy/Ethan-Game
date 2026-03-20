using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    [SerializeField] private float maxHelath = 100f;
    private float currentHealth;
    void Start()
    {
        currentHealth = maxHelath;
        
    }

    // Call on every attack tick
    public void TakeDamage (float amount)
    {
        currentHealth -=amount;
        Debug.Log(gameObject.name + " took " + amount + " damage. Health: " + currentHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        Destroy(gameObject);
    }
}
