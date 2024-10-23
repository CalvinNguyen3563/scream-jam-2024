using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TransitionBackToMain : MonoBehaviour
{
   public VideoPlayer video;

    private void Start()
    {
        StartCoroutine(delayTransition((float)video.clip.length));
    }

    public IEnumerator delayTransition(float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(0);
    }
}
