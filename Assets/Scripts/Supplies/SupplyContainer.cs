namespace VoxTanks.Supplies
{
    using System.Collections;
    using Unity.Netcode;
    using UnityEngine;
    using VoxTanks.Tank;

    public class SupplyContainer : NetworkBehaviour
    {
        public SupplyEffect Supply => _supply;

        [SerializeField] private GameObject Parachute;
        [SerializeField] private float _defalutDrag; // Rigidbody
        [SerializeField] private SupplyEffect _supply;
        [SerializeField] private string _animationName;
        [SerializeField] private int _destroyAfter;
        [SerializeField] private Collider _collider;

        private Animator _animator;
        private Rigidbody _rigidBody;
        private bool _started;
        protected TankUI TankUI;

        void Start()
        {
            if (!IsServer)
                enabled = false;

            _animator = GetComponent<Animator>();
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter()
        {
            GetComponent<Rigidbody>().drag = _defalutDrag;
            Destroy(Parachute);
        }

        public void Destroy()
        {
            _collider.enabled = false;
            _rigidBody.isKinematic = true;
            _animator.Play(_animationName);
            StartCoroutine(DestroyAfter(_destroyAfter));
        }

        private IEnumerator DestroyAfter(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            NetworkObject.Despawn();
        }
    }
}