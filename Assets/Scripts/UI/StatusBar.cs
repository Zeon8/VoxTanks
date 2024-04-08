using UnityEngine;
using UnityEngine.UI;

namespace VoxTanks.UI
{
    public class StatusBar : MonoBehaviour, IStatusBar
    {
        [SerializeField] private GameObject _gameInfo;
        [SerializeField] private Image _reloadProgress;
        [SerializeField] private Slider _healthProgress;

        public void SetVisible(bool value) => _gameInfo.SetActive(value);
        public void SetHealthProgress(float value) => _healthProgress.value = value;
        public void SetReloadProgress(float value) => _reloadProgress.fillAmount = value;
    }
}