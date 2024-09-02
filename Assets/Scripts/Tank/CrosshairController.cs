using Unity.Netcode;
using UnityEngine;
using VoxTanks.UI;

namespace VoxTanks.Tank {
    public class CrosshairController : NetworkBehaviour
    {
        [SerializeField] private Transform _muzzle;

        private Crosshair _crossghair;

        private void Start()
        {
            if (!IsLocalPlayer)
            {
                enabled = false;
                return;
            }

            _crossghair = FindObjectOfType<Crosshair>();
        }

        private void Update()
        {
            _crossghair.UpdatePosition(_muzzle);
        }
    }
}