using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

namespace Vox.UI.Menu
{
    public class ErrorReporter : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private MessageBox _messageBoxPrefab;

        private void Start()
        {
            NetworkManager.Singleton.OnClientStopped += Singleton_OnClientStopped;
        }

        private void OnDestroy()
        {
            NetworkManager.Singleton.OnClientStopped -= Singleton_OnClientStopped;
        }

        private void Singleton_OnClientStopped(bool isHostMode)
        {
            if(!isHostMode)
                ShowMessage("Error", "Failed to connect to server");
        }

        private void ShowMessage(string title, string message)
        {
            MessageBox messageBox = Instantiate(_messageBoxPrefab, _parent);
            messageBox.Setup(title, message);
        }
    }
}