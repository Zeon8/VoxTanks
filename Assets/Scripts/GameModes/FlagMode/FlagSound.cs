using System;
using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.GameModes.FlagMode
{
    public class FlagSound : NetworkBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _capturedFlagSound;
        [SerializeField] private AudioClip _lostFlagSound;
        [SerializeField] private AudioClip _returnedFlagSound;
        [SerializeField] private AudioClip _deliveredFlagSound;
        
        public void PlayFlagCaptured() => _audioSource.PlayOneShot(_capturedFlagSound);

        public void PlayFlagLost() => _audioSource.PlayOneShot(_lostFlagSound);

        public void PlayFlagReturned() => _audioSource.PlayOneShot(_returnedFlagSound);

        public void PlayFlagDelivered() => _audioSource.PlayOneShot(_deliveredFlagSound);
    }
}
