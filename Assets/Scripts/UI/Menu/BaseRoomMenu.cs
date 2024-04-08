using Assets.Scripts.UI.Menu;
using System.Reflection;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VoxTanks.GameModes;

namespace VoxTanks.UI.Menu
{
    public abstract class BaseRoomMenu : MonoBehaviour
    {
        protected NetworkManager NetworkManager { get; private set; }

        protected string SelectedMap { get; private set; }

        protected GameModeItem SelectedGamemode { get; private set; }

        [SerializeField] private Image[] _mapImages;
        [SerializeField] private Image[] _gamemodeImages;

        [SerializeField] private Color _selectedItemColor;
        [SerializeField] private Color _defaultItemColor;

        [SerializeField] private MapItem _defaultMap;
        [SerializeField] private GameModeItem _defaultGameMode;
        
        protected virtual void Start()
        {
            NetworkManager = NetworkManager.Singleton;

            OnSelectedMap(_defaultMap);
            OnSelectedGameMode(_defaultGameMode);
        }

        public void DeselectItems(Image[] items)
        {
            foreach (Image item in items)
            {
                item.color = _defaultItemColor;
            }
        }

        public void OnSelectedMap(MapItem mapItem)
        {
            // Get map name by image object name
            SelectedMap = mapItem.MapName;
            Select(mapItem.Image, _mapImages);
        }

        public void OnSelectedGameMode(GameModeItem gameModeItem)
        {
            // Get gamemode id by image object name
            SelectedGamemode =  gameModeItem;
            Select(gameModeItem.Image, _gamemodeImages);
        }

        private void Select(Image selectedItem, Image[] deselectItems)
        {
            DeselectItems(deselectItems);
            selectedItem.color = _selectedItemColor;
        }
    }
}