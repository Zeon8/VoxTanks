using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank;

namespace VoxTanks.GameModes.FlagMode
{
    public class MapFlagsHidder : MonoBehaviour
    {
        private void Start()
        {
            if(!NetworkManager.Singleton.GetComponent<TeamFlagsMode>())
                gameObject.SetActive(false);
        }
    }
}
