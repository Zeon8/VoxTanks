using UnityEngine;
using UnityEngine.UI;
using VoxTanks.GameModes;

namespace VoxTanks.UI.Menu
{
    public class GameModeItem : MonoBehaviour
    {
        [SerializeField] private string _gameMode;
        [SerializeField] private Image _image;

        public string GameMode => _gameMode;
        public Image Image => _image;

    }
}