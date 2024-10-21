using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathScript : MonoBehaviour
{
    public Collider cameraCollider;
    public Rigidbody rb;
    public PlayerCamera playerCamera;
    public static PlayerDeathScript Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void ActiveDeath()
    {
        cameraCollider.enabled = true;
        rb.isKinematic = false;
        playerCamera.enabled = false;
        cameraCollider.isTrigger = false;

        rb.angularVelocity = new Vector3(0, 0, 20f);    
    }
}
