using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // For changing scenes
using UnityEngine.Tilemaps;

public class GridMovement : MonoBehaviour
{
    public GridManager gridManager;
    public SpawnCharacter spawnCharacter;
    public FinishLevel finishLevel;
    public Level level;
    public Tilemap tilemap;
    public TileBase tileToSpawn;
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

        int layerMask = LayerMask.GetMask("Walls");
        RaycastHit2D hitTroughBox = Physics2D.Raycast(currentGridPosition, (targetGridPosition - currentGridPosition) * 1.5f, raycastLength * 1.5f, layerMask);

        // Check for collision with a wall (assuming "Wall" layer and tag)
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Wall"))
        {

            // Debug.Log("Collision detected!");

            return; // Prevent movement if there's a collision
        }
        else if (hit.collider != null && hit.collider.gameObject.CompareTag("MovableBox"))
        {
            if (hitTroughBox.collider != null && hit.collider.gameObject.CompareTag("Wall")) 
            {
                Debug.Log("bs");
                
                return; // Prevent movement if there's wall behind a movable box    
            }

            Debug.Log(hitTroughBox.collider != null && hit.collider.gameObject.CompareTag("Wall"));

            // Determine movement direction
            int moveDirectionX = Mathf.Clamp(x, -1, 1); // Clamp to ensure it's -1, 0, or 1
            int moveDirectionY = Mathf.Clamp(y, -1, 1); // Clamp to ensure it's -1, 0, or 1

            // Convert the individual X and Y directions into a Vector3Int
            Vector3Int direction = new Vector3Int(moveDirectionX, moveDirectionY, 0);

            Vector3Int currentCell = tilemap.WorldToCell(transform.position); // Get current (character's) cell position
            Vector3Int targetCell = currentCell + direction; // Calculate target (box) cell position

            if (tilemap.HasTile(targetCell))
            {
                // Delete box
                tilemap.SetTile(targetCell, null);
                
                // Spawn box on a new place
                tilemap.SetTile(targetCell + direction, tileToSpawn);
                
            }
        }



        // Update position logic if no collision
        characterGridPosition[0] = newX;
        characterGridPosition[1] = newY;

        if (finishLevel.GetEndLevelPosition(newX, newY)) 
        {
            level.Switch();
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
