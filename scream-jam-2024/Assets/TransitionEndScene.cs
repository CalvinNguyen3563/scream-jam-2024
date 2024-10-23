using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionEndScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FinalScene()
    {
        SceneManager.LoadScene(3);
    }
}
