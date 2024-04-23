using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private int EndLevelPositionX;
    [SerializeField] private int EndLevelPositionY;
    
    public bool GetEndLevelPosition(int positionX, int positionY) 
    {
        if (positionX == EndLevelPositionX && positionY == EndLevelPositionY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
