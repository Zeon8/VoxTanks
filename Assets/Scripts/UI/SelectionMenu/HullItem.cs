using UnityEngine;

namespace VoxTanks.UI
{
    public class HullItem : SelectionMenuItem
    {
        [SerializeField] private int _id;

        public void OnClick()
        {
            GameSetupMenu.SelectHull(_id, this);
        }
    }
}