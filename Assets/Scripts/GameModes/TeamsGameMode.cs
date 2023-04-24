using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using VoxTanks.Tank;
using VoxTanks.Tank.Spawners;

namespace VoxTanks.GameModes
{
    public class TeamsGameMode : DefaultGameMode
    {
        public override ITankRespawner Respawner => FindObjectOfType<TeamTankRespawner>();

        protected InfoTab InfoTab;

        protected virtual void Start()
        {
            //SceneManager.activeSceneChanged +=(previous,nextScene) => OnGameStarted();
            OnGameStarted();
        }

        protected virtual void OnGameStarted()
        {
            InfoTab = FindObjectOfType<InfoTab>();
        }
    }
}
