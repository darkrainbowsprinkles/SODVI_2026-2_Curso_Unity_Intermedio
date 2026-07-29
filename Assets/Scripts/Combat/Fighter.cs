using UnityEngine;

namespace FPS.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] Gun gun;
        [SerializeField] Transform gunContainer;

        public void Fire()
        {
            gun.Fire();
        }

        void Awake()
        {
            Instantiate(gun, gunContainer);
        }
    }
}