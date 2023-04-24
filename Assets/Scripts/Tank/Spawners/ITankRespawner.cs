using UnityEngine;

namespace VoxTanks.Tank.Spawners
{
    public interface ITankRespawner
    {
        void RespawnTank(Transform tank, TankTeam tankTeam);
    }
}
