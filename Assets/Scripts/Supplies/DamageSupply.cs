using UnityEngine;
using VoxTanks.Tank;
using VoxTanks.Tank.Turrets;

namespace VoxTanks.Supplies
{
    [System.Serializable]
    [CreateAssetMenuAttribute(menuName="Supplies/Damage")]
    public class DamageSupply : SupplyEffect
    {
        [SerializeField] private float _damage = 1.5f;

        private ITankTurret _tankTurret;

        public override void StartUse(GameObject tank)
        {
            base.StartUse(tank);
            _tankTurret = tank.GetComponentInChildren<ITankTurret>();
            _tankTurret.AdditionalDamage = _damage;
        }

        public override void Update()
        {
            TankUI.SetDamageProgressClientRpc(UseTime / MaxUseTime);
        }

        public override void EndUse()
        {
            _tankTurret.AdditionalDamage = 1;
            TankUI.SetDamageProgressClientRpc(0);
        }

    }
}