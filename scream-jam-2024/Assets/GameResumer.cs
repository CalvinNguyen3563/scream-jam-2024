using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResumer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseMenu;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResumeGame()
    {
        // Hide the pause menu and resume the game
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Resume game time
    }
}
