using UnityEngine;
using TMPro;

public class TowerPlacer : MonoBehaviour
{

    //Declarations
    [SerializeField] private GameObject towerPrefab; 
    [SerializeField] private int towerCost = 100;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private float feedbackDuration = 2f;
    private Camera mainCamera;
    private GridManager gridManager;
    private float feedbackTimer = 0f;
    private bool showingFeedback = false;


    
    void Start()
    {
        mainCamera = Camera.main;
        gridManager = FindObjectOfType<GridManager>();
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
                if(feedbackText != null) feedbackText.gameObject.SetActive(false);
            }
        }


        if (Input.GetMouseButtonDown(0))
        {

           Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
           RaycastHit hit;

           if (Physics.Raycast(ray, out hit))
            {
                // If player clicks on currency drop, ignore it for placement purposes
                if(hit.collider.GetComponent<CurrencyDrop>() != null)
                {
                    return;
                }
                Vector2Int gridPos = gridManager.WorldToGrid(hit.point);
                int col = gridPos.x;
                int row = gridPos.y;

                if(gridManager.IsPlaceable(col, row))
                {
                    if (CurrencyManager.Instance.TrySpendCurrency(towerCost))
                    {
                    Vector3 spawnPos = gridManager.GridToWorld(col, row);
                    Instantiate(towerPrefab, spawnPos, Quaternion.identity);
                    gridManager.OccupyCell(col, row);
                    }
                    else
                    {
                        ShowFeedback("Not enough currency!");
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
}
