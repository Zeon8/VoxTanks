using UnityEngine;

namespace VoxTanks.Tank.Spawners
{
    public class TankSpawn : MonoBehaviour
    {
        [SerializeField] private TankTeam _team;
        public TankTeam TankTeam => _team;
    }
}