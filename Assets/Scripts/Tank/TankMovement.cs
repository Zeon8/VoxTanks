using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank;

namespace VoxTanks
{
    public class TankMovement : NetworkBehaviour
    {
        public float Nitro {get; set;} = 1;

        [SerializeField] private WheelCollider[] _wheels;
        [SerializeField] private Transform[] _wheelMeshes;

        [SerializeField] private float _motorTorque = 50;
        [SerializeField] private float _topSpeed;
        [SerializeField] private float _rotationSpeed;

        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private TankSound _tankSound;

        private Transform _transform;

        public override void OnNetworkSpawn()
        {
            _transform = transform.root;
        }

        private void FixedUpdate()
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            ProcessInputServerRpc(vertical, horizontal);
        }

        [ServerRpc]
        private void ProcessInputServerRpc(float vertical, float horizontal)
        {
            float motorTorque = 0;
            float brakeTorque = 0;

            if(_rigidBody.velocity.magnitude <= _topSpeed * Nitro)
                motorTorque = _motorTorque * vertical * Nitro;

            if (vertical == 0)
            {
                brakeTorque = _motorTorque;
                _tankSound.PlayStopMoveClientRpc();
            }
            else
                _tankSound.PlayMoveClientRpc();

            foreach (WheelCollider wheel in _wheels)
            {
                wheel.motorTorque = motorTorque;
                wheel.brakeTorque = brakeTorque;
            }

            _transform.Rotate(0, horizontal * _rotationSpeed, 0);
        }
    }
}
