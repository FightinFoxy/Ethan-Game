using UnityEngine;

public class TowerPlacer : MonoBehaviour
{

    //Declarations
    [SerializeField] private GameObject towerPrefab; 
    private Camera mainCamera;
    private GridManager gridManager;


    
    void Start()
    {
        mainCamera = Camera.main;
        gridManager = FindObjectOfType<GridManager>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

           Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
           RaycastHit hit;

           if (Physics.Raycast(ray, out hit))
            {
                Vector2Int gridPos = gridManager.WorldToGrid(hit.point);
                int col = gridPos.x;
                int row = gridPos.y;

                if(gridManager.IsPlaceable(col, row))
                {
                    Vector3 spawnPos = gridManager.GridToWorld(col, row);
                    Instantiate(towerPrefab, spawnPos, Quaternion.identity);

                    gridManager.OccupyCell(col, row);
                }
                else
                {
                    Debug.Log("Cannot Place, out of bounds or already occupied");
                }
            }

        }
        
    }
}
