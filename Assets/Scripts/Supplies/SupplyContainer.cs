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
            if (Parachute != null)
            {
                GetComponent<Rigidbody>().drag = _defalutDrag;
                Destroy(Parachute);
            }
        }

        [ClientRpc]
        public void DestroyClientRpc() => StartCoroutine(Destroy());

        private IEnumerator Destroy()
        {
            _collider.enabled = false;
            _rigidBody.isKinematic = true;
            _rigidBody.rotation = Quaternion.identity;

            _animator.Play(_animationName);
            yield return new WaitForSeconds(_destroyAfter);

            if (IsServer)
                NetworkObject.Despawn(true);
        }
    }
}