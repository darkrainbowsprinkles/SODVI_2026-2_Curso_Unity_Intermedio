using FPS.Core;
using UnityEngine;

namespace FPS.Combat
{
    public class AmmoPickup : Pickup
    {
        [SerializeField] AmmoType ammoType;
        [SerializeField] int ammoAmount = 10;

        protected override void OnPickup(GameObject player)
        {
            player.GetComponent<Fighter>().AdjustAmmo(ammoType, ammoAmount);
        }
    }
}