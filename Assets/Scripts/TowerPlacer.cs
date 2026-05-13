using UnityEngine;
using TMPro;

public class TowerPlacer : MonoBehaviour
{

    //Declarations
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private float feedbackDuration = 2f;
    private Camera mainCamera;
    private GridManager gridManager;
    private float feedbackTimer = 0f;
    private bool showingFeedback = false;
    private float placementCooldown = 0.5f;
    private float lastPlacedTime = -999f;
    private bool destroyMode = false;

    void Start()
    {
        mainCamera = Camera.main;
        gridManager = FindFirstObjectByType<GridManager>();
        if (feedbackText != null) feedbackText.gameObject.SetActive(false);
    }

    void Update()
    {
        //Hide feedback message after duration
        if (showingFeedback)
        {
            feedbackTimer += Time.deltaTime;
            if (feedbackTimer >= feedbackDuration)
            {
                showingFeedback = false;
                if (feedbackText != null) feedbackText.gameObject.SetActive(false);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastPlacedTime < placementCooldown) return;
            lastPlacedTime = Time.time;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (destroyMode)
                {
                    TowerHealth tower = hit.collider.GetComponent<TowerHealth>();
                    if (tower != null)
                    {
                        // Refund Half the cost
                        TowerData data = tower.GetComponent<TowerDataHolder>()?.towerData;
                        if (data != null)
                            CurrencyManager.Instance.AddCurrency(data.cost / 2);

                        gridManager.FreeCell(hit.transform.position);
                        Destroy(tower.gameObject);
                        Debug.Log("Tower Destroyed, currency refunded");
                    }
                    DisableDestroyMode();
                    return;
                }

                // If player clicks on currency drop, ignore it for placement purposes
                if (hit.collider.GetComponent<CurrencyDrop>() != null)
                    return;

                // Read Currently Selected Tower
                TowerData selectedTower = TowerSelector.Instance.SelectedTower;

                if (selectedTower == null)
                {
                    ShowFeedback("No tower selected!");
                    return;
                }

                Vector2Int gridPos = gridManager.WorldToGrid(hit.point);
                int col = gridPos.x;
                int row = gridPos.y;

                if (gridManager.IsPlaceable(col, row))
                {
                    if (CurrencyManager.Instance.TrySpendCurrency(selectedTower.cost))
                    {
                        Vector3 spawnPos = gridManager.GridToWorld(col, row);
                        GameObject placed = Instantiate(selectedTower.prefab, spawnPos, Quaternion.identity);
                        gridManager.OccupyCell(col, row);
                        TowerDataHolder holder = placed.AddComponent<TowerDataHolder>();
                        holder.towerData = selectedTower;
                    }
                    else
                    {
                        ShowFeedback("Not enough currency! Need " + selectedTower.cost);
                    }
                }
                else
                {
                    Debug.Log("Cannot Place, out of bounds or already occupied");
                }
            }
        }
    }

    private void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackText.gameObject.SetActive(true);
            feedbackTimer = 0f;
            showingFeedback = true;
        }
        else
        {
            Debug.Log(message);
        }
    }

    public void EnableDestroyMode()
    {
        destroyMode = true;
        Debug.Log("Destroy Mode ON");
    }

    public void DisableDestroyMode()
    {
        destroyMode = false;
    }
}