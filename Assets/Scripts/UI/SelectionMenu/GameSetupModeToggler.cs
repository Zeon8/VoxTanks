using Unity.Netcode;
using UnityEngine;
using VoxTanks.GameModes;
using VoxTanks.Game;

namespace VoxTanks.UI.SelectionMenu
{
    public class GameSetupModeToggler : MonoBehaviour
    {
        [SerializeField] private GameSetup _gameSetup;
        [SerializeField] private GameSetupMenu _defaultMenu;
        [SerializeField] private GameSetupMenu _teamsModeMenu;

        public GameSetupMenu CurrentMenu { get; private set; }

        private GameSetupMenu DetermineCurrentMenu()
        {
            if (_gameSetup.CurrentGameMode is TeamGameMode)
                return _teamsModeMenu;
            else
                return _defaultMenu;
        }

        public void Show()
        {
            CurrentMenu = DetermineCurrentMenu();
            SetVisible(true);
        }

        public void SetVisible(bool visible) => CurrentMenu.gameObject.SetActive(visible);
    }
}
