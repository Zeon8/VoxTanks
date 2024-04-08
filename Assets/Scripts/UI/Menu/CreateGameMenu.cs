using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VoxTanks.Game;

namespace VoxTanks.UI.Menu
{
    public class CreateGameMenu : BaseRoomMenu
    {
        [SerializeField] private TMP_InputField _roomNameInput;
        [SerializeField] private Slider _maxPlayersSlider;
        [SerializeField] private Slider _playTimeSlider;
        [SerializeField] private GameSettings _settings;

        protected override void Start()
        {
            base.Start();
        }

        public void OnCreateClicked()
        {
            NetworkManager.StartHost();
            NetworkManager.SceneManager.LoadScene(SelectedMap, LoadSceneMode.Single);

            var battleInfo = new GameInfo()
            {
                Name = _roomNameInput.text,
                GameMode = SelectedGamemode.GameMode,
                Map = SelectedMap,
                MaxPlayerCount = (byte)_maxPlayersSlider.value,
                PlayerCount = 1
            };
            _settings.BattleInfo = battleInfo;

            var discovery = NetworkManager.Singleton.GetComponent<GameNetworkDiscovery>();
            discovery.DiscoveryData = battleInfo;
            discovery.StartServer();
        }
    }
}