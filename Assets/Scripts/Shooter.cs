using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject shooterPrefab;
    [SerializeField] private float fireRate = 1.5f;
    [SerializeField] private float detectionRange = 9f;

    [SerializeField] private float laneThreshold = 0.6f;

    private float fireTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= 1f / fireRate)
        {
            if (EnemyInLane()){
                Shoot();
                fireTimer = 0f;
            }
        }
        
    }

    private bool EnemyInLane()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider hit in hits)
        {
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                // Same lane check
                float zDiff = Mathf.Abs(hit.transform.position.z - transform.position.z);
                if(zDiff <= laneThreshold)
                    return true;

            }
        }
        return false;
    }
    private void Shoot()
    {
        if (shooterPrefab != null)
        {
            // Spawn bullet slightly infront of shooter
            Vector3 spawnPos = transform.position + Vector3.right * 0.6f + Vector3.up * 0.4f;
            Instantiate(shooterPrefab, spawnPos, Quaternion.identity);
            Debug.Log("Bullet Fired!");
        }
    }
}
