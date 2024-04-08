using System.Collections;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank;

namespace VoxTanks.Tank
{
    public struct TankSettings : INetworkSerializable
    {
        public int Turret;

        public int Hull;

        public TankTeam Team;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref Turret);
            serializer.SerializeValue(ref Hull);
            serializer.SerializeValue(ref Team);
        }
    }
}