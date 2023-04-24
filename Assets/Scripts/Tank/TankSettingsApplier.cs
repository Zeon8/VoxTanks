using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank
{

    public class TankSettingsApplier : NetworkBehaviour
    {
        public TankSettings TankSettings { get; set; }

        [SerializeField] private GameObject[] _turrets;
        [SerializeField] private GameObject[] _hulls;

        private NetworkVariable<int> _selectedTurret = new NetworkVariable<int>();
        private NetworkVariable<int> _selectedHull = new NetworkVariable<int>();

        private void Start()
        {
            SelectTurret(_selectedTurret.Value);
            SelectHull(_selectedHull.Value);
        }

        [ServerRpc(RequireOwnership=false)]
        public void SelectTurretServerRpc(int selectedTurret)
        {
            _selectedTurret.Value = selectedTurret;
            SelectTurretClientRpc(selectedTurret);
        }

        [ServerRpc(RequireOwnership = false)]
        public void SelectHullServerRpc(int selectedHull)
        {
            _selectedHull.Value = selectedHull;
            SelectHullClientRpc(selectedHull);
        }

        [ClientRpc]
        public void SelectTurretClientRpc(int selectedTurret) => SelectTurret(selectedTurret);

        [ClientRpc]
        public void SelectHullClientRpc(int selectedHull) => SelectHull(selectedHull);

        private void SelectTurret(int selectedTurret)
        {
            if (selectedTurret > _turrets.Length)
                return;

            foreach (var turret in _turrets)
                turret.SetActive(false);

            _turrets[selectedTurret].SetActive(true);
        }

        private void SelectHull(int selectedHull)
        {
            if (selectedHull > _hulls.Length)
                return;

            foreach (var hull in _hulls)
                hull.SetActive(false);
            _hulls[selectedHull].SetActive(true);
        }
    }
}