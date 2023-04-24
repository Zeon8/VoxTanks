using System;
using System.Collections;
using Effects;
using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank.Turrets
{
    public class Railgun : TankTurret
    {
        
        [SerializeField] private float _beforeShootTime;
        [SerializeField] private RailgunRay _ray;
        [SerializeField] private Transform _muzzle;
        private FlashDisplay _flashDisplay;
        
        private void Start()
        {
            _flashDisplay = GetComponent<FlashDisplay>();
        }
        protected override void Shoot(Ray ray)
        {
            StartCoroutine(ShowFlashAndShoot());
        }

        private IEnumerator ShowFlashAndShoot()
        {
            yield return ShowFlash();

#if EXPERIMENTAL
            MakeShotClientRpc();
#else
            var ray = new Ray(_muzzle.position, _muzzle.forward);
            MakeShot(ray);
#endif
        }

#if EXPERIMENTAL
        [ClientRpc]
        private void MakeShotClientRpc() 
        {
            Ray ray = Camera.main.ScreenPointToRay(Crosshair.Position);
            MakeShotServerRpc(ray);
        }
        
        [ServerRpc]
        private void MakeShotServerRpc(Ray ray) => MakeShot(ray);
#endif

        private void MakeShot(Ray ray)
        {
            SpawnRay(ray);
            DamageTarget(ray);
        }


        private IEnumerator ShowFlash()
        {
            _flashDisplay.ShowFlashClientRpc();
            yield return new WaitForSeconds(_beforeShootTime);
            _flashDisplay.HideClientRpc();
        }

        private void DamageTarget(Ray ray)
        {
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (RaycastHit target in hits)
            {
                var tankHealth = target.collider.GetComponentInParent<TankHealth>();
                tankHealth?.TakeDamage(Damage,TankSetup.Playername,TankSetup.Team);
            }
        }

        private void SpawnRay(Ray ray)
        {
            if (Physics.Raycast(ray,out RaycastHit hit,Distance,LayerMask))
            {
                RailgunRay railgunRay = Instantiate(_ray);
                railgunRay.Setup(_muzzle.position, hit.point);
            }
        }
    }
}