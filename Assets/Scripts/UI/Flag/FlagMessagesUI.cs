using Unity.Netcode;
using UnityEngine;
using VoxTanks.GameModes.FlagMode;
using VoxTanks.Tank;

namespace VoxTanks.UI
{
    public class FlagMessagesUI : NetworkBehaviour
    {
        [SerializeField] private FlagMessageItem _flagInfoPrefab;
        [SerializeField] private Transform _itemsContainer;

        [SerializeField] private string _capturedFlagText;
        [SerializeField] private string _returnedFlagText;
        [SerializeField] private string _lostFlagText;
        [SerializeField] private string _deliveredFlagText;

        private FlagSound _flagSound;

        private void Start()
        {
            _flagSound = FindObjectOfType<FlagSound>();
        }

        [ClientRpc]
        public void CreateFlagCapturedMessageClientRpc(string name, TankTeam taemFlag)
        {
            CreateMessage(_capturedFlagText, name, taemFlag);
            _flagSound.PlayFlagCaptured();
        }

        [ClientRpc]
        public void CreateReturnedFlagMessageClientRpc(string name, TankTeam taemFlag)
        {
            CreateMessage(_returnedFlagText, name, taemFlag);
            _flagSound.PlayFlagReturned();
        }

        [ClientRpc]
        public void CreateLostFlagMessageClientRpc(string name, TankTeam taemFlag)
        {
            CreateMessage(_lostFlagText, name, taemFlag);
            _flagSound.PlayFlagLost();
        }

        [ClientRpc]
        public void CreateDeliveredFlagMessageClientRpc(string name, TankTeam taemFlag)
        {
            CreateMessage(_deliveredFlagText, name, taemFlag);
            _flagSound.PlayFlagDelivered();
        }
        
        private void CreateMessage(string messageTemplate, string name, TankTeam taemFlag)
        {
            var message = string.Format(messageTemplate, name, taemFlag.ToString());
            var flagInfo = Instantiate(_flagInfoPrefab, _itemsContainer);
            flagInfo.Setup(message);
        }
    }
}
