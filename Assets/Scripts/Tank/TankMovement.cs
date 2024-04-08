using System;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank;

namespace VoxTanks
{
    public class TankMovement : NetworkBehaviour
    {
        public float Nitro { get; set; } = 1;

        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private TankSound _tankSound;

        private TankHullSettings _hullSettings;
        private Transform _transform;

        public override void OnNetworkSpawn()
        {
            _transform = transform.root;
        }

        private void FixedUpdate()
        {
            if (!IsLocalPlayer || _hullSettings is null)
                return;

            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            MoveServerRpc(vertical, horizontal);
        }

        [ServerRpc]
        private void MoveServerRpc(float vertical, float horizontal)
        {
            float motorTorque = 0;
            float brakeTorque = 0;

            if(_rigidBody.velocity.magnitude <= _hullSettings.TopSpeed * Nitro)
                motorTorque = _hullSettings.MotorTorque * vertical * Nitro;

            if (vertical == 0)
            {
                brakeTorque = _hullSettings.MotorTorque;
                _tankSound.PlayStopMoveClientRpc();
            }
            else
                _tankSound.PlayMoveClientRpc();

            foreach (WheelCollider wheel in _hullSettings.Wheels)
            {
                wheel.motorTorque = motorTorque;
                wheel.brakeTorque = brakeTorque;
            }

            _transform.Rotate(0, horizontal * _hullSettings.RotationSpeed, 0);
        }

        public void SetSettings(TankHullSettings settings)
        {
            _hullSettings = settings;
        }

        private void OnDisable()
        {
            StopMovement();
        }

        private void StopMovement() => MoveServerRpc(0, 0);
    }
}
