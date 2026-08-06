using System;
using System.Collections.Generic;
using FPS.Core;
using UnityEngine;
using UnityEngine.Events;

namespace FPS.Combat
{
    public class AggroGroup : MonoBehaviour
    {
        [SerializeField] UnityEvent onGroupDead;
        Dictionary<Health, UnityAction> deathListeners = new();
        int totalCount;

        public event Action OnChange;

        public bool GroupDead()
        {
            return deathListeners.Count == 0;
        }

        public int GetTotalCount()
        {
            return totalCount;
        }

        public int GetAliveCount()
        {
            return deathListeners.Count;
        }

        void Awake()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                Health enemyHealth = enemy.GetComponent<Health>();
                void listener() => OnEnemyDead(enemyHealth);
                deathListeners[enemyHealth] = listener;
                enemyHealth.onDie.AddListener(listener);
            }

            totalCount = enemies.Length;
        }

        void OnEnemyDead(Health enemy)
        {
            if (deathListeners.TryGetValue(enemy, out UnityAction listener))
            {
                enemy.onDie.RemoveListener(listener);
                deathListeners.Remove(enemy);
            }

            if (deathListeners.Count == 0)
            {
                onGroupDead?.Invoke();
            }

            OnChange?.Invoke();
        }
    }
}