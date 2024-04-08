namespace VoxTanks.Tank
{
    using System;
    using System.Collections.Generic;
    using Unity.Netcode;
    using UnityEngine;
    using VoxTanks.Supplies;

    public class TankSupply : NetworkBehaviour
    {
        [SerializeField] private readonly List<SupplyEffect> _supplies = new List<SupplyEffect>();

        private void OnCollisionEnter(Collision collisionInfo)
        {
            var container = collisionInfo.gameObject.GetComponent<SupplyContainer>();
            if (container != null && container.Supply.CanBeginUse(gameObject))
            {
                SupplyEffect supply = Instantiate(container.Supply);
                supply.StartUsing(gameObject);
                _supplies.Add(supply);
                if (IsServer)
                    container.Destroy();
            }
        }

        private void Update()
        {
            foreach (var supply in _supplies)
            {
                if (supply.Duration < supply.MaxDuration)
                {
                    supply.Update();
                    supply.Duration += Time.deltaTime;
                }
                else
                {
                    supply.FinishUsing();
                    _supplies.Remove(supply);
                }
            }
        }
    }
}