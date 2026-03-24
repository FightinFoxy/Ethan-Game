using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    public static TowerSelector Instance { get; private set; }

    [SerializeField] private TowerData[] availableTowers; // Add available towers here

    public TowerData SelectedTower {get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    void Start()
    {
        // Default to first tower on startup
        if(availableTowers.Length > 0)
            SelectTower(availableTowers[0]);
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check hotkeys for each available tower
        for(int i = 0; i < availableTowers.Length; i++)
        {
            if(Input.GetKeyDown(availableTowers[i].hotkey))
            {
                SelectTower(availableTowers[i]);
            }
        }
    }

        public void SelectTower(TowerData tower)
    {
        SelectedTower = tower;
        Debug.Log("Selected tower: " + tower.towerName);
    }

    public TowerData[] GetAvailableTowers()
    {
        return availableTowers;
    }
        
    
}
