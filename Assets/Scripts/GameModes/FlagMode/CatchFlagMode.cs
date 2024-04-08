using UnityEngine;
using VoxTanks.Tank;
using VoxTanks.UI;

namespace VoxTanks.GameModes.FlagMode
{

    [CreateAssetMenu(menuName = "Game modes/Catch the flag")]
    public class CatchFlagMode : BaseTeamsGameMode
    {
        private FlagMessagesUI _messageUI;

        public override void Initialize()
        {
            base.Initialize();
            _messageUI = FindObjectOfType<FlagMessagesUI>();
        }

        public void OnFlagCaptured(string playerName, TankTeam team) => _messageUI.CreateFlagCapturedMessageClientRpc(playerName, team);

        public void OnFlagLost(string playerName, TankTeam team) => _messageUI.CreateLostFlagMessageClientRpc(playerName, team);

        public void OnFlagReturned(string playerName, TankTeam team) => _messageUI.CreateReturnedFlagMessageClientRpc(playerName, team);

        public void OnFlagDelivered(string playerName, TankTeam tankTeam, TankTeam flagTeam)
        {
            InfoTab.AddTeamScore(tankTeam, 1);
            _messageUI.CreateDeliveredFlagMessageClientRpc(playerName, flagTeam);
        }
    }
}
