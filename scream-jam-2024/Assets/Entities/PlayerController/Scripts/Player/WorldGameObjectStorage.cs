using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGameObjectStorage : MonoBehaviour
{
    public static WorldGameObjectStorage Instance;
    public PlayerManager player;
    public GameObject cameraHolder;
    public GameObject mainCam;
    public CinemachineBrain brain;
    public AudioSource playerSrc;
    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }
}
