using System;
using System.Net;
using UnityEngine;

namespace VoxTanks.Game
{
    public class GameNetworkDiscovery : NetworkDiscovery<DiscoveryBroadcastData, GameInfo>
    {
        public GameInfo DiscoveryData { get; set; }

        [SerializeField] private FindRoomMenu _menu;

        protected override bool ProcessBroadcast(IPEndPoint sender, DiscoveryBroadcastData broadCast, out GameInfo response)
        {
            response = DiscoveryData;
            return true;
        }

        protected override void ResponseReceived(IPEndPoint sender, GameInfo response)
        {
            _menu.AddRoom(response, sender.Address);
        }
    }
}
