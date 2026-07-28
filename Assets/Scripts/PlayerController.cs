using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float sprintMultiplier = 1.5f;
    PlayerInput playerInput;
    CharacterController controller;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleFire();
        HandleMovement();
    }

    void HandleFire()
    {
        InputAction fireAction = playerInput.actions["Fire"];

        if (fireAction.WasPressedThisFrame())
        {
            print("FIRE!!!");
        }
    }

    void HandleMovement()
    {
        float totalSpeed = movementSpeed;

        InputAction sprintAction = playerInput.actions["Sprint"];

        if (sprintAction.IsPressed())
        {
            totalSpeed = movementSpeed * sprintMultiplier;
        }

        controller.Move(CalculateMovement() * Time.deltaTime * totalSpeed);
    }

    Vector3 CalculateMovement()
    {
        Vector2 inputValue = playerInput.actions["Locomotion"].ReadValue<Vector2>();

        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0f;

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;

        Vector3 motion = cameraRight * inputValue.x + cameraForward * inputValue.y;
        return motion;
    }
}
