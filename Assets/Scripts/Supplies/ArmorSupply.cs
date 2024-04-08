using UnityEngine;
using VoxTanks.Tank;

namespace VoxTanks.Supplies
{
    [CreateAssetMenu(menuName="Supplies/Armor")]
    public class ArmorSupply : SupplyEffect
    {
        [SerializeField] private float _armor = 1.5f;

        private TankHealth _tankHealth;

        public override void StartUsing(GameObject tank)
        {
            base.StartUsing(tank);
            _tankHealth = tank.GetComponent<TankHealth>();
            _tankHealth.Armor = _armor;
        }

        public override void Update()
        {
            TankUI.SetArmorProgressClientRpc(Duration / MaxDuration);
        }

        public override void FinishUsing()
        {
            _tankHealth.Armor = 1f;
            TankUI.SetArmorProgressClientRpc(0);
        }

    }
}