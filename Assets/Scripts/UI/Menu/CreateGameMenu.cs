using System;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.StructWrapping;
using Netcode.Transports.PhotonRealtime;
using Photon.Realtime;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace VoxTanks.UI.Menu
{
    public class CreateGameMenu : BaseRoomMenu
    {
        private PhotonRealtimeTransport2 _transport;

        [SerializeField] private TMP_InputField _roomNameInput;
        [SerializeField] private Slider _maxPlayersSlider;
        [SerializeField] private Slider _playTimeSlider;

        protected override void Start()
        {
            base.Start();
            _transport = (PhotonRealtimeTransport2)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        }


        public void OnCreateClicked()
        {
            var roomProperties = new Hashtable
            {
                ["map"] = _selectedMap,
                ["gm"] = _selectedGamemode.Name,
                ["time"] = _playTimeSlider.value
            };
            _transport.Lobby = new TypedLobby("custom", LobbyType.Default);
            _transport.CustomRoomProperties = roomProperties;
            _transport.RoomName = _roomNameInput.text;
            _transport.MaxPlayers = (byte)_maxPlayersSlider.value;
            NetworkManager.Singleton.gameObject.AddComponent(_selectedGamemode.GameMode.GetType());
            _networkManager.OnServerStarted += () =>
            {
                _networkManager.SceneManager.LoadScene(_selectedMap, LoadSceneMode.Single);
            };
            NetworkManager.Singleton.StartHost();
        }
    }
}