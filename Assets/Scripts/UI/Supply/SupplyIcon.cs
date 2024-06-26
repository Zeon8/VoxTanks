using UnityEngine;
using UnityEngine.UI;
using VoxTanks.UI.Supply;

namespace VoxTanks.UI.Supply
{
    public class SupplyIcon : MonoBehaviour
    {
        protected Image Image => _image;

        [SerializeField] private Image _image;

        public void SetProgress(float value)
        {
            gameObject.SetActive(value > 0);
            Image.fillAmount = 1 - value;
        }
    }
}