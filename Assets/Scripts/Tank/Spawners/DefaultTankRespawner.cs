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
            for (int i = 0; i < _tankSpawns.Length; i++)
            {
                var spawnPointNumber = Random.Range(0, _tankSpawns.Length);
                var spawnPoint = _tankSpawns[spawnPointNumber];
                if (spawnPoint != null && !Physics.Raycast(spawnPoint.transform.position, Vector3.down, Mathf.Infinity, _layerMask))
                {
                    Transform spawnTransform = spawnPoint.transform;
                    tank.SetPositionAndRotation(spawnTransform.position, spawnTransform.rotation);
                    break;
                }
            }
        }

    }
}
