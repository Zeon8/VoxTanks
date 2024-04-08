namespace VoxTanks.Supplies
{
    using System;
    using UnityEngine;
    using VoxTanks.Tank;

    [CreateAssetMenuAttribute(menuName="Supplies/Nitro")]
    public class NitroSupply : SupplyEffect
    {
        [SerializeField] private float _nitro = 2f;

        private TankMovement _tankMovement;

        public override void StartUsing(GameObject tank)
        {
            if(tank == null)
                throw new ArgumentException();
            
            base.StartUsing(tank);

            _tankMovement = tank.GetComponentInChildren<TankMovement>();
            if(_tankMovement != null)
                _tankMovement.Nitro = _nitro;
        }

        public override void Update()
        {
            TankUI.SetNitroProgressClientRpc(Duration / MaxDuration);
        }

        public override void FinishUsing()
        {
            if(_tankMovement != null)
                _tankMovement.Nitro = 1f;
            
            TankUI.SetNitroProgressClientRpc(0);
        }
    }
}