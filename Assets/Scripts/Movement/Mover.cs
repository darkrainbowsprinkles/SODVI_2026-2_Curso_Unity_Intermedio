using UnityEngine;

namespace FPS.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float maxSpeed = 10;
        CharacterController controller;

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            float totalSpeed = maxSpeed * Mathf.Clamp01(speedFraction);
            controller.Move(totalSpeed * Time.deltaTime * destination);
        }

        void Awake()
        {
            controller = GetComponent<CharacterController>();
        }
    }
}