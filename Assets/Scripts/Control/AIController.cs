using FPS.Movement;
using UnityEngine;

namespace FPS.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField, Range(0,1)] float chaseSpeedFraction = 0.5f;
        [SerializeField] float chaseRange = 10f;
        GameObject player;
        Mover mover;

        void Awake()
        {
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();
        }

        void Update()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            if (distanceToPlayer < chaseRange)
            {
                mover.MoveTo(player.transform.position, chaseSpeedFraction);
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.softRed;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}