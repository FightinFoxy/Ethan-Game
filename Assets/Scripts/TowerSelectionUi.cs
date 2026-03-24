using UnityEngine;
using UnityEngine.UI;
using TMPro; 
public class TowerSelectionUi : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonContainer;

    private Button[] towerButtons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        BuildUI();
    }

    private void BuildUI()
    {
        TowerData[] towers = TowerSelector.Instance.GetAvailableTowers();
        towerButtons = new Button[towers.Length];

        for (int i = 0; i < towers.Length; i++)
        {
            // Capture index
            int index = i;
            TowerData tower = towers[i];

            // Instantiate a button from the prefab
            GameObject buttonObj = Instantiate(buttonPrefab, buttonContainer);
            Button button = buttonObj.GetComponent<Button>();
            towerButtons[i] = button;

            // Set button label to tower name and cost
            TextMeshProUGUI label = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            if (label != null)
                label.text = tower.towerName + "\n" + tower.cost;

            //Set icon if available
            Image icon = buttonObj.transform.Find("Icon")?.GetComponent<Image>();
            if (icon != null && tower.icon != null)
                icon.sprite = tower.icon;

            button.onClick.AddListener(()=> TowerSelector.Instance.SelectTower(tower));
        }
    }

}
