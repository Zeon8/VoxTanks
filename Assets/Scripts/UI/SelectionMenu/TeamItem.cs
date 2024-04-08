using UnityEngine;
using VoxTanks.Tank;

namespace VoxTanks.UI
{
    public class TeamItem : SelectionMenuItem
    {
        [SerializeField] private TankTeam _tankTeam;

        public void OnClick()
        {
            GameSetupMenu.SelectTeam(_tankTeam, this);
        }
    }
}
