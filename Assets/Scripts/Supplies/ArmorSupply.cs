namespace VoxTanks.Supplies
{
    using UnityEngine;
    using VoxTanks.Tank;
    [System.Serializable]
    [CreateAssetMenuAttribute(menuName="Supplies/Armor")]
    public class ArmorSupply : SupplyEffect
    {
        [SerializeField] private float _armor = 1.5f;

        private TankHealth _tankHealth;

        public override void StartUse(GameObject tank)
        {
            base.StartUse(tank);
            _tankHealth = tank.GetComponent<TankHealth>();
            _tankHealth.Armor = _armor;
        }


        public override void Update()
        {
            TankUI.SetArmorProgressClientRpc(UseTime / MaxUseTime);
        }

        public override void EndUse()
        {
            _tankHealth.Armor = 1f;
            TankUI.SetArmorProgressClientRpc(0);
        }

    }
}