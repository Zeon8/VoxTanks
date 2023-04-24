using UnityEngine;
using VoxTanks.Tank;
using VoxTanks.UI;

namespace VoxTanks.GameModes.FlagMode
{
    public class TeamFlagsMode : TeamsGameMode
    {
        private FlagMessagesUI _messageUI;

        protected override void OnGameStarted()
        {
            base.OnGameStarted();
            _messageUI = FindObjectOfType<FlagMessagesUI>();
            Debug.Log(_messageUI);
        }

        public void OnFlagCaptured(string playerName, TankTeam team) => _messageUI.CreateFlagCapturedMessageClientRpc(playerName, team);

        public void OnFlagLost(string playerName, TankTeam team) => _messageUI.CreateLostFlagMessageClientRpc(playerName, team);

        public void OnFlagReturned(string playerName, TankTeam team) => _messageUI.CreateReturnedFlagMessageClientRpc(playerName, team);

        public void OnFlagDelivered(string playerName, TankTeam tankTeam, TankTeam flagTeam)
        {
            InfoTab.AddTeamScore(tankTeam);
            _messageUI.CreateDeliveredFlagMessageClientRpc(playerName, flagTeam);
        }
    }
}
