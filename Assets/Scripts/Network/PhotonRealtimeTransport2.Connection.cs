using System;
using System.Collections.Generic;
using Photon.Realtime;
using Unity.Netcode;
using UnityEngine;
using WebSocketSharp;

namespace Netcode.Transports.PhotonRealtime
{
    public partial class PhotonRealtimeTransport2 : IConnectionCallbacks
    {
        public RoomOptions RoomOptions;
        public bool JoinToLobby { get; set; }
        public TypedLobby Lobby { get; set; }

        public void OnConnected()
        {
        }

        public void OnConnectedToMaster()
        {
            // Once the client does connect to the master immediately redirect to its room.
            bool success;
            if(m_IsHostOrServer)
            {
                var roomParams = new EnterRoomParams()
                {
                    RoomName = RoomName,
                    Lobby = Lobby,
                    RoomOptions = new RoomOptions()
                    {
                        MaxPlayers = m_MaxPlayers,
                        CustomRoomProperties = CustomRoomProperties,
                        CustomRoomPropertiesForLobby = new string[] { "map", "gm" }
                    }
                };
                success = m_Client.OpCreateRoom(roomParams);
            }
            else
            {
                if(Lobby is null)
                    success = m_Client.OpJoinRandomRoom(new OpJoinRandomRoomParams()
                    {
                        ExpectedCustomRoomProperties = CustomRoomProperties,
                    });
                else
                    success = m_Client.OpJoinRoom(new EnterRoomParams
                    {
                        RoomName = RoomName,
                        Lobby = Lobby,
                    });
            }
            
            if (!success)
            {
                Debug.LogWarning("Unable to create or join room.");
                InvokeTransportEvent(NetworkEvent.Disconnect);
            }
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
        }

        public void OnDisconnected(DisconnectCause cause)
        {
            InvokeTransportEvent(NetworkEvent.Disconnect);
            this.DeInitialize();
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
        }
    }
}
