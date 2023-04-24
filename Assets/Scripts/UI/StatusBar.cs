using UnityEngine;
using UnityEngine.UI;

namespace VoxTanks.UI
{
    public class StatusBar : MonoBehaviour, IStatusBar
    {
        [SerializeField] private GameObject _gameInfo;
        [SerializeField] private Image _reloadProgress;
        [SerializeField] private Slider _healthProgress;

        public bool Visible 
        {
            set => _gameInfo.SetActive(value);
        }

        public float Health
        {
            set => _healthProgress.value = value;
        }

        public float Reload
        {
            set => _reloadProgress.fillAmount = value;
        }
    }
}