using System;
using VoxTanks.Tank.Spawners;

namespace VoxTanks.Tank
{
    public interface IPlayerSetup
    {
        public ITankRespawner Respawner { get; }
    }
}