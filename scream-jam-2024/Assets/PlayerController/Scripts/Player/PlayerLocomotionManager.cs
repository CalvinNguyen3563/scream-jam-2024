using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionManager : MonoBehaviour
{
    [Header("Player Components")]
    public PlayerManager player;

    public float moveSpeed = 5f;
    public float gravityForce = -13f;
    public float maxSpeed = 10f;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //HandleGravity();
        

    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    private void HandleGravity()
    {
        if (!player.isGrounded)
        {
            Debug.Log("hello");
            player.rb.velocity = new Vector3(player.rb.velocity.x, gravityForce, player.rb.velocity.z);
        }
    }

    private void HandlePlayerMovement()
    {
       Vector2 dir =  PlayerInputManager.instance.GetMovementInputDirection();

       Vector3 moveVelocity = dir.y * player.orientation.forward * moveSpeed;
       moveVelocity += dir.x * player.orientation.right * moveSpeed;
       moveVelocity = new Vector3(moveVelocity.x, player.rb.velocity.y, moveVelocity.z);

        if (dir != Vector2.zero)
        {
            if (player.rb.velocity.magnitude < maxSpeed)
            {
                player.rb.velocity = moveVelocity;
            }
        }
        else
        {
            float adjustedVelocity = Mathf.Lerp(player.rb.velocity.x, 0f, 0.5f);
            player.rb.velocity = new Vector3(adjustedVelocity, moveVelocity.y, moveVelocity.z);
        }

    }
}
