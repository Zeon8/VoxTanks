using UnityEngine;

namespace VoxTanks.Tank.Spawners
{
    public class TankSpawn : MonoBehaviour
    {
        public TankTeam TankTeam => _team;

        [SerializeField] private TankTeam _team;
    }
}