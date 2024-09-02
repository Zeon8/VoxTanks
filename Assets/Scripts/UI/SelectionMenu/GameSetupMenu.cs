using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.GameModes;
using VoxTanks.Game;
using VoxTanks.Tank;

namespace VoxTanks.UI
{
    public class GameSetupMenu : MonoBehaviour
    {
        public TankSettings Settings => _tankSettings;

        public event Action<TankSettings> Selected;

        [SerializeField] private Camera _mapCamera;
        [SerializeField] private TurretItem[] _turretItems;
        [SerializeField] private TeamItem[] _teamItems;
        [SerializeField] private HullItem[] _hullItems;
        [SerializeField] private GameSetup _gameSetup;

        private TankSettings _tankSettings;

        public void SelectTurret(int turret, SelectionMenuItem item)
        {
            _tankSettings.Turret = turret;
            foreach (var turretItem in _turretItems)
                turretItem.Deselect();
            item.Select();
        }

        public void SelectHull(int hull, SelectionMenuItem item)
        {
            _tankSettings.Hull = hull;

            foreach (var hullItem in _hullItems)
                hullItem.Deselect();
            item.Select();
        }

        public void SelectTeam(TankTeam team, SelectionMenuItem item)
        {
            _tankSettings.Team = team;
            foreach (TeamItem teamItem in _teamItems)
                teamItem.Deselect();
            item.Select();
        }

        public void Ready()
        {
            _mapCamera.enabled = false;
            gameObject.SetActive(false);
            _tankSettings.PlayerName = PlayerSettings.PlayerName;
            Selected?.Invoke(_tankSettings);
        }
    }
}