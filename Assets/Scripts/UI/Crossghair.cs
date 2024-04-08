using UnityEngine;
using UnityEngine.UI;
using VoxTanks.UI.Supply;

namespace VoxTanks.UI
{
    public class Crossghair : MonoBehaviour, ICrossghair
    {
        private Image _image;

        public bool Visible
        {
            get => _image.enabled;
            set => _image.enabled = value;
        }

        public Vector3 Position => transform.position;

        private void Start()
        {
            _image = GetComponent<Image>();
        }

        public void UpdatePosition(Transform _muzzle)
        {
            bool hitted = Physics.Raycast(_muzzle.position, _muzzle.forward, out RaycastHit hit);
            Visible = hitted;

            if (hitted)
            {
                Vector3 position = Camera.main.WorldToScreenPoint(hit.point);
                position.y = transform.position.y;
                transform.position = position;
            }
        }

    }
}
