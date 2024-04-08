using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using VoxTanks.Tank;
using VoxTanks.Tank.Spawners;

namespace VoxTanks.GameModes
{
    public abstract class BaseTeamsGameMode : DeathmatchGameMode
    {
        public override ITankRespawner Respawner => FindObjectOfType<TeamTankRespawner>();
        protected InfoTab InfoTab { get; private set; }

        public override void Initialize()
        {
            InfoTab = FindAnyObjectByType<InfoTab>();   
        }
    }
}
