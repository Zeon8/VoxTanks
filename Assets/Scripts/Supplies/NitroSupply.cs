namespace VoxTanks.Supplies
{
    using System;
    using UnityEngine;
    using VoxTanks.Tank;

    [CreateAssetMenuAttribute(menuName="Supplies/Nitro")]
    public class NitroSupply : SupplyEffect
    {
        public override SupplyEffectType EffectType => SupplyEffectType.Nitro;

        [SerializeField] private float _nitro = 2f;

        private TankMovement _tankMovement;

        public override void StartUsing(GameObject tank)
        {
            _tankMovement = tank.GetComponent<TankMovement>();
            _tankMovement.Nitro = _nitro;
        }

        public override void Stop()
        {
            _tankMovement.Nitro = 1f;
        }
    }
}