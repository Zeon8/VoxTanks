using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Effects
{
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField] private float _destroyAfter;

        private void Start()
        {
            StartCoroutine(Destroy());
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(_destroyAfter);
            Destroy(gameObject);
        }

    }
}