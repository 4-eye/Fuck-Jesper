using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // For changing scenes

public class GridMovement : MonoBehaviour
{
    public GridManager gridManager;
    public SpawnCharacter spawnCharacter;
    public float moveSpeed = 1f;
    public int[] characterGridPosition;
    public float speed;
    public Vector3 characterAbsolutePosition, targetPosition;
    public Animator Anime;
    bool moving = false;

    private float startTimeStamp;
    private float elapsedTime;

    private void Start()
    {
        Anime = GetComponent<Animator>();

        // Check for valid GridManager reference
        if (gridManager == null)
        {
            Debug.LogError("GridMovement: Missing reference to GridManager!");
            return;
        }

        // Proceed with execution after GridManager's Start() finishes
        int[] gridPosition = spawnCharacter.GetSpawnPosition();

        Vector3 newPosition = gridManager.GetGridPosition(gridPosition[0], gridPosition[1]);
        transform.position = newPosition;

        // save absolute position to variable
        characterAbsolutePosition = newPosition;

        // Save grid position
        characterGridPosition = gridPosition;

    }

    void Update()
    {

        if (moving) 
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) )
        {
            Anime.SetTrigger("GOJO_up");
            // Record the start time stamp
            startTimeStamp = Time.time;
            Move(0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Anime.SetTrigger("GOJO_right");
            Move(1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Anime.SetTrigger("GOJO_down");
            Move(0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Anime.SetTrigger("GOJO_left");
            Move(-1, 0);
        }
        else
        {
            Anime.SetTrigger("GOJO_Idle");
        }

    }

    void Move(int x, int y)
    {
        int newX = characterGridPosition[0] + x;
        int newY = characterGridPosition[1] + y;

        // Get grid size from GridManager
        int gridSizeX = gridManager.gridSizeX;
        int gridSizeY = gridManager.gridSizeY;

        // Boundary check using grid size
        if ((newX == 0 || newX == gridSizeX - 1) || (newY == 0 || newY == gridSizeY - 1))
        {
            return;
        }

        // Raycast for collision detection
        Vector3 currentGridPosition = gridManager.GetGridPosition(characterGridPosition[0], characterGridPosition[1]);
        Vector3 targetGridPosition = gridManager.GetGridPosition(newX, newY);
        float raycastLength = gridManager.cellSize; // Adjust if walls have different thickness

        RaycastHit2D hit = Physics2D.Raycast(currentGridPosition, targetGridPosition - currentGridPosition, raycastLength);

        // Check for collision with a wall (assuming "Wall" layer and tag)
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Wall"))
        {

            // Debug.Log("Collision detected!");

            return; // Prevent movement if there's a collision
        }



        // Update position logic if no collision
        characterGridPosition[0] = newX;
        characterGridPosition[1] = newY;

        if (newX == 2 && newY == 10) 
        {
            SceneManager.LoadScene(2);
        }
        targetPosition = gridManager.GetGridPosition(newX, newY);

        //Vector3 nextPosition = Vector3.Lerp(characterAbsolutePosition, targetPosition, Time.deltaTime * speed);
        //transform.position = nextPosition;

        moving = true;

       

        // save absolute position to variable
        //characterAbsolutePosition = targetPosition;

        // Debug.Log("Balls");
        // Debug.Log(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (moving)
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        if (transform.position == targetPosition)
            moving = false;
    }


}
