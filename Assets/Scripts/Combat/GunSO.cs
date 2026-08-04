using UnityEngine;

namespace FPS.Combat
{
    [CreateAssetMenu(menuName = "FPS/New Gun")]
    public class GunSO : ScriptableObject
    {
        [SerializeField] Gun gunPrefab;
        [SerializeField] float damage = 30f;
        [SerializeField] float range = 40f;
        [SerializeField] float cooldown = 1f;
        [SerializeField] float zoomFOV = 40f;
        [SerializeField] bool isAutomatic;
        [SerializeField] bool canZoom;
        [SerializeField] AmmoType ammoType;
        [SerializeField] Texture2D crosshair;
        [SerializeField] Texture2D scope;
        [SerializeField] Sprite icon;

        public Gun Spawn(Transform container)
        {
            return Instantiate(gunPrefab, container);
        }

        public float GetDamage()
        {
            return damage;
        }

        public float GetRange()
        {
            return range;
        }

        public float GetCooldown()
        {
            return cooldown;
        }

        public float GetZoomFOV()
        {
            return zoomFOV;
        }

        public bool IsAutomatic()
        {
            return isAutomatic;
        }

        public bool CanZoom()
        {
            return canZoom;
        }

        public AmmoType GetAmmoType()
        {
            return ammoType;
        }

        public Texture2D GetCrosshair()
        {
            return crosshair;
        }

        public Texture2D GetScope()
        {
            return scope;
        }

        public Sprite GetIcon()
        {
            return icon;
        }
    }
}