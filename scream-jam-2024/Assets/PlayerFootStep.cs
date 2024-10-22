using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootStep : MonoBehaviour
{
    public GameObject mainCam;
    public PlayerCamera playerCamera;
    public bool canPlayFootStep = true;

    [Header("AudioClips")]
    public AudioClip dirtFootSteps;
    public AudioClip woodFootSteps;

    public AudioSource src;
    void Start()
    {
        mainCam = WorldGameObjectStorage.Instance.mainCam;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCam.transform.localPosition.y < 0 && canPlayFootStep && playerCamera.currentVirtualCameraIndex > 0 )
        {

            RaycastHit[] colliders = Physics.RaycastAll(WorldGameObjectStorage.Instance.player.transform.position, Vector3.down, 15f);
            Debug.Log(colliders.Length);
            //play step
            bool woodDetected = false;
            foreach (RaycastHit hit in colliders)
            {
                if (hit.collider.CompareTag("Wood"))
                {
                    woodDetected = true;
                }
            }
            
            
            if (woodDetected)
            {
                src.pitch = Random.Range(0.5f, 1f);
                src.PlayOneShot(woodFootSteps);
            }
            else
            {
                Debug.Log("play footstep");
                src.pitch = Random.Range(0.5f, 1f);
                src.PlayOneShot(dirtFootSteps);
            }

            canPlayFootStep = false;
            
            

        }
        else if (mainCam.transform.localPosition.y > 0)
        {
            canPlayFootStep = true; 
        }
    }

    
}
