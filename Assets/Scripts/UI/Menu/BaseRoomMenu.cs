using System.Reflection;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VoxTanks.GameModes;

namespace VoxTanks.UI.Menu
{
    public class BaseRoomMenu : MonoBehaviour
    {
        [SerializeField] private Image[] _mapImages;
        [SerializeField] private Image[] _gamemodeImages;

        [SerializeField] private Color _selectedItemColor;
        [SerializeField] private Color _defaultItemColor;
        
        protected NetworkManager _networkManager;
        protected string _selectedMap;
        protected GameModeItem _selectedGamemode;

        protected virtual void Start()
        {
            _networkManager = NetworkManager.Singleton;
        }

        public void DeselectItems(Image[] items)
        {
            foreach (Image item in items)
            {
                item.color = _defaultItemColor;
            }
        }

        public void OnSelectedMap(Image mapImage)
        {
            // Get map name by image object name
            _selectedMap = mapImage.name;
            Select(mapImage,_mapImages);
        }

        public void OnSelectedGameMode(GameModeItem gameModeItem)
        {
            // Get gamemode id by image object name
            _selectedGamemode =  gameModeItem;
            Select(gameModeItem.Image, _gamemodeImages);
        }

        private void Select(Image selectedItem, Image[] deselectItems)
        {
            DeselectItems(deselectItems);
            selectedItem.color = _selectedItemColor;
        }
    }
}