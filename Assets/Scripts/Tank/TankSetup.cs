using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank
{
    public class TankSetup : NetworkBehaviour
    {
        public string Playername => _playerName.Value.Value.ToString();
        public TankTeam Team => _team.Value;

        [SerializeField] private Behaviour[] _behaviours;
        [SerializeField] private GameObject[] _removeObjects;
        [SerializeField] private NetworkVariable<TankTeam> _team = new NetworkVariable<TankTeam>();
        

        private NetworkVariable<ForceNetworkSerializeByMemcpy<FixedString64Bytes>> _playerName = new NetworkVariable<ForceNetworkSerializeByMemcpy<FixedString64Bytes>>();

        public void Start()
        {
            if (IsLocalPlayer)
            {
                gameObject.tag = "Player";
                return;
            }

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

        public void ApplySettings(TankSettings settings)
        {
            _playerName.Value = new ForceNetworkSerializeByMemcpy<FixedString64Bytes>(PlayerSettings.PlayerName);
            _team.Value = settings.Team;
        }
    }
}