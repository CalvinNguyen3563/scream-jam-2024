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

    [Header("Player Classes")]
    public PlayerLocomotionManager playerLocomotionManager;

    [Header("Ground Check")]
    public Transform legs;
    public bool isGrounded;
    public float groundCheckRadius = 2f;
    public LayerMask whatIsGround;

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
    }

    public void DetectGround()
    {
        isGrounded = Physics.OverlapSphere(legs.position, groundCheckRadius, whatIsGround).Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(legs.position, groundCheckRadius);
    }
}
