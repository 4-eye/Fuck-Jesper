using UnityEngine;
using System.Collections;

public class GridMovement : MonoBehaviour
{
    public GridManager gridManager;
    public float moveSpeed = 1f;
    public int[] characterPosition;

    private void Start()
    {
        // Check for valid GridManager reference
        if (gridManager == null)
        {
            Debug.LogError("GridMovement: Missing reference to GridManager!");
            return;
        }

        // Proceed with execution after GridManager's Start() finishes
        int[] gridPosition = { 3, 1 };
        Vector3 newPosition = gridManager.GetGridPosition(gridPosition[0], gridPosition[1]);
        transform.position = newPosition;

        // Save grid position
        characterPosition = gridPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) )
        {
            Move(0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Move(0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Move(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Move(1, 0);
        }
    }

    void Move(int x, int y)
    {
        int newX = characterPosition[0] + x;
        int newY = characterPosition[1] + y;

        // Get grid size from GridManager
        int gridSizeX = gridManager.gridSizeX;
        int gridSizeY = gridManager.gridSizeY;

        // Boundary check using grid size
        if ((newX == 0 || newX == gridSizeX - 1) || (newY == 0 || newY == gridSizeY - 1))
        {
            return;
        }

        // Raycast for collision detection
        Vector3 currentGridPosition = gridManager.GetGridPosition(characterPosition[0], characterPosition[1]);
        Vector3 targetGridPosition = gridManager.GetGridPosition(newX, newY);
        float raycastLength = gridManager.cellSize; // Adjust if walls have different thickness

        RaycastHit2D hit = Physics2D.Raycast(currentGridPosition, targetGridPosition - currentGridPosition, raycastLength);

        // Check for collision with a wall (assuming "Wall" layer and tag)
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Collision detected!");
            return; // Prevent movement if there's a collision
        }

        // Update position logic if no collision
        characterPosition[0] = newX;
        characterPosition[1] = newY;
        Vector3 newPosition = gridManager.GetGridPosition(newX, newY);
        transform.position = newPosition;
    }

   
}
