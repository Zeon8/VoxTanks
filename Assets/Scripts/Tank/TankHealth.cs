using Unity.Netcode;
using UnityEngine;
using VoxTanks.Effects;
using VoxTanks.GameModes;
using VoxTanks.Game;
using VoxTanks.Tank.Spawners;
using VoxTanks.UI;

namespace VoxTanks.Tank
{
    public class TankHealth : NetworkBehaviour
    {
        public float Armor { get; set; } = 1f;

        public bool HasFullHealth => _health.Value == _maxHealth;

        [SerializeField] private NetworkVariable<float> _health = new NetworkVariable<float>(100f);
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private NetworkObject _destroyTankEffect;
        [SerializeField] private TankUI _tankUI;

        private IStatusBar _statusUI;
        private KillTab _killTab;
        private TeamDeathmatchGameMode  _teamsDeathmatchMode;
        private TankSetup _tankSetup;
        private TankFlagCatcher _tankFlagCatcher;
        private TankSound _tankSound;
        private TankLife _tankLife;


        private void Start()
        {
            _health.Value = _maxHealth;
            _health.OnValueChanged += HealthChanged;
            _statusUI = FindObjectOfType<StatusBar>();
            _tankSetup = GetComponent<TankSetup>();
            _tankLife = GetComponent<TankLife>();
            _killTab = FindObjectOfType<KillTab>();
            _tankSound = GetComponent<TankSound>();
            _tankFlagCatcher = GetComponent<TankFlagCatcher>();

            if(FindObjectOfType<GameSetup>().CurrentGameMode is TeamDeathmatchGameMode gameMode)
                _teamsDeathmatchMode = gameMode;
        }

        public void HealthChanged(float oldValue, float newValue)
        {
            if (IsLocalPlayer)
                _statusUI.SetHealthProgress(_health.Value / _maxHealth);
        }

        public void Heal(float health)
        {
            _health.Value += health;
            if (_health.Value > _maxHealth)
                _health.Value = _maxHealth;
        }

        public void SetTankHealth(float health)
        {
            _maxHealth = health;
            _health.Value = health;
        }


        public void TakeDamage(float damage, string attacker, TankTeam attackerTeam)
        {
            if(attackerTeam != TankTeam.None && attackerTeam == _tankSetup.Team)
                return;

            Debug.Log("Tank health was: " + _health.Value);
            Debug.Log("Damage with armor: " + (damage / Armor));
            _health.Value -= damage / Armor;
            Debug.Log("Tank health become: " + _health.Value);

            if (_health.Value <= 0)
            {
                _health.Value = _maxHealth;
                UpdateGameInfo(attacker, attackerTeam);
                DestroyTank();
            }
        }

        private void UpdateGameInfo(string attacker, TankTeam tankTeam)
        {
            string victim = _tankSetup.Playername;
            _killTab.AddKillClientRpc(attacker, victim);
            if (_teamsDeathmatchMode != null)
                _teamsDeathmatchMode.AddTeamScore(tankTeam);
        }

        [ContextMenu("Destroy tank")]
        private void DestroyTank()
        {
            SetTankEnabled(false);
            _tankSound.PlayTankExplodeClientRpc();
            SpawnEffect();

            if(_tankFlagCatcher != null)
                _tankFlagCatcher.DropFlag();
        }

        private void SetTankEnabled(bool state)
        {
            _tankLife.SetControllableClientRpc(state);
            _tankLife.SetVisibleClientRpc(state);
            _tankUI.SetUIVisibleClientRpc(state);
        }

        private void SpawnEffect()
        {
            NetworkObject effect = Instantiate(_destroyTankEffect, transform.position, Quaternion.identity);
            effect.Spawn();
            effect.GetComponent<AutoDestroy>().OnDestroyed += RespawnTank;
        }

        private void RespawnTank()
        {
            SetTankEnabled(true);
            _tankLife.Respawn();
        }
    }
}