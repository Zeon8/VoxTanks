using TMPro;
using UnityEngine;

namespace VoxTanks.UI
{
    public class FlagMessageItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        [Suffix("seconds")]
        [SerializeField] private float _hideAfter;

        public void Setup(string message)
        {
            _text.text = message;
            Destroy(gameObject, _hideAfter);
        }
    }
}
