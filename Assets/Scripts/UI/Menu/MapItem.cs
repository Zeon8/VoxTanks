
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu
{
    public class MapItem : MonoBehaviour
    {
        [SerializeField] private Image _image;
        public Image Image => _image;

        [SerializeField] private string _mapName;
        public string MapName => _mapName;
    }
}