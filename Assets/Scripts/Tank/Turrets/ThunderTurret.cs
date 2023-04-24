using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank.Turrets
{
    public class ThunderTurret : SmokiTurret
    {
        [SerializeField] private float _radius;
        private bool _display = false;
        private Vector3 _position;

        protected override void ProcessShoot(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                foreach (Collider collider in Physics.OverlapSphere(hit.point, _radius))
                {
                    Debug.DrawLine(hit.point, collider.transform.position);
                    if(collider.transform.root != transform.root)
                    {
                        var health = collider.GetComponentInParent<TankHealth>();
                        health?.TakeDamage(_damage * AdditionalDamage,TankSetup.Playername,TankSetup.Team);
                    }
                }
            }
        }

    }
}