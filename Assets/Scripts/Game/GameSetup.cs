using System.Linq;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.GameModes;
using VoxTanks.Tank;

namespace VoxTanks.Game
{
    public class GameSetup : NetworkBehaviour
    {
        public GameMode CurrentGameMode { get; private set; }

        [ReorderableList(Foldable = true)]
        [SerializeField] private GameMode[] _gameModes;
        [SerializeField] private GameSettings _gameSettings;

        private readonly NetworkVariable<GameInfo> _battleInfo = new NetworkVariable<GameInfo>();

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                _battleInfo.Value = _gameSettings.BattleInfo;
            }

            CurrentGameMode = _gameModes.First(gm => gm.name == _battleInfo.Value.GameMode);
        }

        private void Start()
        {
            CurrentGameMode.Initialize();
        }
    }
}