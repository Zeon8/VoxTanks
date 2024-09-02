using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank.Turrets;
using VoxTanks.UI;

namespace VoxTanks.Tank
{
    public class TankPause : NetworkBehaviour
    {
        [field: SerializeField, Disable]
        public bool Paused { get; private set; } = false;

        [SerializeField] private List<Behaviour> _components = new List<Behaviour>();

        private PauseMenu _menu;

        private void Start()
        {
            if (!IsLocalPlayer)
            {
                enabled = false;
                return;
            }

            Cursor.lockState = CursorLockMode.Locked;
            _menu = FindAnyObjectByType<PauseMenu>();
            _menu.Resumed += PauseMenu_Resumed;
            _components.Add(GetComponentInChildren<TankTurret>());
        }

        private void PauseMenu_Resumed() => SetPaused(false, showMenu: true);

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SetPaused(!Paused, showMenu: true);
        }
        public void AddBehavour(Behaviour behaviour)
        {
            _components.Add(behaviour);
        }

        public void SetPaused(bool paused, bool showMenu)
        {
            Paused = paused;
            Cursor.lockState = Paused ? CursorLockMode.None : CursorLockMode.Locked;
            foreach (Behaviour component in _components)
                component.enabled = !Paused;
            _menu.SetActive(paused && showMenu);
        }
    }
}