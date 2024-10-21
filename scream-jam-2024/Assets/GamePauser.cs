using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauser : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused;
    public static GamePauser instance;
    public CinemachineBrain brain;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
                brain.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                PauseGame();
                brain.enabled = false;
                Cursor.lockState = CursorLockMode.None;
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
        }
    }
}
