using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank;

namespace VoxTanks.UI
{
    public class GameSetupMenu : MonoBehaviour
    {
        private IPlayerSetup _playerSetup;
        private int _selectedTurret;
        private TankTeam _selectedTeam = TankTeam.None;

        [SerializeField] private Camera _mapCamera;

        [SerializeField] private TurretItem[] _turretItems;
        [SerializeField] private TeamItem[] _teamItems;
        [SerializeField] private HullItem[] _hullItems;
        
        private int _selectedHull;

        private void Start()
        {
            _playerSetup = NetworkManager.Singleton.GetComponent<IPlayerSetup>();
        }

        public void SelectTurret(int turret)
        {
            _selectedTurret = turret;
            foreach (var turretItem in _turretItems)
                turretItem.Deselect();
        }

        public void SelectHull(int hull)
        {
            _selectedHull = hull;
            foreach (var hullItem in _hullItems)
                hullItem.Deselect();
        }

        public void SelectTeam(TankTeam team)
        {
            Debug.Log(team);
            _selectedTeam = team;
            foreach (TeamItem teamItem in _teamItems)
                teamItem.Deselect();
        }

        public void Ready()
        {
            //Debug.Log(_selectedTeam);
            _playerSetup.SetPlayerReady(_selectedTurret,_selectedHull,_selectedTeam);
            _mapCamera.enabled = false;
            gameObject.SetActive(false);
        }
    }
}