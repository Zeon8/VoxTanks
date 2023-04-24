using System.Reflection;
using Photon.Realtime;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VoxTanks.GameModes;

namespace VoxTanks.UI.Menu
{
    public class FindGameMenu : BaseRoomMenu
    {
        [SerializeField] private PhotonGame _photonGame = new PhotonGame();

        protected override void Start() 
        {
            base.Start();
            _photonGame.Setup();
        }

        private void Update() => _photonGame.Update();
        private void LateUpdate() => _photonGame.LateUpdate();

        public void OnStartGameClicked()
        {
            if(_selectedGamemode != null)
                _networkManager?.gameObject.AddComponent(_selectedGamemode.GameMode.GetType());
            _networkManager.OnServerStarted += () =>
            {
                _networkManager.SceneManager.LoadScene(_selectedMap, LoadSceneMode.Single);
            };
            _photonGame.CreateOrJoinGame(_selectedMap,_selectedGamemode.Name);
        }
    }
}