namespace VoxTanks.Supplies
{
    using UnityEngine;
    using VoxTanks.Tank;

    [CreateAssetMenuAttribute(menuName="Supplies/Medkit")]
    public class MedkitSupply : SupplyEffect
    {

        [SerializeField] private float _healSpeed = 1f;
        private TankHealth _tankHealth;

        public override bool CanBeginUse(GameObject tank)
        {
            var tankHealth = tank.GetComponent<TankHealth>();
            return !tankHealth.HasFullHealth;
        }

        public override void StartUsing(GameObject tank)
        {
            base.StartUsing(tank);
            var tankHealth = tank.GetComponent<TankHealth>();
            _tankHealth = tankHealth;
        }


        public override void Update()
        {
            _tankHealth.Heal(Time.fixedDeltaTime * _healSpeed);

            TankUI.SetMedkitProgressClientRpc(Duration / MaxDuration);

            if (_tankHealth.HasFullHealth)
            {
                Duration = MaxDuration;
            }
        }

        public override void FinishUsing()
        {
            TankUI.SetMedkitProgressClientRpc(0);
        }
    }
}