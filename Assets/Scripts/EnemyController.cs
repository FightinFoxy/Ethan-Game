using UnityEngine;

public class EnemyController : MonoBehaviour
{
   

    // Base values can be edited in Unity Editor
    [SerializeField] private EnemyData enemyData;

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
        transform.Translate(Vector3.left * enemyData.moveSpeed * Time.deltaTime);

        if (transform.position.x < -0.5f)
        {
            GameManager.Instance.TriggerGameOver();

            WaveManager waveManager = FindFirstObjectByType<WaveManager>();
            if(waveManager != null)
                waveManager.SetGameOver();

            Destroy(gameObject);
            return;
        }

        // Check for tower directly ahead
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.6f);
        foreach (Collider hit in hits){
           TowerHealth tower = hit.GetComponent<TowerHealth>();
           if (tower != null)
            {
                // Only attack towers that are in the same lane
                float zDifference = Mathf.Abs(hit.transform.position.z - transform.position.z);
                if (zDifference > 0.6f)
                    continue;   //Skip this tower, different lane

                    
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
            Debug.Log ("Target Tower dead, switching to walking");
            return;
        }

        attackTimer += Time.deltaTime;
        if (attackTimer >=1f / enemyData.attackRate)
        {
            targetTower.TakeDamage(enemyData.attackDamage);
            attackTimer=0f;
        }
    }

    public void Die()
    {
        currentState = enemyState.Dead;
        Destroy(gameObject);
    }

    public void Initialise(EnemyData data)
    {
        enemyData = data;
        GetComponent<EnemyHealth>().Initialise(data.maxHealth);
    }
}
