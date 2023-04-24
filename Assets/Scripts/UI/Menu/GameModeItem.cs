using UnityEngine;
using UnityEngine.UI;
using VoxTanks.GameModes;

namespace VoxTanks.UI.Menu
{
    public class GameModeItem : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private DefaultGameMode _gameMode;
        [SerializeField] private Image _image;
        public DefaultGameMode GameMode => _gameMode;
        public Image Image => _image;
        public string Name => _name;

    }
}