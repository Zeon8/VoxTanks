using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VoxTanks.Tank.Spawners
{
    public class TeamTankRespawner : MonoBehaviour, ITankRespawner
    {
        [SerializeField] private TankSpawn[] _tankSpawns;

        private Dictionary<TankTeam, int> _tankSpawnOrders = new Dictionary<TankTeam, int>()
        {
            {TankTeam.Blue,0},
            {TankTeam.Red,0}
        };
        [SerializeField] private LayerMask _layerMask;

        public void RespawnTank(Transform tank, TankTeam tankTeam)
        {
            Transform point = GetSpawnPoint(tankTeam);
            if(point != null)
                tank.transform.SetPositionAndRotation(point.position,point.rotation);
        }

        private Transform GetSpawnPoint(TankTeam tankTeam)
        {
            var spawns = _tankSpawns.Where(spawn => spawn.TankTeam == tankTeam).ToArray();
            foreach (TankSpawn spawn in spawns)
            {
                if(!Physics.Raycast(spawn.transform.position,Vector3.down,Mathf.Infinity,_layerMask))
                    return spawn.transform;  
            }
            return null;
        }
    }
}
