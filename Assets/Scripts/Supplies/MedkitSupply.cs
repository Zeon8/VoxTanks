namespace VoxTanks.Supplies
{
    using UnityEngine;
    using VoxTanks.Tank;

    [System.Serializable]
    [CreateAssetMenuAttribute(menuName="Supplies/Medkit")]
    public class MedkitSupply : SupplyEffect
    {

        [SerializeField] private float _healSpeed = 1f;
        private TankHealth _tankHealth;

        public override bool CanStartUse(GameObject tank)
        {
            var tankHealth = tank.GetComponent<TankHealth>();
            return !tankHealth.HasFullHealth;
        }

        public override void StartUse(GameObject tank)
        {
            base.StartUse(tank);
            var tankHealth = tank.GetComponent<TankHealth>();
            _tankHealth = tankHealth;
        }


        public override void Update()
        {
            _tankHealth.Heal(Time.fixedDeltaTime * _healSpeed);

            TankUI.SetMedkitProgressClientRpc(UseTime / MaxUseTime);

            if (_tankHealth.HasFullHealth)
            {
                UseTime = MaxUseTime;
            }
        }

        public override void EndUse()
        {
            TankUI.SetMedkitProgressClientRpc(0);
        }
    }
}