using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank.Spawners;

namespace VoxTanks.Tank
{
    public class TankLife : NetworkBehaviour
    {
        [SerializeField] private Behaviour[] controlComponents;
        [SerializeField] private GameObject[] tankParts;
        [SerializeField] private Rigidbody _rigidbody;

        private ITankRespawner _tankRespawner;

        private TankTeam _tankTeam;

        public void Setup(ITankRespawner tankRespawner, TankTeam tankTeam)
        {
            _tankTeam = tankTeam;
            _tankRespawner = tankRespawner;
        }
        
        public void RespawnTank() =>  _tankRespawner?.RespawnTank(transform,_tankTeam);

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
                part?.SetActive(value);
            }
        }
    }
}