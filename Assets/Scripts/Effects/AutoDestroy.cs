using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Effects
{
    public class AutoDestroy : NetworkBehaviour
    {
        public float Duration { get; private set; }

        public event Action OnDestroyed;

        [SerializeField] private float _waitTime;

        private void Start()
        {
            if (!IsServer)
                return;
            StartCoroutine(Destroy());
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(_waitTime);
            OnDestroyed?.Invoke();
            NetworkObject.Despawn(true);

        }

    }
}