using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace FPS.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth = 200f;
        [SerializeField] UnityEvent onDamageTaken;
        [SerializeField] UnityEvent onDie;
        float currentHealth;

        public bool IsDead()
        {
            return currentHealth <= 0f;
        }

        public float GetHealthPercentage()
        {
            return currentHealth / maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth = Mathf.Max(currentHealth - damage, 0f);
            onDamageTaken?.Invoke();

            if (currentHealth == 0)
            {
                HandleDeath();
            }
        }

        void Awake()
        {
            currentHealth = maxHealth;
        }

        void HandleDeath()
        {
            if (TryGetComponent(out Animator animator))
            {
                animator.SetTrigger("die");
            }

            if (TryGetComponent(out NavMeshAgent agent))
            {
                agent.enabled = false;
            }

            GetComponent<Collider>().enabled = false;
            onDie?.Invoke();
        }
    }
}