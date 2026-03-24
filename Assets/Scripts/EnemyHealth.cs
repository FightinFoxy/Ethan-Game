using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        EnemyController controller = GetComponent<EnemyController>();
        if (controller != null)
            controller.Die();
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
