using UnityEngine;
using TMPro;
public class CurrencyManager : MonoBehaviour
{
    // Any Script can access CurrencyManager instance directly
    public static CurrencyManager Instance {get; private set; }

    [SerializeField] private int startingCurrency = 50;
    [SerializeField] private TextMeshProUGUI currencyCounterText;
    
    private int currentCurrency;

    void Awake()
    {
        // Singleton setup, destroy if one is already in use
        if (Instance != null && Instance != this){
            Destroy(gameObject);
            return;
        }
        Instance = this;
    } 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCurrency = startingCurrency;
        UpdateUI();
    }

    public bool TrySpendCurrency(int cost)
    {
        if (currentCurrency >= cost)
        {
            currentCurrency -= cost;
            UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("Not enough currency! Need " + cost + ", have " + currentCurrency);
            return false;
        }
    }

    public void AddCurrency(int amount)
    {
        currentCurrency += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (currencyCounterText != null)
            currencyCounterText.text = "Currency: " + currentCurrency;
    }
}
