using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Netcode.Transports.PhotonRealtime;
using Photon.Realtime;
using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.UI.Menu
{
    public class PhotonGame : LoadBalancingClient, IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks
    {
        private const int MAX_DGRAM_PER_FRAME = 4;
        private PhotonRealtimeTransport2 _transport;

        public event Action OnConnectedToMaster;

        public event Action<List<RoomInfo>> OnRoomListUpdate;

        public void Setup()
        {
            _transport = (PhotonRealtimeTransport2)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
            AddCallbackTarget(this);
            ConnectUsingSettings(PhotonAppSettings.Instance.AppSettings);
        }

        public void CreateOrJoinGame(string mapName, string gamemode)
        {
            var roomProperties = new Hashtable
            {
                ["map"] = mapName,
                ["gm"] = gamemode
            };
            _transport.RoomName = null;
            _transport.Lobby = null;
            _transport.CustomRoomProperties = roomProperties;
            var roomParams = new OpJoinRandomRoomParams()
            {
                ExpectedCustomRoomProperties = roomProperties
            };
            OpJoinRandomRoom(roomParams);
        }

        public void Update()
        {
            do { } while (LoadBalancingPeer.DispatchIncomingCommands());
        }

        public void LateUpdate()
        {
            for (int i = 0; i < MAX_DGRAM_PER_FRAME; i++)
            {
                bool anythingLeftToSend = LoadBalancingPeer.SendOutgoingCommands();
                if (!anythingLeftToSend)
                    break;
            }
        }

        void IConnectionCallbacks.OnConnected() { }

        void IConnectionCallbacks.OnConnectedToMaster()
        {
            Debug.Log("Connected to master!");
            OnConnectedToMaster?.Invoke();
        }

        void IConnectionCallbacks.OnDisconnected(DisconnectCause cause) { }

        void IConnectionCallbacks.OnRegionListReceived(RegionHandler regionHandler) { }

        void IConnectionCallbacks.OnCustomAuthenticationResponse(Dictionary<string, object> data) { }

        void IConnectionCallbacks.OnCustomAuthenticationFailed(string debugMessage) { }

        void IMatchmakingCallbacks.OnFriendListUpdate(List<FriendInfo> friendList) { }

        void IMatchmakingCallbacks.OnCreatedRoom() { }

        void IMatchmakingCallbacks.OnCreateRoomFailed(short returnCode, string message) { }

        void IMatchmakingCallbacks.OnJoinedRoom()
        {
            Disconnect();
            NetworkManager.Singleton.StartClient();
        }

        void IMatchmakingCallbacks.OnJoinRoomFailed(short returnCode, string message) { }

        void IMatchmakingCallbacks.OnJoinRandomFailed(short returnCode, string message) => NetworkManager.Singleton.StartHost();

        void IMatchmakingCallbacks.OnLeftRoom() { }

        void ILobbyCallbacks.OnJoinedLobby() => Debug.Log("Joined to lobby!");
        void ILobbyCallbacks.OnLeftLobby() => Debug.Log("Joined to lobby!");

        void ILobbyCallbacks.OnRoomListUpdate(List<RoomInfo> roomList) => OnRoomListUpdate?.Invoke(roomList);

        void ILobbyCallbacks.OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics) { }
    }
}