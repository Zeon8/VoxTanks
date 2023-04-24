using UnityEngine;

namespace VoxTanks.UI
{
    public class HullItem : SelectionMenuItem
    {
        [SerializeField] private int _id;
        public override void OnClick()
        {
            GameSetupMenu.SelectHull(_id);
            base.OnClick();
        }
    }
}