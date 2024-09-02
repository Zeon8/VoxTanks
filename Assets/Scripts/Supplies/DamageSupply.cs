using UnityEngine;
using VoxTanks.Tank;
using VoxTanks.Tank.Turrets;

namespace VoxTanks.Supplies
{
    [CreateAssetMenuAttribute(menuName="Supplies/Damage")]
    public class DamageSupply : SupplyEffect
    {
        public override SupplyEffectType EffectType => SupplyEffectType.Damage;

        [SerializeField] private float _damage = 1.5f;

        private ITankTurret _tankTurret;

        public override void StartUsing(GameObject tank)
        {
            _tankTurret = tank.GetComponentInChildren<ITankTurret>();
            _tankTurret.AdditionalDamage = _damage;
        }

        public override void Stop()
        {
            _tankTurret.AdditionalDamage = 1;
        }

    }
}