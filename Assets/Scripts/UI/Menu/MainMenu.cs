using UnityEngine;

namespace VoxTanks.UI.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [ReorderableList]
        [SerializeField] private GameObject[] _menus;

        public void QuitGame()
        {
            Application.Quit();
        }

        private void HideMenus()
        {
            foreach (var menu in _menus)
            {
                if(menu != null)
                    menu.SetActive(false);
            }
        }

        public void OpenMenu(GameObject menu)
        {
            HideMenus();
            menu.SetActive(true);
        }
    }
}