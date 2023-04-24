using System;
using TMPro;
using UnityEngine;

namespace VoxTanks.UI.Menu
{
    public class RoomMenuItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _mapText;
        [SerializeField] private TMP_Text _gamemodeText;
        [SerializeField] private TMP_Text _playersText;
        private Action _joinRoom;

        public void Setup(string roomName,string map, string gamemode, int playersCount, byte maxPlayers, Action joinRoom)
        {
            _nameText.text = roomName;
            _mapText.text = map;
            _gamemodeText.text = gamemode;
            _playersText.text = playersCount + "/" + maxPlayers;
            _joinRoom = joinRoom;
        }

        public void OnClicked() => _joinRoom?.Invoke();


    }
}
