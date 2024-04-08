using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using VoxTanks.UI.SelectionMenu;

namespace VoxTanks.Tank
{

    public class TankSettingsApplier : NetworkBehaviour
    {
        [field: SerializeField]
        public UnityEvent<TankSettings> OnSettingsChanged { get; private set; } 

        [SerializeField] private GameObject[] _turrets;
        [SerializeField] private GameObject[] _hulls;

        private NetworkVariable<TankSettings> _settings = new NetworkVariable<TankSettings>();
        private TankSettings? _settingsForRespawn;

        public void Start()
        {
            if (IsLocalPlayer)
            {
                FindObjectOfType<GameSetupModeToggler>().CurrentMenu.Selected +=
                    settings => ApplySettingsAfterRespawnServerRpc(settings);
            }

            if (IsClient)
            {
                _settings.OnValueChanged = (_, value) => ApplyTurretAndHull(value);
                ApplyTurretAndHull(_settings.Value);
            }
            if(IsServer)
                GetComponent<TankLife>().Respawned += TankSettingsApplier_Respawned;
        }

        [ServerRpc]
        public void ApplySettingsServerRpc(TankSettings settings)
        {
            _settings.Value = settings;
            OnSettingsChanged.Invoke(settings);
            ApplyTurretAndHull(settings);
        }

        [ServerRpc]
        public void ApplySettingsAfterRespawnServerRpc(TankSettings settings)
        {
            _settingsForRespawn = settings;
        }

        private void TankSettingsApplier_Respawned()
        {
            if (_settingsForRespawn.HasValue)
            {
                ApplySettingsServerRpc(_settingsForRespawn.Value);
                _settingsForRespawn = null;
            }
        }

        private void ApplyTurretAndHull(TankSettings settings)
        {
            ApplyTurret(settings.Turret);
            ApplyHull(settings.Hull);
        }

        private void ApplyTurret(int selectedTurret) => SelectPart(_turrets, selectedTurret);
        private void ApplyHull(int selectedHull)
        {
            SelectPart(_hulls, selectedHull);
            _hulls[selectedHull].GetComponent<TankHullSettings>().Apply();
        }

        private void SelectPart(GameObject[] parts, int selectedPart)
        {
            if (selectedPart > parts.Length)
                return;

            foreach (var part in parts)
                part.SetActive(false);

            parts[selectedPart].SetActive(true);
        }

        
    }
}