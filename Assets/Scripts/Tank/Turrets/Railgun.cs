using System;
using System.Collections;
using Effects;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.UI;

namespace VoxTanks.Tank.Turrets
{
    public class Railgun : TankTurret
    {
        [SerializeField] private float _beforeShootTime;
        [SerializeField] private RailgunRay _ray;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _distance;

        private FlashDisplay _flashDisplay;
        private RaycastHit[] _hits = new RaycastHit[5];
        
        protected override void AfterStart()
        {
            _flashDisplay = GetComponent<FlashDisplay>();
        }

        protected override void Shoot(Ray ray) => ShootClientRpc();

        [ClientRpc]
        private void ShootClientRpc() => StartCoroutine(ShowFlashAndShoot());

        private IEnumerator ShowFlashAndShoot()
        {
            yield return ShowFlash();

            if (IsLocalPlayer)
            {
                Ray ray = GetRay();
                ShootServerRpc(ray);
            }
        }

        [ServerRpc]
        private void ShootServerRpc(Ray ray)
        {
            SpawnRayClientRpc(ray);
            DamageTargets(ray);
        }

        private IEnumerator ShowFlash()
        {
            _flashDisplay.Show();
            yield return new WaitForSeconds(_beforeShootTime);
            _flashDisplay.Hide();
        }

        private void DamageTargets(Ray ray)
        {
            Physics.RaycastNonAlloc(ray, _hits);
            foreach (RaycastHit target in _hits)
            {
                if (target.collider == null)
                    break;

                var tankHealth = target.collider.GetComponentInParent<TankHealth>();
                if (tankHealth != null)
                    tankHealth.TakeDamage(Damage, TankSetup.PlayerName, TankSetup.Team);
            }
            Array.Clear(_hits, 0, _hits.Length);
        }

        [ClientRpc]
        private void SpawnRayClientRpc(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, _distance, _layerMask))
            {
                RailgunRay railgunRay = Instantiate(_ray);
                railgunRay.Setup(_muzzle.position, hit.point);
            }
        }
    }
}