using System;
using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank.Turrets
{
    public class ThunderTurret : SmokiTurret
    {
        [SerializeField] private float _radius;

        private readonly Collider[] _colliders = new Collider[5];

        protected override void HitTarget(RaycastHit hit)
        {
            Physics.OverlapSphereNonAlloc(hit.point, _radius, _colliders);
            foreach (Collider collider in _colliders)
            {
                if (collider == null)
                    break;

                Debug.DrawLine(hit.point, collider.transform.position);
                if (collider.transform.root != transform.root)
                {
                    var health = collider.GetComponentInParent<TankHealth>();
                    if(health != null)
                        health.TakeDamage(Damage, TankSetup.PlayerName, TankSetup.Team);
                }
            }
            Array.Clear(_colliders, 0, _colliders.Length);
        }

    }
}