using UnityEngine;
using UnityEngine.AI;

namespace FPS.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float maxSpeed = 10;
        CharacterController controller;
        NavMeshAgent agent;

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

        public void Stop()
        {
            agent.isStopped = true;
        }

        void Awake()
        {
            controller = GetComponent<CharacterController>();
            agent = GetComponent<NavMeshAgent>();
        }
    }
}