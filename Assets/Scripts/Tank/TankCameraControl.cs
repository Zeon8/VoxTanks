using Unity.Netcode;
using UnityEditor;
using UnityEngine;

namespace VoxTanks.Tank
{
    public class TankCameraControl : MonoBehaviour
    {
        [SerializeField] private Transform _defaultCameraPosition;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _distanceWhenObsticle;
        [SerializeField] private Transform _camera;
        [SerializeField] private float _sensivityX;
        [SerializeField] private float _sensivityY;
        [SerializeField] private float _followSpeed;

        [SerializeField] private float _minYAngle;
        [SerializeField] private float _maxYAngle;

        private Transform _tankTransform;
        private TankPause _tankPause;
        private Vector3 _rotation = Vector3.zero;
        private Vector3 _hitPoint;
        private float _distance;

        public void Setup(Transform tankTransform)
        {
            _tankTransform = tankTransform;
            _tankPause = _tankTransform.GetComponent<TankPause>();
            _distance = Vector3.Distance(transform.position, _defaultCameraPosition.position);
        }

        private void Update()
        {
            if (!_tankPause.Paused)
            {
                Rotate();

                var direction = _defaultCameraPosition.position - transform.position;
                if (Physics.Raycast(transform.position, direction, out RaycastHit hit, _distance, _layerMask.value))
                {
                    _hitPoint = hit.point;
                    Vector3 position = _defaultCameraPosition.localPosition;
                    position.z = -Vector3.Distance(transform.position, hit.point) / _distanceWhenObsticle;
                    _camera.localPosition = position;
                }
                else
                    _camera.position = _defaultCameraPosition.position;
            }
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
            return Mathf.Clamp(angle, _minYAngle, _maxYAngle);
        }

        private void FollowTank()
        {
            transform.position = _tankTransform.position;
        }
    }
}