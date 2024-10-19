using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionManager : MonoBehaviour
{
    [Header("Player Components")]
    public PlayerManager player;

    public float moveSpeed = 5f;
    public float sprintMoveSpeed = 7f;
    public float gravityForce = -13f;
    public float maxSpeed = 10f;
    public float maxSprintSpeed = 15f;

    

    [Header("Movement Flags")]
    public bool isSprinting = false;

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
        //HandleGravity();
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

        Vector3 moveVelocity = Vector3.zero;
        float speedCap = 0;

        if (!isSprinting)
        {
            speedCap = maxSpeed;
            moveVelocity = dir.y * player.orientation.forward * moveSpeed;
            moveVelocity += dir.x * player.orientation.right * moveSpeed;
            moveVelocity = new Vector3(moveVelocity.x, player.rb.velocity.y, moveVelocity.z);
        }
        else if (PlayerStatsManager.Instance.Stamina > 0f) 
        {
            //Handle stamina
            speedCap = maxSprintSpeed;
            moveVelocity = dir.y * player.orientation.forward * sprintMoveSpeed;
            moveVelocity += dir.x * player.orientation.right * sprintMoveSpeed;
            moveVelocity = new Vector3(moveVelocity.x, player.rb.velocity.y, moveVelocity.z);
           
            PlayerStatsManager.Instance.DecreaseStamina();
        }
        

        if (dir != Vector2.zero)
        {
            Vector2 currentVelocity = player.rb.velocity;

            // Totally made by Andy
            // Calculate the dot product between movement direction and current velocity
            float dotProduct = Vector2.Dot(currentVelocity.normalized, dir.normalized);

            // If the dot product is negative, it means the player is trying to move in the opposite direction
            if (dotProduct < 0)
            {
                // Apply counter force to reduce the current velocity before applying new movement
                Vector2 counterForce = -currentVelocity * 4f;  // Adjust multiplier for how strong the counter force is
                player.rb.AddForce(new Vector3(counterForce.x, 0f, 0f));
            }

            if (player.rb.velocity.magnitude < speedCap)
            {
                player.rb.AddForce(moveVelocity);
            }
            else
            {
                player.rb.velocity = player.rb.velocity.normalized * speedCap;
            }
        }
        else
        {
            float adjustedVelocityX = Mathf.Lerp(player.rb.velocity.x, 0f, 0.2f);
            float adjustedVelocityZ = Mathf.Lerp(player.rb.velocity.z, 0f, 0.2f);
            player.rb.velocity = new Vector3(adjustedVelocityX, moveVelocity.y, adjustedVelocityZ);
        }

    }

    public void HandleSprinting()
    {
        // Only allow sprinting if running forward
        if (PlayerInputManager.instance.GetMovementInputDirection().y > 0f && PlayerStatsManager.Instance.Stamina > 0f)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
   
    }
}
