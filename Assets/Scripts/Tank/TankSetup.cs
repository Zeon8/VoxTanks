using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank
{
    public class TankSetup : NetworkBehaviour
    {
        public string PlayerName { get; private set; }

        public TankTeam Team { get; private set; }

        [SerializeField] private Behaviour[] _behaviours;
        [SerializeField] private GameObject[] _removeObjects;

        private void Start()
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

            foreach (GameObject @object in _removeObjects)
            {
                Destroy(@object);
            }
        }

        public void ApplySettings(TankSettings settings)
        {
            PlayerName = settings.PlayerName;
            Team = settings.Team;
        }
    }
}