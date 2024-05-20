using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void OpenLevel(int levelId)
    {
        string levelName = "Level_" + levelId;
        SceneManager.LoadScene(levelName);
    }

}
