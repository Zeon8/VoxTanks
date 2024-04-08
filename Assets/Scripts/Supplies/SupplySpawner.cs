using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Supplies
{
    public class SupplySpawner : MonoBehaviour
    {
        [SerializeField] private SupplyContainer[] _supplies;

        [EditorButton(nameof(SpawnMedkit), "Spawn medkit", ButtonActivityType.OnPlayMode)]
        [EditorButton(nameof(SpawnNitro), "Spawn nitro kit", ButtonActivityType.OnPlayMode)]
        [EditorButton(nameof(SpawnDamage), "Spawn damage kit", ButtonActivityType.OnPlayMode)]
        [EditorButton(nameof(SpawnArmor), "Spawn armor kit", ButtonActivityType.OnPlayMode)]

        [SerializeField] private Vector2 _spawnPeriodRange;

        private float _spawnPeriod;
        private float _time;

        private void SpawnMedkit() => SpawnSupply(_supplies.First(c => c.Supply is MedkitSupply));
        private void SpawnNitro() => SpawnSupply(_supplies.First(c => c.Supply is NitroSupply));
        private void SpawnDamage() => SpawnSupply(_supplies.First(c => c.Supply is DamageSupply));
        private void SpawnArmor() => SpawnSupply(_supplies.First(c => c.Supply is ArmorSupply));

        private void SpawnSupply(SupplyContainer supply)
        {
            SupplyContainer container = Instantiate(supply, transform.position, Quaternion.identity);
            container.NetworkObject.Spawn();
        }

        private void Start()
        {
            _spawnPeriod = Random.Range(_spawnPeriodRange.x, _spawnPeriodRange.y);
        }

        private void Update()
        {
            if (!NetworkManager.Singleton.IsServer)
                return;

            if (_time < _spawnPeriod)
                _time += Time.deltaTime;
            else
            {
                _time = 0;
                if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit)
                    && hit.collider.GetComponent<SupplyContainer>() == null)
                {
                    int supply = Random.Range(0, _supplies.Length);
                    SpawnSupply(_supplies[supply]);
                }

            }
            
        }
    }
}