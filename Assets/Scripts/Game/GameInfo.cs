using System;
using Unity.Netcode;

namespace VoxTanks.Game
{
    [Serializable]
    public struct GameInfo : INetworkSerializable
    {
        public string Name;

        public string Map;

        public string GameMode;

        public byte PlayerCount;

        public byte MaxPlayerCount;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) 
            where T : IReaderWriter
        {
            serializer.SerializeValue(ref Name);
            serializer.SerializeValue(ref Map);
            serializer.SerializeValue(ref GameMode);
            serializer.SerializeValue(ref PlayerCount);
            serializer.SerializeValue(ref MaxPlayerCount);
        }
    }
}
