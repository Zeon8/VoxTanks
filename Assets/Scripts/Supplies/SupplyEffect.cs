using UnityEngine;
using VoxTanks.Tank;

namespace VoxTanks.Supplies
{
    public abstract class SupplyEffect : ScriptableObject
    {
        public float Duration { get; set; }

        public float MaxDuration => _maxUseTime;

        protected TankUI TankUI { get; private set; }

        [SerializeField] private float _maxUseTime;

        public virtual bool CanBeginUse(GameObject tank) => true;

        public virtual void StartUsing(GameObject tank) => TankUI = tank.GetComponent<TankUI>();

        public virtual void Update() { }

        public virtual void FinishUsing() { }
    }
}