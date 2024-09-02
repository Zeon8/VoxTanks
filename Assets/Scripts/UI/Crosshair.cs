using UnityEngine;
using UnityEngine.UI;
using VoxTanks.UI.Supply;

namespace VoxTanks.UI
{
    public class Crosshair : MonoBehaviour
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
            Visible = false;
        }

        public void UpdatePosition(Transform _muzzle)
        {
            if (Camera.main == null)
                return;

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
