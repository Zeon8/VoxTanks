using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VoxTanks.Tank;
using VoxTanks.Tank.Spawners;

namespace VoxTanks.GameModes
{
    public abstract class BaseGameMode : ScriptableObject
    {
        public abstract ITankRespawner Respawner { get; }

        public abstract void Initialize();
    }
}
