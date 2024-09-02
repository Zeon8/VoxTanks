using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank.Turrets;

namespace VoxTanks.Tank
{
    public class TankTurretControl : NetworkBehaviour
    {
        [SerializeField] private Transform _turret;
        [SerializeField] private int _rotationSpeed;
        [SerializeField] private TankCameraControl _cameraControlPrefab;
        
        private Transform _muzzle;
        private Transform _camera;

        private void Start()
        {
            if (!IsLocalPlayer)
                return;

            var cameraControl = Instantiate(_cameraControlPrefab);
            cameraControl.Setup(transform);
            _camera = cameraControl.transform; 
        }

        public void ApplySettings(TankSettings settings)
        {
            if (IsServer || IsLocalPlayer)
            {
                var turret = GetComponentInChildren<TankTurret>();
                _muzzle = turret.Mazzle;
            }
        }

        private void FixedUpdate()
        {
            if (_muzzle == null)
                return;

            if (_camera.localRotation.normalized.y != _turret.localRotation.normalized.y)
            {
                float cameraY = _camera.eulerAngles.y - transform.eulerAngles.y;
                RotateTurretServerRpc(cameraY);
            }
            if (_camera.rotation.normalized.x != _muzzle.rotation.normalized.x)
            {
                RotateMuzzleServerRpc(_camera.localEulerAngles.x);
            }
        }

        [ServerRpc]
        private void RotateTurretServerRpc(float cameraY)
        {
            var cameraRotation = Quaternion.Euler(0, cameraY, 0);
            var turretRotation = Quaternion.Euler(0, _turret.localEulerAngles.y, 0);
            var rotation = Quaternion.RotateTowards(turretRotation, cameraRotation, _rotationSpeed);
            _turret.localRotation = rotation;
        }

        [ServerRpc]
        private void RotateMuzzleServerRpc(float cameraX)
        {
            var muzzleRotation = _muzzle.eulerAngles;

            var cameraRotation = muzzleRotation;
            cameraRotation.x = cameraX;

            var rotation = Quaternion.RotateTowards(Quaternion.Euler(muzzleRotation), Quaternion.Euler(cameraRotation), _rotationSpeed);
            _muzzle.rotation = rotation;

            var angles = new Vector3(_muzzle.localEulerAngles.x, 180, 0);
            _muzzle.localEulerAngles = angles;
        }
    }
}