using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using VoxTanks.Game;
using VoxTanks.Tank.Spawners;

namespace VoxTanks.Tank
{
    public class TankLife : NetworkBehaviour
    {
        public event Action Respawned;

        [SerializeField] private Behaviour[] controlComponents;
        [SerializeField] private GameObject[] tankParts;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private UnityEvent<bool> _visibilityChanged;

        private ITankRespawner _tankRespawner;
        private TankSetup _setup;

        private void Start()
        {
            _setup = GetComponent<TankSetup>();
        }

        public void ApplySettings(TankSettings settings)
        {
            if (IsServer)
            {
                if(_tankRespawner == null)
                    _tankRespawner = FindAnyObjectByType<GameSetup>().CurrentGameMode.Respawner;
                _tankRespawner.RespawnTank(transform, settings.Team);
            }
        }

        public void Respawn()
        {
            _tankRespawner.RespawnTank(transform, _setup.Team);
            Respawned?.Invoke();
        }

        public void SetTankActive(bool value)
        {
            SetControllable(value);
            SetVisible(value);
        }

        private void SetControllable(bool value)
        {
            if (!IsLocalPlayer)
                return;

            foreach (var component in controlComponents)
                component.enabled = value;
        }

        
        private void SetVisible(bool value)
        {
            _rigidbody.isKinematic = !value;
            foreach (var part in tankParts)
            {
                if(part != null)
                    part.SetActive(value);
            }
            _visibilityChanged.Invoke(value);
        }

        
    }
}