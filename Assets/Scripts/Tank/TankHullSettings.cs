using System.Collections;
using UnityEngine;
using VoxTanks.Tank;

namespace VoxTanks
{
    public class TankHullSettings : MonoBehaviour
    {
        [SerializeField] private float _health;

        [field: SerializeField] 
        public WheelCollider[] Wheels { get; private set; }

        [field: SerializeField] 
        public Transform[] WheelMeshes { get; private set; }

        [field: SerializeField] 
        public float MotorTorque { get; private set; } = 50;

        [field: SerializeField] 
        public float TopSpeed { get; private set; }

        [field: SerializeField] 
        public float RotationSpeed { get; private set; }

        public void Apply()
        {
            GetComponentInParent<TankHealth>().SetTankHealth(_health);
            GetComponentInParent<TankMovement>().SetSettings(this);
        }
    }
}