using System;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank;
using VoxTanks.Tank.Spawners;

namespace VoxTanks.Network
{
    public class TankSpawner : NetworkBehaviour
    {
        private IPlayerSetup PlayerSetup { get; set; }
        //private TankSettings TankSettings { get; set; }
        [SerializeField] private NetworkObject _tank;

        private void Start()
        {
            PlayerSetup = NetworkManager.GetComponent<IPlayerSetup>();

            if(IsLocalPlayer)
                PlayerSetup.OnPlayerReady += OnPlayerReady;
        }

        private void OnPlayerReady(int turret, int hull, TankTeam tankTeam)
        {
            PlayerSetup.OnPlayerReady -= OnPlayerReady;
            SpawnTankServerRpc(NetworkManager.LocalClientId, GameSettings.PlayerName, turret, hull, tankTeam);
        }

        [ServerRpc]
        private void SpawnTankServerRpc(ulong localClientId,string name, int turret, int hull, TankTeam team)
        {
            NetworkObject tank = Instantiate(_tank);
            tank.SpawnAsPlayerObject(localClientId);
            TankSetup(tank,name,turret,hull,team);
            NetworkObject.Despawn(true);
        }

        protected virtual void TankSetup(NetworkObject tank,string name, int turret, int hull, TankTeam team)
        {
            tank.GetComponent<TankSetup>()?.Setup(name, team);
            tank.GetComponent<TankIdentifier>()?.SetupClientRpc();

            if (tank.TryGetComponent(out TankSettingsApplier tankSettingsApplier))
            {
                tankSettingsApplier.SelectTurretServerRpc(turret);
                tankSettingsApplier.SelectHullServerRpc(hull);
            }

            if (tank.TryGetComponent(out TankLife tankLife))
            {
                tankLife.Setup(PlayerSetup.Respawner, team);
                tankLife.RespawnTank();
            }
        }
    }
}