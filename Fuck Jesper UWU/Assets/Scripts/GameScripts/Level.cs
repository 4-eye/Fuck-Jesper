using UnityEngine;
using UnityEngine.SceneManagement; // For changing scenes

public class Level : MonoBehaviour
{
    [SerializeField] private int LevelSceneNum;
    
    public void Switch()
    {
        SceneManager.LoadScene(LevelSceneNum);
    }

    public void Switch(int LevelSceneNum)
    {
        SceneManager.LoadScene(LevelSceneNum);
    }
}
