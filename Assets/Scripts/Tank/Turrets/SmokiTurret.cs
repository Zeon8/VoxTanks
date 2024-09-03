using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank.Turrets
{
    public class SmokiTurret : ProjectileTurret
    {
        protected override void HitTarget(RaycastHit hit)
        {
            var health = hit.collider.GetComponentInParent<TankHealth>();
            if(health != null)
                health.TakeDamage(Damage, TankSetup.PlayerName, TankSetup.Team);
        }
    }
}