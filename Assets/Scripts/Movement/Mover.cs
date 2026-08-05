using UnityEngine;
using UnityEngine.AI;

namespace FPS.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float maxSpeed = 10f;
        [SerializeField] float rotationSpeed = 10f;
        CharacterController controller;
        NavMeshAgent agent;
        Animator animator;

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            float totalSpeed = maxSpeed * Mathf.Clamp01(speedFraction);

            if (CompareTag("Player"))
            {
                controller.Move(totalSpeed * Time.deltaTime * destination);
            }
            else if (CompareTag("Enemy"))
            {
                agent.isStopped = false;
                agent.SetDestination(destination);
                agent.speed = totalSpeed;
            }
        }

        public void LookAt(GameObject target)
        {
            Vector3 lookDirection = target.transform.position - transform.position;
            lookDirection.y = 0f;

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(lookDirection),
                Time.deltaTime * rotationSpeed
            );
        }

        public void Stop()
        {
            agent.isStopped = true;
        }

        void Awake()
        {
            controller = GetComponent<CharacterController>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (animator == null)
            {
                return;
            }

            float localVelocity = transform.InverseTransformDirection(agent.velocity).magnitude;
            animator.SetFloat("movementSpeed", localVelocity, 0.1f, Time.deltaTime);
        }
    }
}