using UnityEngine;
using VoxTanks.Tank;

namespace VoxTanks.GameModes
{
    public class TeamDeathmatchGameMode : TeamsGameMode
    {
        public void AddTeamScore(TankTeam team)
        {
            InfoTab.AddTeamScore(team);
        }
    }
}
