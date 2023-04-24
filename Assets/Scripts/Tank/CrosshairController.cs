using Unity.Netcode;
using Unity.Netcode.Transports.UNET;
using UnityEngine;
using VoxTanks.UI;

namespace VoxTanks.Tank {
    public class CrosshairController : NetworkBehaviour
    {
        private Crossghair _crossghair;

        [SerializeField] private Transform _muzzle;

        void Start()
        {
            _crossghair = FindObjectOfType<Crossghair>();
        }

        private void Update()
        {
            _crossghair?.SetPosition(_muzzle);
        }

    }
}