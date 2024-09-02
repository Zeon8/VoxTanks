using Unity.Netcode;
using UnityEngine;
using VoxTanks.Supplies;
using VoxTanks.UI;
using VoxTanks.UI.Supply;

namespace VoxTanks.Tank
{
    public class TankUI : NetworkBehaviour
    {
        private SupplyUI _supplyUI;
        private StatusBar _statusUI;

        private void Start()
        {
            if (!IsLocalPlayer)
                return;

            _supplyUI = FindObjectOfType<SupplyUI>();
            _statusUI = FindObjectOfType<StatusBar>();
        }

        [ClientRpc]
        public void SetSupplyProgressClientRpc(SupplyEffectType type, float progress)
        {
            if (IsLocalPlayer)
                _supplyUI.SetProgress(type, progress);
        }

        [ClientRpc]
        public void ResetProgressClientRpc(SupplyEffectType type)
        {
            if (IsLocalPlayer)
                _supplyUI.ResetProgress(type);
        }


        public void SetUIVisible(bool value)
        {
            _statusUI.SetVisible(value);
        }

    }
}