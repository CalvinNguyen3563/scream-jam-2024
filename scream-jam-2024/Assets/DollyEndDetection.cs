using UnityEngine;

public class DollyCameraController : MonoBehaviour
{
    public Canvas canvas;  // Assign your Canvas in the Inspector

    void Start()
    {
        canvas.enabled = false;  // Hide the canvas initially
    }

    // This function will be triggered by the animation event
    public void OnDollyComplete()
    {
        canvas.enabled = true;  // Show the canvas when animation finishes
        Debug.Log("Dolly animation complete!");
    }
}
