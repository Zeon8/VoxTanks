namespace VoxTanks.Tank
{
    using System;
    using System.Collections.Generic;
    using Unity.Netcode;
    using UnityEngine;
    using VoxTanks.Supplies;

    public class TankSupply : NetworkBehaviour
    {
        [SerializeField] private List<SupplyEffect> _supplies = new List<SupplyEffect>();

        private void OnCollisionEnter(Collision collisionInfo)
        {
            var container = collisionInfo.gameObject.GetComponent<SupplyContainer>();
            if (container != null && container.Supply.CanStartUse(gameObject))
            {
                SupplyEffect supply = Instantiate(container.Supply);
                supply.StartUse(gameObject);
                _supplies.Add(supply);
                if (IsServer)
                    container.Destroy();
            }
        }

        private void Update()
        {
            foreach (var supply in _supplies)
            {
                if (supply.UseTime < supply.MaxUseTime)
                {
                    supply.Update();
                    supply.UseTime += Time.deltaTime;
                }
                else
                {
                    supply.EndUse();
                    _supplies.Remove(supply);
                }
            }
        }
    }
}