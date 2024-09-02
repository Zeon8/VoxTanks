using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using VoxTanks.Supplies;

namespace VoxTanks.UI.Supply
{
    public class SupplyUI : MonoBehaviour
    {
        [SerializeField]
        private SerializedDictionary<SupplyEffectType, SupplyIcon> _supplyEffect = new();

        public void ResetProgress(SupplyEffectType type)
        {
            if (_supplyEffect.TryGetValue(type, out SupplyIcon icon))
                icon.Reset();
        }

        public void SetProgress(SupplyEffectType type, float progress)
        {
            if (_supplyEffect.TryGetValue(type, out SupplyIcon icon))
                icon.SetProgress(progress);
        }
    }
}