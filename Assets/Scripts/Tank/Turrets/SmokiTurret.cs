using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank;
using VoxTanks.Tank.Turrets;
using VoxTanks.UI;

namespace VoxTanks.Tank.Turrets {
    public class SmokiTurret : TankTurret
    {
        [SerializeField] private GameObject _projectileEffect;

        protected override void Shoot(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit) && !hit.collider.CompareTag("Player"))
            {
                SpawnProjectile(hit.point);
                HandleShot(hit);
            } 
        }

        protected virtual void HandleShot(RaycastHit hit)
        {
            var health = hit.collider.GetComponentInParent<TankHealth>();
            if(health != null)
                health.TakeDamage(Damage, TankSetup.Playername, TankSetup.Team);
        }

        protected virtual void SpawnProjectile(Vector3 position)
        {
            GameObject gm = Instantiate(_projectileEffect, position, Quaternion.identity);
            gm.GetComponent<NetworkObject>().Spawn();
        }

        
    }
}