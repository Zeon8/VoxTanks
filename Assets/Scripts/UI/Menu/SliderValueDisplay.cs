using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VoxTanks.UI.Menu
{
    public class SliderValueDisplay : MonoBehaviour
    {
        [SerializeField] private string suffix;
        [SerializeField] private TMP_Text _text;

        public void OnValueChanged(float value)
        {
            _text.text = value + " " + suffix;
        }
    }
}