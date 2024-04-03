using UnityEngine;
using System.Collections;

public class GridMovement : MonoBehaviour
{
    public GridManager gridManager;
    public float moveSpeed = 1f;
    public int[] characterPossition;

    private void Start()
    {  
        // Proceed with execution after GridManager's Start() finishes
        int[] gridPosition = {3, 1};
        Vector3 newPosition = gridManager.GetGridPosition(gridPosition[0], gridPosition[1]);
        transform.position = newPosition;

        // Save grid position
        characterPossition = gridPosition;     
    }
      

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
        Vector3 newPosition = gridManager.GetGridPosition(characterPossition[0]+x, characterPossition[1]+y);
        transform.position = newPosition;

        // save grid position
        characterPossition[0] += x;
        characterPossition[1] += y;
    }
}
