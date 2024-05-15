using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public Canvas canvasToControl;

    // Method to enable the canvas
    public void EnableCanvas()
    {
        if (canvasToControl != null)
        {
            canvasToControl.enabled = true;
        }
    }

    // Method to disable the canvas
    public void DisableCanvas()
    {
        if (canvasToControl != null)
        {
            canvasToControl.enabled = false;
        }
    }
}
