using System.Collections;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.Game;
using VoxTanks.GameModes;

namespace VoxTanks.Tank
{
    public class TankDestroyer : NetworkBehaviour
    {
        [SerializeField] private GameObject _destroyTankEffect;
        [SerializeField] private float _respawnTime;

        private TankUI _tankUI;
        private TankSetup _tankSetup; 
        private KillTab _killTab;
        private TankLife _tankLife;
        private TankFlagCatcher _tankFlagCatcher;
        private TeamDeathmatchGameMode _teamsDeathmatchMode;
        private bool _isDestroyed;

        private void Start()
        {
            _tankLife = GetComponent<TankLife>();
            _tankUI = GetComponent<TankUI>();  

            if (!IsServer)
                return;

            _tankSetup = GetComponent<TankSetup>();
            _killTab = FindObjectOfType<KillTab>();
            _tankFlagCatcher = GetComponent<TankFlagCatcher>();

            if (FindObjectOfType<GameSetup>().CurrentGameMode is TeamDeathmatchGameMode gameMode)
                _teamsDeathmatchMode = gameMode;

            if (!IsLocalPlayer)
                enabled = false;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.K))
                DestroyServerRpc();
        }

        public void Destroy(string attacker, TankTeam attackerTeam)
        {
            StartCoroutine(Destroy());
            AddGameScore(attacker, attackerTeam);
        }

        private void AddGameScore(string attacker, TankTeam tankTeam)
        {
            string victim = _tankSetup.PlayerName;
            _killTab.AddKillClientRpc(attacker, victim);
            if (_teamsDeathmatchMode != null)
                _teamsDeathmatchMode.AddTeamScore(tankTeam);
        }

        [ContextMenu("Destroy tank")]
        [ServerRpc]
        private void DestroyServerRpc()
        {
            if(!_isDestroyed)
                StartCoroutine(Destroy());
        }

        private IEnumerator Destroy()
        {
            _isDestroyed = true;
            SetTankEnabledClientRpc(false);
            SpawnEffectClientRpc();
            if (_tankFlagCatcher != null)
                _tankFlagCatcher.DropFlag();
            yield return new WaitForSeconds(_respawnTime);
            RespawnTank();
            _isDestroyed = false;
        }

        [ClientRpc]
        private void SetTankEnabledClientRpc(bool state)
        {
            _tankLife.SetTankActive(state);
            if(IsLocalPlayer)
                _tankUI.SetUIVisible(state);
        }

        [ClientRpc]
        private void SpawnEffectClientRpc()
        {
            Instantiate(_destroyTankEffect, transform.position, Quaternion.identity);
        }

        private void RespawnTank()
        {
            SetTankEnabledClientRpc(true);
            _tankLife.Respawn();
        }
    }
}