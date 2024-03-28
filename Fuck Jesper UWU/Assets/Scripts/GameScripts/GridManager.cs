using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject stage; // Reference to the stage GameObject
    public int gridSizeX, gridSizeY;
    private Vector3[,] grid; // Corrected to Vector3
    private float cellSize;

    void Start()
    {
        // Calculate cell size by dividing the stage's length and width by the grid size
        float stageLength = stage.transform.localScale.x;
        float stageWidth = stage.transform.localScale.y;
        cellSize = Mathf.Min(stageLength / gridSizeX, stageWidth / gridSizeY);
        Debug.Log(stageLength);
        Debug.Log(stageWidth);

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
        return grid[x, y];
    }

    void OnDrawGizmos()
    {
        if (grid != null)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Gizmos.DrawWireCube(grid[x, y], new Vector3(cellSize, cellSize, 0));
                }
            }
        }
    }

}
