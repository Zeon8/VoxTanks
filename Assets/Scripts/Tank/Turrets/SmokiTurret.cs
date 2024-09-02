using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank.Turrets
{
    public class SmokiTurret : TankTurret
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

        protected virtual void HitTarget(RaycastHit hit)
        {
            var health = hit.collider.GetComponentInParent<TankHealth>();
            if(health != null)
                health.TakeDamage(Damage, TankSetup.PlayerName, TankSetup.Team);
        }

        [ClientRpc]
        private void SpawnProjectileClientRpc(Vector3 position)
        {
            Instantiate(_projectileEffect, position, Quaternion.identity);
        }
    }
}