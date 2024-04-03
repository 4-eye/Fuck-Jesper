using UnityEngine;

public class GridManager : MonoBehaviour
{ 
    public GameObject stage; // Reference to the stage GameObject
    public int gridSizeX, gridSizeY;
    private Vector3[,] grid; // Corrected to Vector3
    public float cellSize;

    void Awake()
    {
        // Calculate cell size by dividing the stage's length and width by the grid size
        float stageLength = stage.transform.localScale.x;
        float stageWidth = stage.transform.localScale.y;
        cellSize = Mathf.Min(stageLength / gridSizeX, stageWidth / gridSizeY);

        grid = new Vector3[gridSizeX, gridSizeY];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // Adjust the grid position to account for the sprite's pivot point
                float adjustedX = x * cellSize - stageLength / 2 + cellSize / 2;
                float adjustedY = y * cellSize - stageWidth / 2 + cellSize / 2;
                grid[x, y] = new Vector3(adjustedX, adjustedY, 0);
            }
        }
    }

    public Vector3 GetGridPosition(int x, int y)
    {
        Vector3 position = grid[x, y];
        position.z -= 1; // Decrease the z value by 1
        return position;
    }

    // showing grid for debuggin purposes
    // void OnDrawGizmos()
    // {
    //     if (grid != null)
    //     {
    //         for (int x = 0; x < gridSizeX; x++)
    //         {
    //             for (int y = 0; y < gridSizeY; y++)
    //             {
    //                 Gizmos.DrawWireCube(grid[x, y], new Vector3(cellSize, cellSize, 0));
    //             }
    //         }
    //     }
    // }

}
