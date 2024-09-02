using UnityEngine;
using Unity.Netcode;

namespace VoxTanks.Tank.Turrets
{
    public class FlashDisplay : NetworkBehaviour
    {
        [SerializeField] private GameObject _shotEffect;

        public void Show() => _shotEffect.SetActive(true);

        public void Hide() => _shotEffect.SetActive(false);
    }
}