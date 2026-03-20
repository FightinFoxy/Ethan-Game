using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Other states and values cna be implemented in order to have a wider vairety of enemy types

    // Base values can be edited in Unity Editor
    [SerializeField] private float movespeed = 1.5f;
    [SerializeField] private float attackDamage = 1.0f;
    [SerializeField] private float attackRate = 1f;
    [SerializeField] private float detectionRange = 0.6;

    // States
    private enum enemyState { Walking, Attacking, Dead }
    private enemyState currentState = enemyState.Walking;

    // References
    private TowerHealth targetTower;
    private float attackTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case enemyState.Walking:
            Walk();
            break;

            case enemyState.Attacking:
            Attack();
            break;

            case enemyState.Dead:
            break;
        }
        
    }

    private void Walk()
    {
        // Move in negative X direction, right to left
        transform.Translate(Vector3.left * movespeed * Time.deltaTime);

        // Check for tower directly ahead
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider hit in hits){
           TowerHealth tower = hit.GetComponent<TowerHealth>();
           if (tower != null)
            {
                // Found Tower Switch to attack mode
                targetTower = tower;
                currentState = enemyState.Attacking;
                Debug.Log("Enemy found a tower, switching to Attacking");
                return;
            } 
        }
    }

    private void Attack()
    {
        if (targetTower == null)
        {
            currentState = enemyState.Walking;
            Debug.log ("Target Tower dead, switching to walking");
        }

        attackDamageTimer += Time.deltaTime;
        if (attackTimer >=1f / attackRate)
        {
            targetTower.TakeDamage(attackDamage);
            attaclTimer=0f;
        }
    }

    public void Die()
    {
        currentState = enemyState.Dead;
        Destroy(gameObject);
    }
}
