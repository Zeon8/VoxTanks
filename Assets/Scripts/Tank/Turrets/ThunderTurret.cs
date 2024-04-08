using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank.Turrets
{
    public class ThunderTurret : SmokiTurret
    {
        [SerializeField] private float _radius;

        protected override void HandleShot(RaycastHit hit)
        {
            foreach (Collider collider in Physics.OverlapSphere(hit.point, _radius))
            {
                Debug.DrawLine(hit.point, collider.transform.position);
                if (collider.transform.root != transform.root)
                {
                    var health = collider.GetComponentInParent<TankHealth>();
                    if(health != null)
                        health.TakeDamage(Damage, TankSetup.Playername, TankSetup.Team);
                }
            }
        }

    }
}