using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank
{
    public class TankSetup : NetworkBehaviour
    {

        [SerializeField]
        private Behaviour[] _behaviours;
        [SerializeField]
        private GameObject[] _removeObjects;

        private NetworkVariable<ForceNetworkSerializeByMemcpy<FixedString64Bytes>> _playerName = new NetworkVariable<ForceNetworkSerializeByMemcpy<FixedString64Bytes>>();
        [SerializeField] private NetworkVariable<TankTeam> _team = new NetworkVariable<TankTeam>();

        public string Playername => _playerName.Value.Value.ToString();
        public TankTeam Team => _team.Value;

        public void Setup(string name, TankTeam team)
        {
            _playerName.Value = new ForceNetworkSerializeByMemcpy<FixedString64Bytes>(name);
            _team.Value = team;
        }

        public void Start()
        {
            if (IsLocalPlayer)
            {
                gameObject.tag = "Player";
                return;
            }

            if(string.IsNullOrWhiteSpace(_playerName.Value.Value.ToString()))
                _playerName.Value = new ForceNetworkSerializeByMemcpy<FixedString64Bytes>("Bot");

            foreach (var behaviour in _behaviours)
            {
                behaviour.enabled = false;
            }

            foreach (GameObject robject in _removeObjects)
            {
                Destroy(robject);
            }
        }

        [ContextMenu("Update List")]
        private void AddToList()
        {
            _behaviours = GetComponents<NetworkBehaviour>();
        }
    }
}