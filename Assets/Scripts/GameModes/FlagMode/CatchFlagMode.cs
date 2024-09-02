using UnityEngine;
using VoxTanks.Tank;
using VoxTanks.UI;

namespace VoxTanks.GameModes.FlagMode
{

    [CreateAssetMenu(menuName = "Game modes/Catch the flag")]
    public class CatchFlagMode : TeamGameMode
    {
        private FlagMessagesUI _messageUI;

        protected override void InitializeTeamMode()
        {
            _messageUI = FindObjectOfType<FlagMessagesUI>();
        }

        public void OnFlagCaptured(string playerName, TankTeam team) => _messageUI.CreateFlagCapturedMessage(playerName, team);

        public void OnFlagLost(string playerName, TankTeam team) => _messageUI.CreateLostFlagMessage(playerName, team);

        public void OnFlagReturned(string playerName, TankTeam team) => _messageUI.CreateReturnedFlagMessage(playerName, team);

        public void OnFlagDelivered(string playerName, TankTeam tankTeam, TankTeam flagTeam)
        {
            if(InfoTab.IsServer)
                InfoTab.AddTeamScore(tankTeam, 1);
            _messageUI.CreateDeliveredFlagMessage(playerName, flagTeam);
        }
    }
}
