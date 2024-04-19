using UnityEngine;


public class SpawnCharacter : MonoBehaviour
{
    [SerializeField] private int SpawnPositionX;
    [SerializeField] private int SpawnPositionY;
    
    public int[] GetSpawnPosition() 
    {
        int[] gridPosition = { SpawnPositionX, SpawnPositionY };
        return gridPosition;
    }
}
