using System;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.GameModes.FlagMode;
using VoxTanks.Tank;

namespace VoxTanks.UI
{
    public class FlagMessagesUI : MonoBehaviour
    {
        [SerializeField] private FlagMessageItem _flagInfoPrefab;
        [SerializeField] private Transform _itemsContainer;

        [Multiline]
        [SerializeField] private string _capturedFlagText;

        [Multiline]
        [SerializeField] private string _returnedFlagText;

        [Multiline]
        [SerializeField] private string _lostFlagText;

        [Multiline]
        [SerializeField] private string _deliveredFlagText;

        [Multiline]
        [SerializeField] private string _redFlagText;

        [Multiline]
        [SerializeField] private string _blueFlagText;

        private FlagSound _flagSound;

        private void Start()
        {
            _flagSound = FindObjectOfType<FlagSound>();
        }

        public void CreateFlagCapturedMessage(string playerName, TankTeam team)
        {
            CreateMessage(_capturedFlagText, playerName, team);
            _flagSound.PlayFlagCaptured();
        }

        public void CreateReturnedFlagMessage(string playerName, TankTeam team)
        {
            CreateMessage(_returnedFlagText, playerName, team);
            _flagSound.PlayFlagReturned();
        }

        public void CreateLostFlagMessage(string playerName, TankTeam team)
        {
            CreateMessage(_lostFlagText, playerName, team);
            _flagSound.PlayFlagLost();
        }

        public void CreateDeliveredFlagMessage(string playerName, TankTeam team)
        {
            CreateMessage(_deliveredFlagText, playerName, team);
            _flagSound.PlayFlagDelivered();
        }
        
        private void CreateMessage(string messageTemplate, string playerName, TankTeam team)
        {
            string flagText = GetFlagText(team);
            string message = string.Format(messageTemplate, playerName, flagText);
            FlagMessageItem flagInfo = Instantiate(_flagInfoPrefab, _itemsContainer);
            flagInfo.Setup(message);
        }

        private string GetFlagText(TankTeam team)
        {
            return team switch
            {
                TankTeam.Blue => _blueFlagText,
                TankTeam.Red => _redFlagText,
                _ => throw new ArgumentOutOfRangeException(nameof(team))
            };
        }
    }
}
