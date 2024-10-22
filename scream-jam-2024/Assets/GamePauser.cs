using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauser : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused;
    public CinemachineBrain brain;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        brain = WorldGameObjectStorage.Instance.brain;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !WorldGameObjectStorage.Instance.player.isDead)
        {
            if (isPaused)
            {
                ResumeGame();
                
            }
            else
            {
                PauseGame();
                
            }
        }
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            // Show the pause menu and pause the game
            pauseMenu.SetActive(true);
            Time.timeScale = 0f; // Stop the game time
            isPaused = true;
            brain.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            // Hide the pause menu and resume the game
            pauseMenu.SetActive(false);
            Time.timeScale = 1f; // Resume game time
            isPaused = false;
            brain.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
