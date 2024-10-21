using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;  // Import Post-Processing namespace
using System.Collections;

public class SliderMaxValueCheck : MonoBehaviour
{
    public Slider slider;                    // Reference to the Slider component
    public GameObject displayText;           // Reference to the Text GameObject
    public PostProcessProfile postProcessProfile; // Reference to the Post-Processing Profile asset
    public Camera mainCamera;                // Reference to the main camera

    private Vignette vignette;  // Vignette effect reference
    private Vector3 originalCameraPosition;  // Store the original camera position

    void Start()
    {
        // Ensure the display text is initially hidden
        displayText.SetActive(false);

        // Store the original camera position
        originalCameraPosition = mainCamera.transform.position;


        // Add listener to call a function whenever the slider value changes
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    // Function called when slider value changes
    private void OnSliderValueChanged(float value)
    {
        if (Mathf.Approximately(value, slider.maxValue))
        {
            displayText.SetActive(true);      // Show the display text
            vignette.intensity.value = 0.5f;  // Increase vignette intensity
            StartCoroutine(ShakeScreen(0.2f, 0.1f)); // Start screen shake
        }
        else
        {
            displayText.SetActive(false);     // Hide the display text
            vignette.intensity.value = value / slider.maxValue;  // Adjust intensity
        }
    }

    // Coroutine to handle the screen shake effect
    private IEnumerator ShakeScreen(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Generate random offsets
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            // Apply the offset to the camera position
            mainCamera.transform.position = originalCameraPosition + new Vector3(x, y, 0f);

            // Increment the elapsed time
            elapsed += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        // Reset the camera position after shaking
        mainCamera.transform.position = originalCameraPosition;
    }

    private void OnDestroy()
    {
        // Remove listener to avoid memory leaks
        slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}
