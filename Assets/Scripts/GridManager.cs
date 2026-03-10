using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Declare Variables
    [SerializeField] private int columns;
    [SerializeField] private int rows;
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

       // Old Testing to check if WorldtoGrid works WorldToGrid(new Vector3(3.7f, 0, 2.1f));
        
    }

    void WorldToGrid(Vector3 worldPosition)
    {
        int col = Mathf.FloorToInt(worldPosition.x);
        int row = Mathf.FloorToInt(worldPosition.z);
        
        Debug.Log("Col: " + col + " Row: " + row);  // Test Functionality

    }

   
}
