using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank.Turrets
{
    public abstract class ProjectileTurret : TankTurret
    {
        [SerializeField] private GameObject _projectileEffect;

        protected override void Shoot(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                SpawnProjectileClientRpc(hit.point);
                HitTarget(hit);
            }
        }

        protected abstract void HitTarget(RaycastHit hit);

        [ClientRpc]
        private void SpawnProjectileClientRpc(Vector3 position)
        {
            Instantiate(_projectileEffect, position, Quaternion.identity);
        }
    }
}