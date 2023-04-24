using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank
{
    public class TankPause : NetworkBehaviour
    {
        [SerializeField]
        private Behaviour[] _components;

        private bool _paused = false;

        private void Start()
        {
            if(IsLocalPlayer)
                Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (IsLocalPlayer && Input.GetKeyDown(KeyCode.Escape))
            {
                _paused = !_paused;
                Cursor.lockState = _paused ? CursorLockMode.None : CursorLockMode.Locked;
                foreach (Behaviour component in _components)
                {
                    component.enabled = !_paused;
                }
            }
        }

    }
}