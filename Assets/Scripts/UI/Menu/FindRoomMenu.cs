using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using VoxTanks.Game;
using VoxTanks.UI.Menu;

public class FindRoomMenu : MonoBehaviour
{
    [SerializeField] private GameNetworkDiscovery _discovery;
    [SerializeField] private UnityTransport _transport;

    [SerializeField] private RoomMenuItem _roomItemPrefab;
    [SerializeField] private Transform _content;

    private void Start()
    {
        _discovery.StartClient();
    }

    public void AddRoom(GameInfo battle, IPAddress address)
    {
        RoomMenuItem roomItem = Instantiate(_roomItemPrefab, _content);
        roomItem.Setup(battle, () => JoinRoom(address));
    }

    private void JoinRoom(IPAddress address)
    {
        _transport.ConnectionData.Address = address.ToString();
        NetworkManager.Singleton.StartClient();
    }

    private void ClearItems()
    {
        foreach (Transform item in _content)
            Destroy(item.gameObject);
    }

    public void Refresh()
    {
        ClearItems();
        _discovery.ClientBroadcast(new DiscoveryBroadcastData());
    }
}