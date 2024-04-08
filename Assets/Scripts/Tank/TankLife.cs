using System;
using Unity.Netcode;
using UnityEngine;
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

        private ITankRespawner _tankRespawner;
        private TankTeam _tankTeam;

        public void ApplySettings(TankSettings settings)
        {
            _tankTeam = settings.Team;
            _tankRespawner = FindAnyObjectByType<GameSetup>().CurrentGameMode.Respawner;
        }

        public void Respawn()
        {
            _tankRespawner.RespawnTank(transform, _tankTeam);
            Respawned?.Invoke();
        }

        [ClientRpc]
        public void SetControllableClientRpc(bool value)
        {
            if (!IsLocalPlayer)
                return;

            foreach (var component in controlComponents)
                component.enabled = value;
        }

        [ClientRpc]
        public void SetVisibleClientRpc(bool value)
        {
            _rigidbody.isKinematic = !value;
            foreach (var part in tankParts)
            {
                if(part != null)
                    part.SetActive(value);
            }
        }

        
    }
}