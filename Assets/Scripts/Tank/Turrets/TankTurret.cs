using System;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.UI;

namespace VoxTanks.Tank.Turrets
{
    public abstract class TankTurret : NetworkBehaviour, ITankTurret
    {
        [field: SerializeField]
        public Transform Mazzle { get; private set; }

        public float AdditionalDamage
        {
            get => _additionalDamage;
            set => _additionalDamage = MathF.Min(value, 1f);
        }

        protected float Damage => _baseDamage * _additionalDamage;
        protected TankSetup TankSetup { get; private set; }
        protected Crosshair Crosshair { get; private set; }

        private bool Reloaded => _currentTime >= _reloadTime;

        [Header("Settings")]
        [SerializeField] private TankSound _tankSound;
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _baseDamage;
        [SerializeField] private int _soundIndex;

        private TurretAnimator _animator;
        private float _additionalDamage = 1;
        private float _currentTime;
        
        private IStatusBar _statusBar;

        private void Start()
        {
            if (!IsServer && !IsLocalPlayer)
                enabled = false;

            _animator = GetComponent<TurretAnimator>();
            TankSetup = GetComponentInParent<TankSetup>();

            if (IsLocalPlayer)
            {
                Crosshair = FindObjectOfType<Crosshair>();
                _statusBar = FindObjectOfType<StatusBar>();
            }
            AfterStart();
        } 

        protected virtual void AfterStart() {}

        private void Update()
        {
            if(!IsLocalPlayer)
                return;
            
            if (Input.GetButton("Fire1") && Reloaded)
            {
                Ray ray = GetRay();
                FireServerRpc(ray);
                _currentTime = 0;
            }

            _statusBar?.SetReloadProgress(_currentTime / _reloadTime);
        }

        protected Ray GetRay() => Camera.main.ScreenPointToRay(Crosshair.Position);

        private void FixedUpdate()
        {
            if (_currentTime < _reloadTime)
                _currentTime += Time.fixedDeltaTime;
        }

        [ServerRpc]
        private void FireServerRpc(Ray ray)
        {
            if (Reloaded)
            {
                if (_animator != null)
                    _animator.PlayShootAnimationClientRpc();

                _tankSound.PlayShotClientRpc(_soundIndex);
                _currentTime = 0;
                Shoot(ray);
            }
        }

        protected abstract void Shoot(Ray ray);
    }
}