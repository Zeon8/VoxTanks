using System;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.UI;

namespace VoxTanks.Tank.Turrets
{
    public abstract class TankTurret : NetworkBehaviour, ITankTurret
    {
        public float AdditionalDamage 
        { 
            get => _additionalDamage; 
            set 
            {
                if(value < 1)
                    throw new ArgumentOutOfRangeException(nameof(value), "must be greater than 1");
                _additionalDamage = value;
            }
        }

        protected float Damage => _damage * _additionalDamage;
        protected Crossghair Crosshair => _crossghair;
        protected LayerMask LayerMask => _layermask;
        protected float Distance => _distance;
        protected TankSetup TankSetup { get; private set; }

        [Header("Settings")]
        [SerializeField] private float _distance;
        [SerializeField] private TankSound _tankSound;
        [SerializeField] private LayerMask _layermask;
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _damage;
        [SerializeField] private int _soundIndex;

        private TurretAnimator _animator;
        private float _additionalDamage = 1;
        private float _currentTime;
        private Crossghair _crossghair;
        private IStatusBar _statusBar;

        private void Start()
        {
            if (!IsServer && !IsLocalPlayer)
                enabled = false;
        }

        private void OnEnable()
        {
            _statusBar = FindObjectOfType<StatusBar>();
            _crossghair = FindObjectOfType<Crossghair>();
            _animator = GetComponent<TurretAnimator>();
            TankSetup = GetComponentInParent<TankSetup>();
        }

        private void Update()
        {
            if(!IsLocalPlayer)
                return;
            
            if (Input.GetButton("Fire1"))
            {
                var ray = Camera.main.ScreenPointToRay(Crosshair.Position);
                FireServerRpc(ray);
            }

            _statusBar?.SetReloadProgress(_currentTime / _reloadTime);
        }

        private void FixedUpdate()
        {
            if (_currentTime < _reloadTime)
                _currentTime += Time.fixedDeltaTime;
        }

        [ServerRpc]
        private void FireServerRpc(Ray ray)
        {
            if (_currentTime >= _reloadTime)
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