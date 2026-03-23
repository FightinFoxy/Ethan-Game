using UnityEngine;

public class CurrencyDrop : MonoBehaviour
{
    [SerializeField] private int currencyValue = 25;
    [SerializeField] private float fallSpeed = 2f;
    [SerializeField] private float lifetime = 10f; // Disappear if not collected

    private float lifetimeTimer = 0f;
    private bool collected = false;

    void Update()
    {
        // Fall down
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Destory if uncollected in time
        lifetimeTimer += Time.deltaTime;
        if (lifetimeTimer >= lifetime)
        {
            Destroy(gameObject);
        }
        
    }

    // Called when player clicks currency drop
    void OnMouseDown()
    {
        if (!collected){
            collected = true;
            CurrencyManager.Instance.AddCurrency(currencyValue);
            Debug.Log("Collected " + currencyValue + "currency!");
            Destroy(gameObject);
        }
    }


}
