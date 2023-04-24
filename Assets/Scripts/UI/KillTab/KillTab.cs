using Unity.Netcode;
using UnityEngine;
using VoxTanks.UI;

namespace VoxTanks.GameModes
{
    public class KillTab : NetworkBehaviour
    {
        [SerializeField] private KillTabItem _itemPrefab;
        [SerializeField] private Transform _kiilsContainer;

        [ClientRpc]
        public void AddKillClientRpc(string attacker, string victim)
        {
            KillTabItem item = Instantiate(_itemPrefab, _kiilsContainer);
            item.Setup(attacker, victim);
        }
    }
}
