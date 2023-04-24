using System;
using UnityEngine;

namespace VoxTanks.Tank
{
    public class TankSettings : MonoBehaviour
    {

        /// <summary>Returns Turret id</summary>
        public event Action<int> OnTurretChanged;

        public void SetTurret(int turret) => OnTurretChanged?.Invoke(turret);
    }
}
