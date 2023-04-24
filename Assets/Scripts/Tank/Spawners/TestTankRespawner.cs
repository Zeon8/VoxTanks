using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank.Spawners
{
    public class TestTankRespawner : MonoBehaviour, ITankRespawner
    {
        public void RespawnTank(Transform tank,TankTeam tankTeam) 
        {  
            var startPosition = FindObjectOfType<TankSpawn>().transform.position;
            tank.position = startPosition;
        }
    }
}