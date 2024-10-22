using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathScript : MonoBehaviour
{
    public Collider cameraCollider;
    public Rigidbody rb;
    public PlayerCamera playerCamera;
    public static PlayerDeathScript Instance;

    [Header("DeathMenu")]
    public GamePauser pauser;
    public float restartDelay = 1.5f;

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

        StartCoroutine(delayDeathMenu(restartDelay));
        rb.angularVelocity = new Vector3(100f, 100f, 100f);   
    }

    public IEnumerator delayDeathMenu(float time)
    {
        yield return new WaitForSeconds(restartDelay);

        pauser.PauseGame();
    }
}
