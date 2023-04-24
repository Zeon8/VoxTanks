using UnityEngine;

namespace VoxTanks.UI
{
    public interface ICrossghair
    {
        bool Visible { get; set; }

        Vector3 Position { get; }
        void SetPosition(Transform _muzzle);
    }
}
