using FPS.Combat;
using FPS.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPS.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField, Range(0,1)] float walkSpeedFraction = 0.5f;
        [SerializeField, Range(0,1)] float sprintSpeedFraction = 1f;
        PlayerInput playerInput;
        Mover mover;
        Fighter fighter;

        void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
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
                fighter.Fire();
            }
        }

        void HandleMovement()
        {
            InputAction sprintAction = playerInput.actions["Sprint"];

            if (sprintAction.IsPressed())
            {
                mover.MoveTo(CalculateMovement(), sprintSpeedFraction);
            }
            else
            {
                mover.MoveTo(CalculateMovement(), walkSpeedFraction);
            }
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
}