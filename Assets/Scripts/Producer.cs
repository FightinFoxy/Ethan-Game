using UnityEngine;

public class Producer : MonoBehaviour
{
    [SerializeField] private float productionInterval = 10f;
    [SerializeField] private int currencyProduced = 25;

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
        CurrencyManager.Instance.AddCurrency(currencyProduced);
        Debug.Log("Producer made " + currencyProduced + " currency!");
        // Add Visual Drop for the player to click 
    }
}
