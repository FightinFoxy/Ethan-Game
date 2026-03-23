using UnityEngine;

public class CurrencySpawner : MonoBehaviour
{
    [SerializeField] private GameObject currencyDropPrefab;
    [SerializeField] private float spawnInterval = 5f; // Seconds between drops
    [SerializeField] private float spawnHeight = 8f; //Y Position to spawn
    [SerializeField] private float minX = 0.5f; // Left Edge of gamestage
    [SerializeField] private float maxX = 8.5f; // Right edge of gamestage
    [SerializeField] private float minZ = 0.5f; // Front edge of gamestage
    [SerializeField] private float maxZ = 4.5f; // Back edge of gamestage

    private float spawnTimer = 0f;
    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if(spawnTimer >= spawnInterval)
        {
            spawnCurrencyDrop();
            spawnTimer = 0f;
        }
        
    }

    private void spawnCurrencyDrop()
    {
        // Pick random position over gamestage
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 spawnPos = new Vector3(randomX, spawnHeight, randomZ);
        Instantiate(currencyDropPrefab, spawnPos, Quaternion.identity);
        Debug.Log("Currency Dropped at:" + spawnPos);
    }
}
