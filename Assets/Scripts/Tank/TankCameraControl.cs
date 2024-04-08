using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank
{
    public class TankCameraControl : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private float _sensivityX;
        [SerializeField] private float _sensivityY;
        [SerializeField] private float _followSpeed;

        private Transform _tankTransform;
        private TankPause _tankPause;
        private Vector3 _rotation = Vector3.zero;

        public void Setup(Transform tankTransform)
        {
            _tankTransform = tankTransform;
            _tankPause = _tankTransform.GetComponent<TankPause>();
        }

        private void Update()
        {
            if(!_tankPause.Paused)
                Rotate();
        }

        private void LateUpdate()
        {
            FollowTank();
        }

        private void Rotate()
        {
            float mouseX = Input.GetAxis("Mouse X") * _sensivityX;
            float mouseY = -Input.GetAxis("Mouse Y") * _sensivityY;

            _rotation.y += mouseX;
            _rotation.x = Clamp(_rotation.x + mouseY);

            transform.localEulerAngles = _rotation;
        }

        private float Clamp(float angle)
        {
            if (angle > 180)
            {
                angle -= 360;
            }
            return Mathf.Clamp(angle, -30, 60);
        }

        private void FollowTank()
        {
            transform.position = _tankTransform.position;
        }
    }
}