namespace VoxTanks.Supplies
{
    using Unity.Netcode;
    using UnityEngine;

    public class SupplySpawner : MonoBehaviour
    {
        [SerializeField] private SupplyContainer _medkitSupply;
        [SerializeField] private SupplyContainer _nitroSupply;
        [SerializeField] private SupplyContainer _damageSupply;

        [EditorButton(nameof(SpawnMedkit), "Spawn medkit", ButtonActivityType.OnPlayMode)]
        [EditorButton(nameof(SpawnNitro), "Spawn nitro kit", ButtonActivityType.OnPlayMode)]
        [EditorButton(nameof(SpawnDamage), "Spawn damage kit", ButtonActivityType.OnPlayMode)]
        [EditorButton(nameof(SpawnArmor), "Spawn armor kit", ButtonActivityType.OnPlayMode)]
        [SerializeField] private SupplyContainer _armorSupply;

        private void SpawnMedkit() => SpawnSupply(_medkitSupply);
        private void SpawnNitro() => SpawnSupply(_nitroSupply);
        private void SpawnDamage() => SpawnSupply(_damageSupply);
        private void SpawnArmor() => SpawnSupply(_armorSupply);

        private void SpawnSupply(SupplyContainer supply)
        {
            SupplyContainer container = Instantiate(supply, transform.position, Quaternion.identity);
            container.NetworkObject.Spawn();
        }
    }
}