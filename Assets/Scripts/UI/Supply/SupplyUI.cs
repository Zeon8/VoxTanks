using UnityEngine;

namespace VoxTanks.UI.Supply
{
    public class SupplyUI : MonoBehaviour
    {
        [SerializeField] private SupplyIcon _medkit;
        [SerializeField] private SupplyIcon _nitro;
        [SerializeField] private SupplyIcon _damage;
        [SerializeField] private SupplyIcon _armor;

        public SupplyIcon Medkit => _medkit;
        public SupplyIcon Nitro => _nitro;
        public SupplyIcon Damage => _damage;
        public SupplyIcon Armor => _armor;

    }
}