using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Enemy Types")]
    [SerializeField] private List<EnemyData> enemyTypes;  

    [Header("Wave Settings")]
    [SerializeField] private int totalWaves = 3;
    [SerializeField] private float timeBetweenWaves = 12f;
    [SerializeField] private float timeBetweenSpawns = 2f;
    [SerializeField] private float spawnX = 9.5f;
    [SerializeField] private int basePointBudget = 6;    // wave 1 budget
    [SerializeField] private int budgetIncreasePerWave = 4; // each wave gets harder

    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool gameOver = false;

    private GridManager gridManager;

    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        StartCoroutine(RunWaves());
    }

    private IEnumerator RunWaves()
    {
        yield return new WaitForSeconds(50f);

        for (int wave = 1; wave <= totalWaves; wave++)
        {
            if (gameOver) yield break;

            currentWave = wave;
            int budget = basePointBudget + (wave - 1) * budgetIncreasePerWave;

            Debug.Log("Wave " + wave + " starting. Budget: " + budget);

            List<EnemyData> waveEnemies = BuildWaveFromBudget(budget);

            yield return StartCoroutine(SpawnWave(waveEnemies));
            yield return new WaitUntil(() => enemiesAlive <= 0);

            if (wave < totalWaves)
            {
                Debug.Log("Wave cleared! Next wave in " + timeBetweenWaves + "s");
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        if (!gameOver)
            GameManager.Instance.TriggerWin();
    }

    private List<EnemyData> BuildWaveFromBudget(int budget)
{
    List<EnemyData> result = new List<EnemyData>();
    int remaining = budget;

    int safetyLimit = 100;
    while (remaining > 0 && safetyLimit > 0)
    {
        safetyLimit--;

        // Only enemies affordable enemies and ones unlocked for current wave
        List<EnemyData> affordable = enemyTypes.FindAll(e => 
            e.pointCost <= remaining && e.minimumWave <= currentWave);
        if (affordable.Count == 0) break;

        // Weighted random selection
        int totalWeight = 0;
        foreach (EnemyData e in affordable) totalWeight += e.spawnWeight;

        int roll = Random.Range(0, totalWeight);
        int cumulative = 0;
        EnemyData chosen = affordable[0];

        foreach (EnemyData e in affordable)
        {
            cumulative += e.spawnWeight;
            if (roll < cumulative)
            {
                chosen = e;
                break;
            }
        }

        result.Add(chosen);
        remaining -= chosen.pointCost;
    }

    return result;
}

    private IEnumerator SpawnWave(List<EnemyData> enemies)
    {
        foreach (EnemyData data in enemies)
        {
            if (gameOver) yield break;

            int laneRow = Random.Range(0, 5);
            Vector3 lanePos = gridManager.GridToWorld(0, laneRow);
            Vector3 spawnPos = new Vector3(spawnX, 0.4f, lanePos.z);

            GameObject enemy = Instantiate(data.prefab, spawnPos, Quaternion.identity);

            // Initialise the enemy with its stats from EnemyData
            EnemyController controller = enemy.GetComponent<EnemyController>();
            if (controller != null) controller.Initialise(data);

            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            if (health != null) health.OnDeath += HandleEnemyDeath;

            enemiesAlive++;
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private void HandleEnemyDeath()
    {
        enemiesAlive--;
    }

    public void SetGameOver()
    {
        gameOver = true;
    }


    public int GetCurrentWave() { return currentWave; }
    public int GetTotalWaves() { return totalWaves; }
}