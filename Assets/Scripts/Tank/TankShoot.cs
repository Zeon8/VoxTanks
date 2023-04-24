using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank.Turrets;

namespace VoxTanks.Tank
{
    public class TankShoot : NetworkBehaviour
    {
        [SerializeField] private TankTurret _tankTurret;

        public void SetAditionalDamage(float damage) => _tankTurret.AdditionalDamage = damage;

    }
}