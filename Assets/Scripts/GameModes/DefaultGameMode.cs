using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank;
using VoxTanks.Tank.Spawners;

namespace VoxTanks.GameModes
{
    public class DefaultGameMode : MonoBehaviour, IPlayerSetup
    {
        public virtual ITankRespawner Respawner => FindObjectOfType<DefaultTankRespawner>();

        public event IPlayerSetup.PlayerReady OnPlayerReady;

        public virtual void SetPlayerReady(int tankTurret, int hull, TankTeam tankTeam = TankTeam.None) => OnPlayerReady?.Invoke(tankTurret,hull,tankTeam);
    }
}
