using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Components")]
    public Rigidbody rb;
    public Transform orientation;
    public Camera cam;
    public CinemachineVirtualCamera vcam;
    public GameObject cameraHolder;
    public PlayerAnimatorManager playerAnimatorManager;
    public PlayerInteractableManager playerInteractableManager;
    public Collider playerCollider;

    [Header("Player Classes")]
    public PlayerLocomotionManager playerLocomotionManager;

    [Header("Ground Check")]
    public Transform legs;
    public bool isGrounded;
    public float groundCheckRadius = 2f;
    public LayerMask whatIsGround;
    

    [Header("Layer Mask")]
    public LayerMask whatIsEnemy;
    public LayerMask whatIsWater;

    [Header("Water")]
    public float waterDmg = 10f;
    public bool canWaterDmg = true;
    public float waterDmgCooldown = 1.5f;

    [Header("IsDead")]
    public bool isDead = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
    }
    void Start()
    {
        playerAnimatorManager = WorldGameObjectStorage.Instance.cameraHolder.GetComponentInChildren<PlayerAnimatorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectGround();

        if (PlayerStatsManager.Instance.Health <= 0 && !isDead)
        {
            isDead = true;
            PlayerDeathScript.Instance.ActiveDeath();
        }
    }

    public void DetectGround()
    {
        isGrounded = Physics.OverlapSphere(legs.position, groundCheckRadius, whatIsGround).Length > 0;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsWater) != 0 && canWaterDmg)
        {
            PlayerStatsManager.Instance.DecreaseHealth(waterDmg);
            StartCoroutine(resetWaterCooldown(waterDmgCooldown));
        }
    }

    private IEnumerator resetWaterCooldown(float time)
    {
        canWaterDmg = false;
        yield return new WaitForSeconds(time);
        canWaterDmg = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(legs.position, groundCheckRadius);
    }
}
