using UnityEngine;
using UnityEngine.Tilemaps;

public class TileDistanceFromCenter : MonoBehaviour
{
    public Tilemap tilemap;

    // Function to calculate the distance of a tile from the center
    public int GetTileDistanceFromCenter(Vector3Int tilePosition)
    {
        // Get the size of the Tilemap
        Vector3Int size = tilemap.size;

        // Calculate the center position of the Tilemap
        Vector3 center = tilemap.transform.position + new Vector3(size.x * 0.5f, size.y * 0.5f, 0f);

        // Calculate the position of the tile
        Vector3 tilePositionWorld = tilemap.CellToWorld(tilePosition);

        // Calculate the distance between the center and the tile
        int distance = Mathf.RoundToInt(Vector3.Distance(center, tilePositionWorld));

        return distance;
    }
}
