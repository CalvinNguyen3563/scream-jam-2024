using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterManager : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public MonsterLocomotionManager monsterLocomotionManager;

    [Header("Particles")]
    public GameObject fireParticle;
    public Vector3 fireTargetScale = new Vector3(0.75f, 0.75f, 0.75f);
    public Coroutine fadeOutCoroutine;

    [Header("Layer")]
    public LayerMask whatIsPlayer;
    private void Awake()
    {
        monsterLocomotionManager = GetComponent<MonsterLocomotionManager>();
    }

    public void PlayFireParticle()
    {
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
        }
        fireParticle.transform.localScale = fireTargetScale;

    }

    public IEnumerator FadeOutFire()
    {
        float timeToFade = 2f;
        float timeElapsed = 0f;

        while (timeElapsed < timeToFade)
        {
            float t = timeElapsed / timeToFade;
            fireParticle.transform.localScale = Vector3.Lerp(fireParticle.transform.localScale, Vector3.zero, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        fireParticle.transform.localScale = Vector3.zero;
    }
}
