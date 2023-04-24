using System;
using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank.Turrets
{
    [RequireComponent(typeof(Animator))]
    public class TurretAnimator : NetworkBehaviour
    {
        [SerializeField] private string _animationName = "shooting";
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        [ClientRpc]
        public void PlayShootAnimationClientRpc()
        {
            Debug.Log("Activated!");
            _animator?.SetTrigger(_animationName);
        }

        public void StopShootAnimation() => _animator?.ResetTrigger(_animationName);
    }
}