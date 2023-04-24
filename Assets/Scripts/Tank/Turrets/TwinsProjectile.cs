using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank;

namespace VoxTanks.Tank.Turrets {
    public class TwinsProjectile : NetworkBehaviour
    {
        [SerializeField] private float _speed;

        private NetworkVariable<float> _damage = new NetworkVariable<float>();

        private string _playerName;
        private TankTeam _tankTeam;

        public void Setup(float damage,string playaerName, TankTeam tankTeam) 
        {
            _damage.Value = damage;
            _playerName = playaerName;
            _tankTeam = tankTeam;
        } 

        private void Update()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!IsServer)
                return;
            Transform root = other.transform.root;
            root?.GetComponent<TankHealth>()?.TakeDamage(_damage.Value,_playerName,_tankTeam);
            NetworkObject?.Despawn(true);

        }
    }
}