using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class SliderMaxValueCheck : MonoBehaviour
{
    public Slider slider;                    // Reference to the Slider component
    public GameObject displayText;           // Reference to the Text GameObject
    public GameObject postProcessing;        // Reference to the Post-Processing GameObject
    private Vignette vignette;               // Vignette effect reference

    void Start()
    {
        // Ensure the display text is initially hidden
        displayText.SetActive(false);

        // Add listener to call a function whenever the slider value changes
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    // Function called when slider value changes
    private void OnSliderValueChanged(float value)
    {
        if (Mathf.Approximately(value, slider.maxValue))
        {
            displayText.SetActive(true); // Show the display text
            if (vignette != null)
            {
                vignette.intensity.value = 0.5f;  // Increase vignette intensity
            }

            // Start shaking the camera through the CameraShakeManager
            CameraShakerManager.Instance.StartShaking(0.2f);
        }
        else
        {
            displayText.SetActive(false); // Hide the display text
            if (vignette != null)
            {
                vignette.intensity.value = value / slider.maxValue; // Adjust intensity
            }

            CameraShakerManager.Instance.StopShaking();
        }
    }

    private void OnDestroy()
    {
        // Remove listener to avoid memory leaks
        slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}
