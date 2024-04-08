using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VoxTanks.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public event Action Resumed;

        [SerializeField] private GameObject _menu;

        [SceneName]
        [SerializeField] private string _mainMenuScene;

        private void Start()
        {
            NetworkManager.Singleton.OnTransportFailure += Singleton_OnTransportFailure;
            SetActive(false);
        }

        private void Singleton_OnTransportFailure()
        {
            QuitToMenu();
        }

        public void SetActive(bool value)
        {
            _menu.SetActive(value);
        }

        public void Resume()
        {
            Resumed?.Invoke();
        }

        public void QuitToMenu()
        {
            NetworkManager.Singleton.Shutdown();
            Destroy(NetworkManager.Singleton.gameObject);
            SceneManager.LoadScene(_mainMenuScene);
        }
    }
}