using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using VoxTanks.Tank;

namespace VoxTanks.UI
{
    public class TurretItem : SelectionMenuItem
    {
        [SerializeField] private int _id;

        public override void OnClick()
        {
            GameSetupMenu.SelectTurret(_id);
            base.OnClick();
        }
    }
}