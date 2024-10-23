using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestarter : MonoBehaviour
{
    // This function will be called when the button is pressed.
    public void RestartGame()
    {
        // Reloads the current scene.
        Scene currentScene = SceneManager.GetActiveScene();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(currentScene.name);
    }
}
