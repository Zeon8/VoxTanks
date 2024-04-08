using Unity.Netcode;
using UnityEngine;
using VoxTanks.Game;
using VoxTanks.Tank;

namespace VoxTanks.GameModes.FlagMode
{
    public class MapFlagsHidder : MonoBehaviour
    {
        [SerializeField] private GameSetup _gameSetup;

        private void Start()
        {
            if (_gameSetup.CurrentGameMode is not CatchFlagMode)
                gameObject.SetActive(false);
        }
    }
}
