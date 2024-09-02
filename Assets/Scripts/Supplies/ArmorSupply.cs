using UnityEngine;
using VoxTanks.Tank;

namespace VoxTanks.Supplies
{
    [CreateAssetMenu(menuName="Supplies/Armor")]
    public class ArmorSupply : SupplyEffect
    {
        public override SupplyEffectType EffectType => SupplyEffectType.Armor;

        [SerializeField] private float _armor = 1.5f;

        private TankHealth _tankHealth;

        public override void StartUsing(GameObject tank)
        {
            _tankHealth = tank.GetComponent<TankHealth>();
            _tankHealth.Armor = _armor;
        }

        public override void Stop()
        {
            _tankHealth.Armor = 1f;
        }

    }
}