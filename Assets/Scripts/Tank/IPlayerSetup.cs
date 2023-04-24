using System;
using VoxTanks.Tank.Spawners;

namespace VoxTanks.Tank
{
    public interface IPlayerSetup
    {
        delegate void PlayerReady(int turret,int hull, TankTeam tankTeam);
        event PlayerReady OnPlayerReady;

        public ITankRespawner Respawner { get; }

        void SetPlayerReady(int tankTurret,int hull, TankTeam tankTeam = TankTeam.None);
    }
}