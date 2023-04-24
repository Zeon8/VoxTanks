using System;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.UI;

namespace VoxTanks.Tank.Turrets
{
    public abstract class TankTurret : NetworkBehaviour, ITankTurret
    {
        private float _additionalDamage = 1;
        public float AdditionalDamage { 
            get => _additionalDamage; 
            set 
            {
                if(value < 1)
                    throw new ArgumentOutOfRangeException(nameof(value), "must be greater than 1");
                _additionalDamage = value;
            }
        }

        [SerializeField] private TankSound _tankSound;

        [Header("Settings")]
        [SerializeField] private float _distance;
        [SerializeField] private LayerMask _layermask;

        [SerializeField] private float _reloadTime;

        [SerializeField] protected float _damage;

        [SerializeField] private int _soundIndex;


        protected float Damage => _damage;
        protected Crossghair Crosshair => _crossghair;

        protected LayerMask LayerMask => _layermask;
        protected float Distance => _distance;
        
        protected TankSetup TankSetup { get; private set; }

        private TurretAnimator _animator;

        private float _currentTime;
        private Transform _camera;
        private Crossghair _crossghair;
        private IStatusBar _statusBar;

        private void Start()
        {
            if (!IsServer && !IsLocalPlayer)
                enabled = false;
        }

        private void OnEnable()
        {
            _camera = Camera.main.transform;
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


            if(_statusBar is not null)
                _statusBar.Reload = _currentTime / _reloadTime;
        }

        private void FixedUpdate()
        {
            if (_currentTime < _reloadTime)
                _currentTime += Time.fixedDeltaTime;
        }

        [ServerRpc]
        private void FireServerRpc(Ray ray)
        {
            Debug.Log($"Received: {ray}");
            if (_currentTime >= _reloadTime)
            {
                _animator?.PlayShootAnimationClientRpc();
                _tankSound.PlayShotClientRpc(_soundIndex);
                _currentTime = 0;
                Shoot(ray);
                Debug.Log($"Shooted: {ray}");
            }
        }

        protected abstract void Shoot(Ray ray);
    }
}