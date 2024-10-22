using UnityEngine;
using System.Collections;

public class CameraShakerManager : MonoBehaviour
{
    public static CameraShakerManager Instance; // Singleton instance
    public GameObject Cameras;                  // Reference to the main camera
    private Vector3 originalCameraPosition;     // Store the original camera position
    private Coroutine shakeCoroutine;           // Store the shake coroutine reference
    private bool isShaking = false;             // Flag to control shaking state

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist this GameObject through scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    void Start()
    {
        originalCameraPosition = Cameras.transform.position; // Store original position
    }

    // Start shaking the camera
    public void StartShaking(float magnitude)
    {
        if (!isShaking)
        {
            shakeCoroutine = StartCoroutine(ShakeCamera(magnitude));
        }
    }

    // Coroutine to handle shake effect
    private IEnumerator ShakeCamera(float magnitude)
    {
        isShaking = true; // Set shaking flag
        while (isShaking)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Cameras.transform.position = originalCameraPosition + new Vector3(x, y, 0f);
            yield return null; // Wait until next frame
        }

        // Reset camera position when shaking stops
        Cameras.transform.position = originalCameraPosition;
    }

    // Method to stop shaking
    public void StopShaking()
    {
        isShaking = false; // Set shaking flag to false
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine); // Stop shaking coroutine
        }
    }
}
