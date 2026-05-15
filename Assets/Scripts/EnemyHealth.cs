using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    public event Action OnDeath;
    [SerializeField] private AudioClip deathSound;
    
    void Start()
    {
        currentHealth = maxHealth;
        
    }



    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage. Health: " + currentHealth);

        if(currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        AudioManager.Instance.PlaySound(deathSound);

        OnDeath?.Invoke();
        EnemyController controller = GetComponent<EnemyController>();
        if (controller != null)
            controller.Die();
        else
            Destroy(gameObject);
    }

    public void Initialise(float health)
{
    maxHealth = health;
    currentHealth = health;
}
}
