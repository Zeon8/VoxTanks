using Unity.Netcode;
using UnityEngine;
using VoxTanks.UI;

namespace VoxTanks.Tank {
    public class CrosshairController : NetworkBehaviour
    {
        [SerializeField] private Transform _muzzle;

        private Crossghair _crossghair;

        private void Start() => _crossghair = FindObjectOfType<Crossghair>();

        private void Update()
        {
            _crossghair?.UpdatePosition(_muzzle);
        }
    }
}