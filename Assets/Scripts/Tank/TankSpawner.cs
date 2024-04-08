using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using VoxTanks.Tank;
using VoxTanks.UI;
using VoxTanks.UI.SelectionMenu;

namespace VoxTanks.Game
{
    public class TankSpawner : NetworkBehaviour
    {
        [SerializeField] private string _mainMenuScene;
        [SerializeField] private NetworkObject _tank;
        private GameSetupMenu _menu;


        private void Start()
        {
            if (SceneManager.GetActiveScene().name == _mainMenuScene)
                NetworkManager.SceneManager.OnLoadComplete += SceneManager_OnLoadComplete;
            else 
                Init();
        }

        private void SceneManager_OnLoadComplete(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
        {
            NetworkManager.SceneManager.OnLoadComplete -= SceneManager_OnLoadComplete;
            Init();
        }

        private void Init()
        {
            FindObjectOfType<GameSetupModeToggler>().Show();
            _menu = FindObjectOfType<GameSetupMenu>();
            _menu.Selected += OnPlayerReady;
        }

        private void OnPlayerReady(TankSettings tankSettings)
        {
            _menu.Selected -= OnPlayerReady;
            SpawnTankServerRpc(NetworkManager.LocalClientId, tankSettings);
        }

        [ServerRpc]
        private void SpawnTankServerRpc(ulong localClientId, TankSettings tankSettings)
        {
            NetworkObject tank = Instantiate(_tank);
            tank.SpawnAsPlayerObject(localClientId);
            tank.GetComponent<TankSettingsApplier>().ApplySettingsServerRpc(tankSettings);
            tank.GetComponent<TankLife>().Respawn();
            NetworkObject.Despawn(true);
        }
    }
}