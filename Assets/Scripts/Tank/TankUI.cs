using Unity.Netcode;
using UnityEngine;
using VoxTanks.UI;
using VoxTanks.UI.Supply;

namespace VoxTanks.Tank
{
    public class TankUI : NetworkBehaviour
    {
        private SupplyUI _supplyUI;
        private IStatusBar _statusUI;


        private void Start()
        {
            _supplyUI = FindObjectOfType<SupplyUI>();
            _statusUI = FindObjectOfType<StatusBar>();
        }

        [ClientRpc]
        public void SetArmorProgressClientRpc(float progress)
        {
            if (IsLocalPlayer)
                _supplyUI.Armor.SetProgress(progress);
        }

        [ClientRpc]
        public void SetMedkitProgressClientRpc(float progress)
        {
            if (IsLocalPlayer)
                _supplyUI.Medkit.SetProgress(progress);
        }

        [ClientRpc]
        public void SetDamageProgressClientRpc(float progress)
        {
            if (IsLocalPlayer)
                _supplyUI.Damage.SetProgress(progress);
        }

        [ClientRpc]
        public void SetNitroProgressClientRpc(float progress)
        {
            if (IsLocalPlayer)
                _supplyUI.Nitro.SetProgress(progress);
        }

        [ClientRpc]
        public void SetUIVisibleClientRpc(bool value)
        {
            if (IsLocalPlayer)
                _statusUI.SetVisible(value);
        }

    }
}