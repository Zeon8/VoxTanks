namespace VoxTanks.Supplies
{
    using System;
    using UnityEngine;
    using VoxTanks.Tank;

    [System.Serializable]
    [CreateAssetMenuAttribute(menuName="Supplies/Nitro")]
    public class NitroSupply : SupplyEffect
    {
        [SerializeField] private float _nitro = 2f;
        private TankMovement _tankMovement;

        public override void StartUse(GameObject tank)
        {
            if(tank is null)
                throw new ArgumentException();
            
            base.StartUse(tank);

            _tankMovement = tank.GetComponentInChildren<TankMovement>();
            if(_tankMovement != null)
                _tankMovement.Nitro = _nitro;
        }

        public override void Update()
        {
            TankUI.SetNitroProgressClientRpc(UseTime / MaxUseTime);
        }

        public override void EndUse()
        {
            if(_tankMovement != null)
                _tankMovement.Nitro = 1f;
            
            TankUI.SetNitroProgressClientRpc(0);
        }
    }
}