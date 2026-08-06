using FPS.Core;
using FPS.Movement;
using UnityEngine;

namespace FPS.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField, Range(0,1)] float chaseSpeedFraction = 0.5f;
        [SerializeField] float chaseRange = 10f;
        [SerializeField] float attackRange = 1.5f;
        [SerializeField] float hitRange = 3f;
        [SerializeField] float attackDamage = 30f;
        GameObject player;
        Mover mover;
        Animator animator;
        Health health;

        void Awake()
        {
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (health.IsDead())
            {
                return;
            }

            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            bool isAttacking = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");

            if (isAttacking || distanceToPlayer < attackRange)
            {
                AttackBehavior();
            }
            else if (distanceToPlayer < chaseRange)
            {
                ChaseBehavior();
            }
            else
            {
                IdleBehavior();
            }
        }

        void AttackBehavior()
        {
            mover.Stop();
            animator.SetTrigger("attack");
            mover.LookAt(player);
        }

        void ChaseBehavior()
        {
            animator.ResetTrigger("attack");
            mover.MoveTo(player.transform.position, chaseSpeedFraction);
        }

        void IdleBehavior()
        {
            mover.Stop();
            animator.ResetTrigger("attack");
        }

        // Called in Unity Events
        void Hit()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            if (distanceToPlayer < hitRange)
            {
                player.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.softRed;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}