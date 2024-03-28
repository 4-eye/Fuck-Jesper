using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public GridManager gridManager;
    public float moveSpeed = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(1, 0);
        }
    }

    void Move(int x, int y)
    {
        Vector3 newPosition = gridManager.GetGridPosition(Mathf.RoundToInt(transform.position.x / gridManager.cellSize) + x, Mathf.RoundToInt(transform.position.y / gridManager.cellSize) + y);
        transform.position = newPosition;
    }
}
