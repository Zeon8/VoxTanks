using Unity.Netcode;
using UnityEngine;
using VoxTanks.GameModes;
using VoxTanks.Tank;
using VoxTanks.UI;

namespace VoxTanks.UI.SelectionMenu
{
    public class GameSetupModeToggler : MonoBehaviour
    {
        [SerializeField] private GameSetupMenu _defaultMenu;
        [SerializeField] private GameSetupMenu _teamsModeMenu;
        private void Start()
        {
            GameSetupMenu menu;
            if(NetworkManager.Singleton.GetComponent<TeamsGameMode>())
                menu = _teamsModeMenu;
            else
                menu = _defaultMenu;

            menu.gameObject.SetActive(true);
        }
    }
}
