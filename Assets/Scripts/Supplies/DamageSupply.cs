using UnityEngine;
using VoxTanks.Tank;
using VoxTanks.Tank.Turrets;

namespace VoxTanks.Supplies
{
    [CreateAssetMenuAttribute(menuName="Supplies/Damage")]
    public class DamageSupply : SupplyEffect
    {
        [SerializeField] private float _damage = 1.5f;

        private ITankTurret _tankTurret;

        public override void StartUsing(GameObject tank)
        {
            base.StartUsing(tank);
            _tankTurret = tank.GetComponentInChildren<ITankTurret>();
            _tankTurret.AdditionalDamage = _damage;
        }

        public override void Update()
        {
            TankUI.SetDamageProgressClientRpc(Duration / MaxDuration);
        }

        public override void FinishUsing()
        {
            _tankTurret.AdditionalDamage = 1;
            TankUI.SetDamageProgressClientRpc(0);
        }

    }
}