using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.UI;

namespace VoxTanks.Tank
{
    public class TankPause : NetworkBehaviour
    {
        [field: SerializeField, Disable]
        public bool Paused { get; private set; } = false;

        [SerializeField] private List<Behaviour> _components = new List<Behaviour>();

        private PauseMenu _menu;

        public void AddBehavour(Behaviour behaviour)
        {
            _components.Add(behaviour);
        }

        private void Start()
        {
            if (IsLocalPlayer)
            {
                Cursor.lockState = CursorLockMode.Locked;
                _menu = FindAnyObjectByType<PauseMenu>();
                _menu.Resumed += PauseMenu_Resumed;
            }
        }

        private void PauseMenu_Resumed() => SetPaused(false);

        private void Update()
        {
            if (IsLocalPlayer && Input.GetKeyDown(KeyCode.Escape))
            {
                SetPaused(!Paused);
            }
        }

        public void SetPaused(bool paused)
        {
            Paused = paused;
            Cursor.lockState = Paused ? CursorLockMode.None : CursorLockMode.Locked;
            foreach (Behaviour component in _components)
            {
                component.enabled = !Paused;
            }
            _menu.SetActive(paused);
        }

        private void OnDisable()
        {
            _menu.Resumed -= PauseMenu_Resumed;
        }
    }
}