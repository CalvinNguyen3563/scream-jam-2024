using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public PlayerControls playerControls;
    public PlayerManager player;
    public static PlayerInputManager instance;

    public Vector2 movementInput;

    public Vector2 cameraInput;
    public float cameraXInput;
    public float cameraYInput;
    public bool sprintInput = false;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        HandleAllInput();
    }

    void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerCamera.CameraRotation.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Sprinting.performed += i => sprintInput = true;
            playerControls.PlayerMovement.Sprinting.canceled += i => sprintInput = false;

        }

        playerControls.Enable();
    }

    private void HandleAllInput()
    {
        HandleCameraInput();
        HandleSprintInput();
    }

    private void HandleSprintInput()
    {
        if (sprintInput)
        {
            player.playerLocomotionManager.HandleSprinting();
        }
        else
        {
            player.playerLocomotionManager.isSprinting = false;
        }
    }

    private void HandleCameraInput()
    {
       
        cameraXInput = cameraInput.x;
        cameraYInput = cameraInput.y;
    }

    public Vector2 GetMovementInputDirection()
    {
        return movementInput.normalized;
    }
}
