using UnityEngine;

namespace FPS.Core
{
    public abstract class Pickup : MonoBehaviour
    {
        [SerializeField] float rotationSpeed = 100f;
        [SerializeField] bool destroy;

        void Update()
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPickup(other.gameObject);

                if (destroy)
                {
                    Destroy(gameObject);
                }
            }
        }

        protected abstract void OnPickup(GameObject player);
    }
}