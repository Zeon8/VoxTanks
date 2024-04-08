using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VoxTanks.Game;
using VoxTanks.Tank;

namespace VoxTanks.Game
{
    [CreateAssetMenu]
    public class GameSettings : ScriptableObject
    {
        [field:SerializeField]
        public GameInfo BattleInfo { get; set; }
    }
}
