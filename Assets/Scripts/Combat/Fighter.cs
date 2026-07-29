using UnityEngine;

namespace FPS.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] GunSO defaultGunSO;
        [SerializeField] Transform gunContainer;
        GunSO currentGunSO;
        Gun currentGun;
        float timeSinceLastFire = Mathf.Infinity;

        public GunSO GetCurrentGunSO()
        {
            return currentGunSO;
        }

        public void Fire()
        {
            if (timeSinceLastFire < currentGunSO.GetCooldown())
            {
                return;
            }

            currentGun.Fire(currentGunSO.GetDamage(), currentGunSO.GetRange());
            timeSinceLastFire = 0f;
        }

        void Awake()
        {
            EquipGun(defaultGunSO);
        }

        void Update()
        {
            timeSinceLastFire += Time.deltaTime;
        }

        void EquipGun(GunSO gunSO)
        {
            currentGunSO = gunSO;
            currentGun = gunSO.Spawn(gunContainer);
        }
    }
}