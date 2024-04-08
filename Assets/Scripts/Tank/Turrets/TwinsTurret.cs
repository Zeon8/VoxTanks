using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank.Turrets 
{
    public class TwinsTurret : TankTurret
    {
        [SerializeField] private TwinsProjectile _projectile;
        [SerializeField] private Transform _leftMuzzle;
        [SerializeField] private Transform _rightMuzzle;

        private bool _leftMuzzleQueue = false;

        protected override void Shoot(Ray ray)
        {
            Quaternion rotation = Quaternion.LookRotation(ray.direction, Vector3.up);
            _leftMuzzleQueue = !_leftMuzzleQueue;

            if(_leftMuzzleQueue)
                SpawnProjectileServerRpc(_leftMuzzle.position, rotation);
            else
                SpawnProjectileServerRpc(_rightMuzzle.position, rotation);

        }

        [ServerRpc]
        private void SpawnProjectileServerRpc(Vector3 position, Quaternion rotation)
        {
            TwinsProjectile projectile = Instantiate(_projectile, position, rotation);
            projectile.NetworkObject.Spawn();
            projectile.Setup(Damage, TankSetup.Playername, TankSetup.Team);
        }
    }
}