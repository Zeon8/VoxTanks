using System.Collections;
using System.Collections.Generic;
using Netcode.Transports.PhotonRealtime;
using Photon.Realtime;
using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.UI.Menu
{
    public class MainMenu : MonoBehaviour
    {

        [SerializeField] private GameObject _createRoomWindow;
        [SerializeField] private GameObject _findGameWindow;
        [SerializeField] private GameObject _findRoomWindow;

        public void OnCreateCustomRoomClicked()
        {
            _findGameWindow.SetActive(false);
            _createRoomWindow.SetActive(true);
            _findRoomWindow.SetActive(false);
        }

        public void OnPlayClicked()
        {
            _findGameWindow.SetActive(true);
            _createRoomWindow.SetActive(false);
            _findRoomWindow.SetActive(false);
        }

        public void OnFindRoomClicked()
        {
            _findRoomWindow.SetActive(true);
            _createRoomWindow.SetActive(false);
            _findGameWindow.SetActive(false);
        }
    }
}