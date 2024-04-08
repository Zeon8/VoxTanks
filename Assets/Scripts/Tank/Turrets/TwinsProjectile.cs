using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank;

namespace VoxTanks.Tank.Turrets {
    public class TwinsProjectile : NetworkBehaviour
    {
        [SerializeField] private float _speed;

        private float _damage;
        private string _playerName;
        private TankTeam _tankTeam;

        public void Setup(float damage, string playerName, TankTeam tankTeam)
        {
            _damage = damage;
            _playerName = playerName;
            _tankTeam = tankTeam;
        } 

        private void Update()
        {
            transform.position += _speed * Time.deltaTime * transform.forward;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!IsServer)
                return;

            Debug.Log("Hitted: "+other.gameObject);

            var tankHealth = other.transform.GetComponentInParent<TankHealth>();
            if (tankHealth != null)
                tankHealth.TakeDamage(_damage, _playerName, _tankTeam);

            NetworkObject.Despawn(true);
        }
    }
}