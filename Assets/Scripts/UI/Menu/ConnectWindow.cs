using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

namespace Assets.Scripts.UI.Menu
{
    public class ConnectWindow : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _adressEdit;
        [SerializeField] private UnityTransport _transport;

        public void Connect()
        {
            string adress = _adressEdit.text;
            string[] parts = adress.Split(':');

            if (parts.Length == 1)
                _transport.ConnectionData.Address = adress;
            else
            {
                var ip = parts[0];
                if (!ushort.TryParse(parts[1], out ushort port))
                    return;
                _transport.SetConnectionData(ip, port);
            }

            NetworkManager.Singleton.StartClient();
            gameObject.SetActive(false);
        }    
    }
}