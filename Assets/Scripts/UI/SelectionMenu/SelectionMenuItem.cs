using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using VoxTanks.Network;
using VoxTanks.Tank;

namespace VoxTanks.UI
{
    public class SelectionMenuItem : MonoBehaviour
    {

        [SerializeField] IPlayerSetup _playerSetup;

        [SerializeField] Image _border;

        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _defaultColor;

        [SerializeField] private GameSetupMenu _gameSetupMenu;

        protected GameSetupMenu GameSetupMenu => _gameSetupMenu;

        private void Start()
        {
            _playerSetup = NetworkManager.Singleton.GetComponent<IPlayerSetup>();
        }

        public virtual void OnClick() => _border.color = _selectedColor;

        public void Deselect()
        {
            _border.color = _defaultColor;
        }
    }
}