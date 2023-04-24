using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Netcode.Transports.PhotonRealtime;
using Photon.Realtime;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.UI.Menu;

public class FindRoomMenu : BaseRoomMenu
{
    private PhotonGame _photonGame = new PhotonGame();

    [SerializeField] private RoomMenuItem _roomItemPrefab;
    [SerializeField] private Transform _content;

    private TypedLobby _lobby = new TypedLobby("custom", LobbyType.Default);

    private new void Start()
    {
        _photonGame.Setup();
        _photonGame.OnConnectedToMaster += () => _photonGame.OpJoinLobby(_lobby);
        _photonGame.OnRoomListUpdate += OnRoomListUpdate;
    }

    private void OnRoomListUpdate(List<RoomInfo> rooms)
    {
        ClearItems();
        foreach (RoomInfo room in rooms)
        {
            RoomMenuItem roomItem = Instantiate(_roomItemPrefab, _content);
            string map = room.CustomProperties["map"] as string;
            string gamemode = room.CustomProperties["gm"] as string;
            roomItem.Setup(room.Name,map,gamemode,room.PlayerCount,room.MaxPlayers,() => JoinRoom(room.Name));
        }
    }

    private void JoinRoom(string name)
    {
        var transport = (PhotonRealtimeTransport2)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        transport.RoomName = name;
        transport.Lobby = _lobby;
        NetworkManager.Singleton.StartClient();
    }

    private void ClearItems()
    {
        foreach (Transform item in _content)
            Destroy(item.gameObject);
    }

    private void Update()
    {
        _photonGame.Update();
    }

    private void LateUpdate()
    {
        _photonGame.LateUpdate();
    }
}