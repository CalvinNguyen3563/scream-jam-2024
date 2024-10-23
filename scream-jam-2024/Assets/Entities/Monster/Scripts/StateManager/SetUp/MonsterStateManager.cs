using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MonsterStateManager : MonoBehaviour
{
    [Header("References")]
    public MonsterBaseState currentState;
    public MonsterManager monster;
    public NavMeshAgent agent;
    public GameObject orientation;
    public Animator animator;
    public MonsterPatrolState patrolState = new MonsterPatrolState();
    public MonsterChaseState chaseState = new MonsterChaseState();
    public MonsterStunState stunState = new MonsterStunState();
    public MonsterDeathState deathState = new MonsterDeathState();

    [Header("Speed")]
    public float walkingAcceleration = 5f;
    public float walkingSpeed = 3.5f;
    public float runningAcceleration = 5f; 
    public float runningSpeed = 7f;

    [Header("Patrol")]
    public float patrolRange = 25f;
    public float playerDetectRadius;
    public bool playerInRadius = true;
    public float stepSize = 10f;

    [Header("Debug")]
    public bool pointFound = false;
    public Vector3 point;

    [Header("LayerMask")]
    public LayerMask whatIsPlayer;
    public LayerMask whatIsObstacle;

    [Header("AudioClip")]
    public AudioClip roarClip;
    public AudioClip attackClip;
    public AudioClip stunClip;
    public AudioClip deathClip;


    public Animation fadeAnimation;

    public bool isDead = false;
   
    private void Awake()
    {
        monster = GetComponent<MonsterManager>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        animator = monster.animator;

        currentState = patrolState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        if (MonsterStatsManager.Instance.health <= 0 && !isDead)
        {
            isDead = true;
            SwitchState(deathState);
            StartCoroutine(FadeOut());
        }
        
        currentState.UpdateState(this);

        float turnMovement = Vector3.Dot(transform.right, agent.velocity.normalized);

        animator.SetFloat("velocity", agent.velocity.magnitude / runningSpeed);
        animator.SetFloat("horizontal", turnMovement);
    }

    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(4f);

        fadeAnimation.Play();
    }

    public void SwitchState(MonsterBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public IEnumerator stun(float time)
    {
        animator.SetBool("stunned", true);
        monster.PlayFireParticle();

        yield return new WaitForSeconds(time);

        animator.SetBool("stunned", false);
        monster.fadeOutCoroutine = StartCoroutine(monster.FadeOutFire());
    }

    public void PlayAttackAudio()
    {
        SoundManager.instance.PlaySoundFXClip(attackClip, orientation.transform.position, 1f);
    }

    private void OnDrawGizmos()
    {
        // If a point was found, draw a sphere gizmo at the position
        if (pointFound)
        {
            Gizmos.color = Color.green; // Set gizmo color to green
            Gizmos.DrawSphere(point, 1f); // Draw the sphere with a radius of 1 unit
        }

        if (WorldGameObjectStorage.Instance != null && WorldGameObjectStorage.Instance.player != null)
        {
            Gizmos.color = Color.blue; // Set gizmo color to blue for the patrol range
            Vector3 playerPosition = WorldGameObjectStorage.Instance.player.transform.position;
            Gizmos.DrawWireSphere(playerPosition, patrolRange); // Draw a wireframe sphere for patrol range
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerDetectRadius);

    }

}
