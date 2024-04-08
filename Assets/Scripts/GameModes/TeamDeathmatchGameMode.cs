using UnityEngine;
using VoxTanks.Tank;

namespace VoxTanks.GameModes
{
    [CreateAssetMenu(menuName = "Game modes/Team deathmatch")]
    public class TeamDeathmatchGameMode : BaseTeamsGameMode
    {
        public void AddTeamScore(TankTeam team)
        {
            InfoTab.AddTeamScore(team, 1);
        }
    }
}
