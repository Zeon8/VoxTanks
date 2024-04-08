using System;
using TMPro;
using UnityEngine;
using VoxTanks.Game;

namespace VoxTanks.UI.Menu
{
    public class RoomMenuItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _mapText;
        [SerializeField] private TMP_Text _gamemodeText;
        [SerializeField] private TMP_Text _playersText;
        private Action _joinRoom;

        public void Setup(GameInfo battle, Action joinRoom)
        {
            _nameText.text = battle.Name;
            _mapText.text = battle.Map ;
            _gamemodeText.text = battle.GameMode;
            _playersText.text = battle.PlayerCount + "/" + battle.MaxPlayerCount;
            _joinRoom = joinRoom;
        }

        public void OnClicked() => _joinRoom?.Invoke();


    }
}
