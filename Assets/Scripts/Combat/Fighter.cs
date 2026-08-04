using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace FPS.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] GunSO defaultGunSO;
        [SerializeField] Transform gunContainer;
        [SerializeField] AmmoSlot[] ammoSlots;
        [SerializeField] AmmoIcon[] ammoIcons;
        [SerializeField] CinemachineCamera firstPersonCamera;
        [SerializeField] Camera gunCamera;
        GunSO currentGunSO;
        Gun currentGun;
        float timeSinceLastFire = Mathf.Infinity;
        float defaultFOV;
        bool isZooming;
        Dictionary<AmmoType, int> ammoLookup;

        public event Action OnGunEquipped;
        public event Action OnAmmoAdjusted;

        public GunSO GetCurrentGunSO()
        {
            return currentGunSO;
        }

        public bool IsZooming()
        {
            return isZooming;
        }

        public void EquipGun(GunSO gunSO)
        {
            if (currentGun != null)
            {
                Destroy(currentGun.gameObject);
            }

            currentGunSO = gunSO;
            currentGun = gunSO.Spawn(gunContainer);
            OnGunEquipped?.Invoke();
        }

        public void AdjustAmmo(AmmoType ammoType, int ammoAmount)
        {
            ammoLookup[ammoType] += ammoAmount;
            OnAmmoAdjusted?.Invoke();
        }

        public int GetAmmo(AmmoType ammoType)
        {
            return ammoLookup[ammoType];
        }

        public void Fire()
        {
            if (timeSinceLastFire < currentGunSO.GetCooldown())
            {
                return;
            }

            AmmoType currentAmmoType = currentGunSO.GetAmmoType();

            if (GetAmmo(currentAmmoType) <= 0)
            {
                return;
            }

            currentGun.Fire(currentGunSO.GetDamage(), currentGunSO.GetRange());
            timeSinceLastFire = 0f;
            AdjustAmmo(currentAmmoType, -1);
            print($"Ammo type: {currentAmmoType} - {GetAmmo(currentAmmoType)}");
        }

        public void ToggleZoom(bool isZooming)
        {
            this.isZooming = isZooming;

            if (isZooming && currentGunSO.CanZoom())
            {
                firstPersonCamera.Lens.FieldOfView = currentGunSO.GetZoomFOV();
                gunCamera.fieldOfView = currentGunSO.GetZoomFOV();
            }
            else
            {
                firstPersonCamera.Lens.FieldOfView = defaultFOV;
                gunCamera.fieldOfView = defaultFOV;
            }
        }

        public Sprite GetIcon(AmmoType ammoType)
        {
            foreach (AmmoIcon ammoIcon in ammoIcons)
            {
                if (ammoIcon.ammoType == ammoType)
                {
                    return ammoIcon.icon;
                }
            }

            return null;
        }

        [System.Serializable]
        struct AmmoSlot
        {
            public AmmoType ammoType;
            public int ammoAmount;
        }

        [System.Serializable]
        struct AmmoIcon
        {
            public AmmoType ammoType;
            public Sprite icon;
        }

        void Awake()
        {
            CreateAmmoLookup();
            EquipGun(defaultGunSO);
            defaultFOV = firstPersonCamera.Lens.FieldOfView;
        }

        void Update()
        {
            timeSinceLastFire += Time.deltaTime;
        }

        void CreateAmmoLookup()
        {
            ammoLookup = new Dictionary<AmmoType, int>();

            foreach (AmmoSlot ammoSlot in ammoSlots)
            {
                ammoLookup[ammoSlot.ammoType] = ammoSlot.ammoAmount;
            }
        }
    }
}