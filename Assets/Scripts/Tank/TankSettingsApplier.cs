using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using VoxTanks.Tank.Turrets;
using VoxTanks.UI;
using VoxTanks.UI.SelectionMenu;

namespace VoxTanks.Tank
{
    public class TankSettingsApplier : NetworkBehaviour
    {
        [field: SerializeField]
        public UnityEvent<TankSettings> OnSettingsChanged { get; private set; } 

        [SerializeField] private GameObject[] _turrets;
        [SerializeField] private GameObject[] _hulls;

        [SerializeField] private Transform _turretParent;

        private NetworkVariable<TankSettings> _settings = new NetworkVariable<TankSettings>();
        private TankSettings? _settingsForRespawn;
        private GameSetupMenu _menu;

        private void Start()
        {
            _settings.OnValueChanged = (_, settings) => Apply(settings);
            _menu = FindObjectOfType<GameSetupModeToggler>().CurrentMenu;

            if (IsLocalPlayer)
            {
                _menu.Selected += settings => ApplyAfterRespawnServerRpc(settings);
                SetSettingsServerRpc(_menu.Settings);
            }
            else if (!IsServer)
                Apply(_settings.Value);

            if (IsServer)
                GetComponent<TankLife>().Respawned += TankSettingsApplier_Respawned;
        }

        [ServerRpc]
        public void SetSettingsServerRpc(TankSettings settings) => _settings.Value = settings;

        private void Apply(TankSettings settings)
        {
            ApplyTurretAndHull(settings);
            OnSettingsChanged.Invoke(settings);
        }

        [ServerRpc]
        public void ApplyAfterRespawnServerRpc(TankSettings settings)
        {
            _settingsForRespawn = settings;
        }

        private void TankSettingsApplier_Respawned()
        {
            if (_settingsForRespawn.HasValue)
            {
                _settings.Value = _settingsForRespawn.Value;
                _settingsForRespawn = null;
            }
        }

        private void ApplyTurretAndHull(TankSettings settings)
        {
            ApplyTurret(settings.Turret);
            ApplyHull(settings.Hull);
        }

        private void ApplyTurret(int selectedTurret)
        {
            SelectPart(_turrets, selectedTurret);
        }

        private void ApplyHull(int selectedHull)
        {
            SelectPart(_hulls, selectedHull);
            if(IsServer)
                _hulls[selectedHull].GetComponent<TankHullSettings>().Apply();
        }

        private void SelectPart(GameObject[] parts, int selectedPart)
        {
            if (selectedPart > parts.Length)
                return;

            for (int i = 0; i < parts.Length; i++)
            {
                if (i != selectedPart)
                {
                    GameObject part = parts[i];
                    part.SetActive(false);
                }
            }
            parts[selectedPart].SetActive(true);
        }
    }
}