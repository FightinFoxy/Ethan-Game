using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Declare Variables
    [SerializeField] private int columns = 9;
    [SerializeField] private int rows = 5;
    [SerializeField] private Vector3 gridOrigin = new Vector3(-0.5f, 0f, -0.5f);
    private bool [,] grid;  // Check if Occupied
    void Start()
    {
        // Intialise variables
        grid = new bool[columns, rows];
        
        for (int col = 0; col <columns; col++)  // Set all Grid Spaces to empty
        {
            for(int row = 0; row < rows; row++)
            {
                grid[col, row] = false;
            }
        }

      // WorldToGrid(new Vector3(3.7f, 0, 2.1f));  Old Testing Lines
      // WorldToGrid(new Vector3(99f, 0, 99f));
        
    }

    public Vector2Int WorldToGrid(Vector3 worldPosition)
    {
        int col = Mathf.FloorToInt(worldPosition.x - gridOrigin.x);
        int row = Mathf.FloorToInt(worldPosition.z - gridOrigin.z);

        return new Vector2Int(col, row);

    }

    public Vector3 GridToWorld (int col, int row)
    {
        return new Vector3(
            gridOrigin.x + col + 0.5f,
            gridOrigin.y,
            gridOrigin.z + row + 0.5f
        );
    }

    public bool IsPlaceable(int col, int row)
    {
        if (col < 0 || col >= columns || row < 0 || row >= rows)    //Check if player has clicked within bounds
        {
            Debug.LogWarning("Clicked outside grid bounds!");
            return false;
        }

        return !grid[col,row]; // Only if cell is empty
        
    }    

    // Mark cell as occupied
    public void OccupyCell(int col, int row)
    {
        grid [col, row] = true;
        Debug.Log("Occupied cell - Col: " + col + "Row " + row);
    }

    
    



   
}
