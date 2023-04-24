namespace VoxTanks.Supplies
{
    using UnityEngine;
    using UnityEngine.PlayerLoop;
    using VoxTanks.Tank;

    [System.Serializable]
    public abstract class SupplyEffect : ScriptableObject
    {
        public float UseTime;

        [SerializeField] private float _maxUseTime;

        public float MaxUseTime => _maxUseTime;

        protected TankUI TankUI { get; private set; }

        public virtual bool CanStartUse(GameObject tank) => true;

        public virtual void StartUse(GameObject tank) => TankUI = tank.GetComponent<TankUI>();

        public virtual void Update() { }

        public virtual void EndUse() { }
    }
}