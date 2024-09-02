using System.Collections;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank;
using VoxTanks.UI.SelectionMenu;

namespace Assets.Scripts.Tank
{
    public class TankSelectionMenuOpener : NetworkBehaviour
    {
        private GameSetupModeToggler _toggler;
        private TankPause _tankPause;
        private bool _menuOpened;

        void Start()
        {
            if (!IsLocalPlayer)
                return;

            _tankPause = GetComponent<TankPause>();
            _toggler = FindObjectOfType<GameSetupModeToggler>();
            _toggler.CurrentMenu.Selected += CurrentMenu_Selected;
        }

        private void CurrentMenu_Selected(TankSettings obj) => ToggleMenu(false);

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.M))
            {
                ToggleMenu(!_menuOpened);
            }

            if (_menuOpened && Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleMenu(false);
            }
        }

        private void ToggleMenu(bool opened)
        {
            _menuOpened = opened;
            _toggler.SetVisible(opened);
            _tankPause.SetPaused(opened, showMenu: false);
        }
    }
}