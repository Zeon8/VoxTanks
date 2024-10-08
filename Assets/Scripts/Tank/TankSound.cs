using System;
using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank
{
    public class TankSound : NetworkBehaviour
    {

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioSource _movementAudioSource;
        [SerializeField] private AudioClip _useSupplyClip;
        [SerializeField] private AudioClip[] _shotSounds;

        [ClientRpc]
        public void PlayMoveClientRpc()
        {
            if (!_movementAudioSource.isPlaying)
                _movementAudioSource.Play();
        }

        [ClientRpc]
        public void PlayStopMoveClientRpc() => _movementAudioSource.Stop();

        [ClientRpc]
        public void PlayShotClientRpc(int index) => _audioSource.PlayOneShot(_shotSounds[index]);
    }
}