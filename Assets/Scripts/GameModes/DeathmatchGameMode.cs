using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank;
using VoxTanks.Tank.Spawners;

namespace VoxTanks.GameModes
{
    [CreateAssetMenu(menuName = "Game modes/Deathmatch")]
    public class DeathmatchGameMode : GameMode
    {
        public override ITankRespawner Respawner => FindObjectOfType<DefaultTankRespawner>();

        public override void Initialize() { }
    }
}
