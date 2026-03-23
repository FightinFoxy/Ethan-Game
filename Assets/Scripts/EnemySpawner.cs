using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Basic Enemy Spawner, will be updated and changed to waves system when levels are implemented 
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int laneRow = 2;
    [SerializeField] private float spawnX = 9.5f;
    [SerializeField] private float spawnDelay = 3f;

    private GridManager gridManager;


    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();

        // Schedule first enemy spawn
        Invoke("SpawnEnemy", spawnDelay);
        
    }

    private void SpawnEnemy()
    {
        // Use grid manager to find correct Z position for spawn
        Vector3 lanePos = gridManager.GridToWorld(0, laneRow);

        // Force spawn to be at edge 
        Vector3 spawnPosition = new Vector3(spawnX, 0.4f, lanePos.z);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Enemy Spawned in lane " + laneRow);
    }

}
