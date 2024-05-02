using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_manager : MonoBehaviour
{

    public TMP_Text Keys_TextMeshPro;
    int keyCounter = 0;
    
    public void AddKeyUI()
    {
        
        keyCounter += 1;
        Keys_TextMeshPro.text = keyCounter.ToString();
    }

    public void DeleteKeyUI()
    {
        keyCounter -= 1;
        Keys_TextMeshPro.text = keyCounter.ToString();
    }


}
