using System.Collections;
using UnityEngine;

namespace VoxTanks.Tank.Spawners
{
    public class DefaultTankRespawner : MonoBehaviour, ITankRespawner
    {
        [SerializeField] private TankSpawn[] _tankSpawns;
        [SerializeField] private LayerMask _layerMask;

        public void RespawnTank(Transform tank, TankTeam tankTeam)
        {
            TankSpawn spawnPoint;
            do
            {
                var spawnPointNumber = Random.Range(0, _tankSpawns.Length);
                spawnPoint = _tankSpawns[spawnPointNumber];
            }
            while (spawnPoint == null || Physics.Raycast(spawnPoint.transform.position, Vector3.down, Mathf.Infinity, _layerMask));
            
            Transform spawnTransform = spawnPoint.transform;
            tank.SetPositionAndRotation(spawnTransform.position, spawnTransform.rotation);
        }

    }
}
