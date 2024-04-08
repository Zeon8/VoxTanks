using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using VoxTanks.GameModes;
using VoxTanks.Game;
using VoxTanks.Tank;

namespace VoxTanks.UI
{
    public abstract class SelectionMenuItem : MonoBehaviour
    {

        [SerializeField] BaseGameMode _playerSetup;

        [SerializeField] Image _border;

        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _defaultColor;

        [SerializeField] private GameSetupMenu _gameSetupMenu;

        protected GameSetupMenu GameSetupMenu => _gameSetupMenu;

        private void Start()
        {
            _playerSetup = FindObjectOfType<GameSetup>().CurrentGameMode;
        }

        public void Select() => _border.color = _selectedColor;

        public void Deselect() => _border.color = _defaultColor;
    }
}