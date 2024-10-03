using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public PlayerControls playerControls;
    public static PlayerInputManager instance;

    public Vector2 movementInput;

    public Vector2 cameraInput;
    public float cameraXInput;
    public float cameraYInput;
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
    }

    private void Update()
    {
        HandleCameraInput();
    }

    void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerCamera.CameraRotation.performed += i => cameraInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();
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
