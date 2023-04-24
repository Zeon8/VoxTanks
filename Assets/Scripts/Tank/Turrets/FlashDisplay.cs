using UnityEngine;
using Unity.Netcode;

namespace VoxTanks.Tank.Turrets
{
    public class FlashDisplay : NetworkBehaviour
    {
        [SerializeField] private GameObject _shotEffect;

        [ClientRpc]
        public void ShowFlashClientRpc() => _shotEffect.SetActive(true);

        [ClientRpc]
        public void HideClientRpc() => _shotEffect.SetActive(false);
    }
}