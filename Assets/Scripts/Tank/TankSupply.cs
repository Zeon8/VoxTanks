using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.Supplies;

namespace VoxTanks.Tank
{
    public class TankSupply : NetworkBehaviour
    {
        private TankUI _tankUI;

        private static readonly int s_suppliesCount = Enum.GetNames(typeof(SupplyEffectType)).Length;

        private readonly Dictionary<SupplyEffectType, SupplyEffect> _supplies = new(s_suppliesCount);
        private readonly List<SupplyEffectType> _supplyKeys = new();

        private void Start()
        {
            if (!IsServer)
            {
                enabled = false;
                return;
            }
             _tankUI = GetComponent<TankUI>();
        }

        private void OnCollisionEnter(Collision collisionInfo)
        {
            var collidedObject = collisionInfo.gameObject;
            if (collidedObject.TryGetComponent(out SupplyContainer container)
                && container.Supply.CanBeUsed(gameObject))
            {
                if (_supplies.TryGetValue(container.Supply.EffectType, out SupplyEffect supply))
                {
                    supply.Time = 0;
                    container.DestroyClientRpc();
                    return;
                }

                supply = Instantiate(container.Supply);
                supply.StartUsing(gameObject);
                _supplies.Add(supply.EffectType, supply);
                _supplyKeys.Add(supply.EffectType);
                container.DestroyClientRpc();
            }
        }

        private void Update()
        {
            for (int i = _supplies.Count - 1; i >= 0; i--)
            {
                var key = _supplyKeys[i];
                var supply = _supplies[key];
                if (supply.Time < supply.Duration)
                {
                    supply.Update();
                    supply.Time += Time.deltaTime;
                    _tankUI.SetSupplyProgressClientRpc(supply.EffectType, supply.Time / supply.Duration);
                }
                else
                {
                    supply.Stop();
                    _supplies.Remove(key);
                    _supplyKeys.Remove(key);
                    _tankUI.ResetProgressClientRpc(supply.EffectType);
                }
            }
        }
    }
}