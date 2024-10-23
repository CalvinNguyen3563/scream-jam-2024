using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathScript : MonoBehaviour
{
    public Collider cameraCollider;
    public Rigidbody rb;
    public PlayerCamera playerCamera;
    public static PlayerDeathScript Instance;
    public AudioClip clip;

    [Header("DeathMenu")]
    public GameObject pauser;

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
        SoundManager.instance.PlaySoundFXClip(clip, WorldGameObjectStorage.Instance.player.transform.position, 1f);

        StartCoroutine(delayDeathMenu(1.5f));
        rb.angularVelocity = new Vector3(100f, 100f, 100f);   
    }

    public IEnumerator delayDeathMenu(float time)
    {
        yield return new WaitForSeconds(time);

        pauser.SetActive(true);
    }
}
