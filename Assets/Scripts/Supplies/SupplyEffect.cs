using UnityEngine;
using VoxTanks.Tank;

namespace VoxTanks.Supplies
{
    public abstract class SupplyEffect : ScriptableObject
    {
        public float Time { get; set; }

        public abstract SupplyEffectType EffectType { get; }

        public float Duration => _maxUseTime;

        [SerializeField] private float _maxUseTime;

        public void Terminate() => Time = Duration; 

        public virtual bool CanBeUsed(GameObject tank) => true;

        public virtual void StartUsing(GameObject tank) { }

        public virtual void Update() { }

        public virtual void Stop() { }
    }
}