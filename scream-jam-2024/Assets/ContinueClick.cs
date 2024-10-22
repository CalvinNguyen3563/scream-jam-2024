using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueClick : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    public Animation animation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animation.Play();
            source.PlayOneShot(clip);
            StartCoroutine(WaitForSoundToFinish());
        }
    }

    IEnumerator WaitForSoundToFinish()
    {
        // Wait for the audio clip's duration before loading the scene
        yield return new WaitForSeconds(clip.length);

        // Load the new scene
        SceneManager.LoadScene(2);
    }
}
