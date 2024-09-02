using UnityEngine;
using VoxTanks.Tank;

namespace VoxTanks.Supplies
{
    [CreateAssetMenuAttribute(menuName="Supplies/Medkit")]
    public class MedkitSupply : SupplyEffect
    {
        public override SupplyEffectType EffectType => SupplyEffectType.RepairKit;

        [SerializeField] private float _healSpeed = 1f;
        private TankHealth _tankHealth;

        public override bool CanBeUsed(GameObject tank)
        {
            var tankHealth = tank.GetComponent<TankHealth>();
            return !tankHealth.HasFullHealth;
        }

        public override void StartUsing(GameObject tank)
        {
            var tankHealth = tank.GetComponent<TankHealth>();
            _tankHealth = tankHealth;
        }

        public override void Update()
        {
            _tankHealth.Heal(UnityEngine.Time.fixedDeltaTime * _healSpeed);

            if (_tankHealth.HasFullHealth)
                Terminate();
        }
    }
}