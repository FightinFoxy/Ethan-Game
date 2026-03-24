using UnityEngine;

public class Producer : MonoBehaviour
{
    [SerializeField] private float productionInterval = 10f;
    [SerializeField] private int currencyProduced = 25;
    [SerializeField] private GameObject currencyDropPrefab;
    [SerializeField] private float dropSpawnHeight = 1.5f;

    private float productionTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        productionTimer += Time.deltaTime;
        
        if (productionTimer >= productionInterval)
        {
            ProduceCurrency();
            productionTimer = 0f;
        }
        
    }

    private void ProduceCurrency()
    {
        if (currencyDropPrefab != null)
        {
            // Spawn Drop slightly above producers position
            Vector3 spawnPos = new Vector3(
                transform.position.x,
                transform.position.y + dropSpawnHeight,
                transform.position.z
            );

            GameObject drop = Instantiate(currencyDropPrefab, spawnPos, Quaternion.identity);
            CurrencyDrop dropscript = drop.GetComponent<CurrencyDrop>();
            if (dropscript != null)
                dropscript.SetFallSpeed(0f);
            Debug.Log ("Producer produced currency!");
        }
        else
        {
            Debug.LogWarning("No prefab assigned for producer!");
        }

    }
}
