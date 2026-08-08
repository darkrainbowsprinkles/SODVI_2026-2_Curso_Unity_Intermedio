using FPS.Combat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FPS.UI
{
    public class GunUI : MonoBehaviour
    {
        [SerializeField] RawImage crosshairImage;
        [SerializeField] RawImage scopeImage;
        [SerializeField] Image gunIconImage;
        [SerializeField] Image ammoIconImage;
        [SerializeField] TMP_Text ammoText;
        Fighter fighter;

        void Awake()
        {
            GameObject player = GameObject.FindWithTag("Player");
            fighter = player.GetComponent<Fighter>();
        }

        void Start()
        {
            RefreshGunUI();
            RefreshAmmoUI();
        }

        void Update()
        {
            RefreshScopeUI();
        }

        void OnEnable()
        {
            fighter.OnGunEquipped += RefreshGunUI;
            fighter.OnAmmoAdjusted += RefreshAmmoUI;
        }

        void OnDisable()
        {
            fighter.OnGunEquipped -= RefreshGunUI;
            fighter.OnAmmoAdjusted -= RefreshAmmoUI;
        }

        void RefreshGunUI()
        {
            GunSO currentGunSO = fighter.GetCurrentGunSO();
            crosshairImage.texture = currentGunSO.GetCrosshair();
            gunIconImage.sprite = currentGunSO.GetIcon();
            ammoIconImage.sprite = fighter.GetIcon(currentGunSO.GetAmmoType());
            RefreshAmmoUI();
        }

        void RefreshAmmoUI()
        {
            GunSO currentGunSO = fighter.GetCurrentGunSO();
            ammoText.text = fighter.GetAmmo(currentGunSO.GetAmmoType()).ToString();
        }

        void RefreshScopeUI()
        {
            GunSO currentGunSO = fighter.GetCurrentGunSO();

            if (currentGunSO.GetScope() == null)
            {
                scopeImage.enabled = false;
                crosshairImage.enabled = true;
                return;
            }

            scopeImage.enabled = fighter.IsZooming();
            crosshairImage.enabled = !fighter.IsZooming();
        }
    }
}