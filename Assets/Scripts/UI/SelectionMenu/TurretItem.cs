using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using VoxTanks.Tank;

namespace VoxTanks.UI
{
    public class TurretItem : SelectionMenuItem
    {
        [SerializeField] private int _id;

        public void OnClick()
        {
            GameSetupMenu.SelectTurret(_id, this);
        }
    }
}