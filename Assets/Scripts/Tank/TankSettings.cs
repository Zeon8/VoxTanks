using Unity.Netcode;

namespace VoxTanks.Tank
{
    public struct TankSettings : INetworkSerializable
    {
        public string PlayerName;

        public int Turret;

        public int Hull;

        public TankTeam Team;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            // Hack, because unity netcode is buggy
            if (PlayerName is null)
                PlayerName = string.Empty;

            serializer.SerializeValue(ref PlayerName);
            serializer.SerializeValue(ref Turret);
            serializer.SerializeValue(ref Hull);
            serializer.SerializeValue(ref Team);
        }
    }
}